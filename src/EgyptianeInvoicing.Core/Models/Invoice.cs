using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using BuildingBlocks.Primitives;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.ValueObjects;
using EgyptianeInvoicing.Shared.Enums;

namespace EgyptianeInvoicing.Core.Models
{
    public class Invoice : AggregateRoot, IAuditableEntity
    {
        private readonly List<InvoiceLine>? _invoiceLines = new();
        private readonly List<TaxTotal>? _taxTotals = new();

        public Guid IssuerId { get; private set; }
        public Company Issuer { get; private set; }
        public Guid ReceiverId { get; private set; }
        public Company Receiver { get; private set; }
        public string? EinvoiceId { get; private set; }
        public Guid? PurchaseOrderId { get; private set; }
        public Guid? SalesOrderId { get; private set; }
        public string InvoiceNumber { get; private set; }
        public Payment? Payment { get; private set; }
        public Delivery? Delivery { get; private set; }
        public InvoiceStatus Status { get; private set; } = InvoiceStatus.Valid;
        public DocumentType DocumentType { get; private set; } = DocumentType.Invoice;
        public Currency Currency { get; private set; } = Currency.EGP;
        public IReadOnlyCollection<InvoiceLine>? InvoiceLines => _invoiceLines;
        public IReadOnlyCollection<TaxTotal>? TaxTotals => _taxTotals;
        public double ExtraDiscountAmount { get; private set; } = 0;
        public double TotalDiscountAmount { get; private set; } = 0;
        public double TotalSalesAmount { get; private set; } = 0;
        public double NetAmount { get; private set; } = 0;
        public double TotalAmount { get; private set; } = 0;
        public double TotalItemsDiscountAmount { get; private set; } = 0;

        private Invoice() {
            //UpdateTotals();
        }

        private Invoice(
            Guid id,
            Guid issuerId,
            Guid receiverId,
            string? einvoiceId,
            Guid? purchaseOrderId,
            Guid? salesOrderId,
            string invoiceNumber,
            Payment? payment,
            Delivery? delivery,
            List<InvoiceLine>? invoiceLines,
            InvoiceStatus status,
            DocumentType documentType,
            Currency currency,
            double extraDiscountAmount)
            : base(id)
        {
            IssuerId = issuerId;
            ReceiverId = receiverId;
            EinvoiceId = einvoiceId;
            PurchaseOrderId = purchaseOrderId;
            SalesOrderId = salesOrderId;
            InvoiceNumber = invoiceNumber;
            Payment = payment;
            Delivery = delivery;
            Status = status;
            DocumentType = documentType;
            Currency = currency;
            _invoiceLines = invoiceLines ?? new List<InvoiceLine>();
            ExtraDiscountAmount = extraDiscountAmount;
            UpdateTotals();
        }

        public static Result<Invoice> Create(
            Guid issuerId,
            Guid receiverId,
            string? einvoiceId,
            Guid? purchaseOrderId,
            Guid? salesOrderId,
            string invoiceNumber,
            InvoiceStatus status = InvoiceStatus.Valid,
            DocumentType documentType = DocumentType.Invoice,
            Currency currency = Currency.EGP,
            double extraDiscountAmount = 0,
            Payment? payment = null,
            Delivery? delivery = null,
            List<InvoiceLine> invoiceLines = null)
        {
            // Validate required fields
            if (issuerId == Guid.Empty)
                return Result.Failure<Invoice>("Invoice.Create", "Issuer ID is required.");

            if (receiverId == Guid.Empty)
                return Result.Failure<Invoice>("Invoice.Create", "Receiver ID is required.");

            if (string.IsNullOrEmpty(invoiceNumber))
                return Result.Failure<Invoice>("Invoice.Create", "InvoiceNumber is required.");

            if (!Enum.IsDefined(typeof(InvoiceStatus), status))
                return Result.Failure<Invoice>("Invoice.Create", $"Invalid invoice status '{status}'.");

            if (!Enum.IsDefined(typeof(DocumentType), documentType))
                return Result.Failure<Invoice>("Invoice.Create", $"Invalid document type '{documentType}'.");

            if (!Enum.IsDefined(typeof(Currency), currency))
                return Result.Failure<Invoice>("Invoice.Create", $"Invalid currency type '{currency}'.");


            var id = Guid.NewGuid();
            var invoice = new Invoice(
                id,
                issuerId,
                receiverId,
                einvoiceId,
                purchaseOrderId,
                salesOrderId,
                invoiceNumber,
                payment,
                delivery,
                invoiceLines,
                status,
                documentType,
                currency,
                extraDiscountAmount);

            return Result.Success(invoice);
        }

        #region Calculation
        public double CalculateTotalSalesAmount()
        {
            double totalSales = 0;
            foreach (var line in _invoiceLines)
            {
                totalSales += line.SalesTotal;
            }
            return totalSales;
        }
        public double CalculateTotalDiscountAmount()
        {
            double totalDiscount = 0;
            foreach (var line in _invoiceLines)
            {
                totalDiscount += line.Discount?.Amount ?? 0;
            }
            return totalDiscount;
        }

        public double CalculateNetAmount()
        {
            return TotalSalesAmount - TotalDiscountAmount;
        }

        public double CalculateTotalItemsDiscountAmount()
        {
            double totalItemsDiscount = 0;
            foreach (var line in _invoiceLines)
            {
                totalItemsDiscount += line.Discount?.Amount ?? 0;
            }
            return totalItemsDiscount;
        }
        public Result<bool> UpdateTaxTotals()
        {
            _taxTotals.Clear();

            foreach (var line in _invoiceLines)
            {
                foreach (var tax in line.TaxableItems)
                {
                    //var amount = line.UnitValue.Amount * tax.Rate;

                    var existingTax = _taxTotals.FirstOrDefault(tt => tt.Code == tax.Code);
                    if (existingTax != null)
                    {
                        var increaseResult = existingTax.IncreaseAmount(tax.Amount);
                        if (increaseResult.IsFailure)
                        {
                            return Result.Failure<bool>(increaseResult.Error);
                        }
                    }
                    else
                    {
                        var addedTaxResult = TaxTotal.Create(tax.Code, tax.Amount);
                        if (addedTaxResult.IsFailure)
                        {
                            return Result.Failure<bool>(addedTaxResult.Error);
                        }
                        _taxTotals.Add(addedTaxResult.Value);
                    }
                }
            }

            return Result.Success(true);
        }

        public double CalculateTotalAmount()
        {
            UpdateTaxTotals();
            return NetAmount + _taxTotals.Sum(tt => tt.Amount) + ExtraDiscountAmount;
        }
        public void UpdateTotals()
        {
            TotalSalesAmount = CalculateTotalSalesAmount();
            TotalDiscountAmount = CalculateTotalDiscountAmount();
            NetAmount = CalculateNetAmount();
            TotalItemsDiscountAmount = CalculateTotalItemsDiscountAmount();
            TotalAmount = CalculateTotalAmount();
        }
        #endregion
        public Result<bool> AddInvoiceLine(InvoiceLine invoiceLine)
        {
            if (invoiceLine == null)
                return Result.Failure<bool>("Invoice.AddInvoiceLine", "Invoice line cannot be null.");

            _invoiceLines.Add(invoiceLine);
            UpdateTotals();
            return Result.Success(true);
        }

        public Result<bool> RemoveInvoiceLine(InvoiceLine invoiceLine)
        {
            if (invoiceLine == null)
                return Result.Failure<bool>("Invoice.RemoveInvoiceLine", "Invoice line cannot be null.");

            var removed = _invoiceLines.Remove(invoiceLine);
            if (removed)
            {
                UpdateTotals();
                return Result.Success(true);
            }
            else
                return Result.Failure<bool>("Invoice.RemoveInvoiceLine", "Invoice line not found.");
        }

        public Result<InvoiceLine> GetInvoiceLine(Guid invoiceLineId)
        {
            var invoiceLine = _invoiceLines.FirstOrDefault(il => il.Id == invoiceLineId);
            if (invoiceLine == null)
                return Result.Failure<InvoiceLine>("Invoice.GetInvoiceLine", $"Invoice line with ID '{invoiceLineId}' not found.");

            return Result.Success(invoiceLine);
        }

        public Result<InvoiceLine> GetInvoiceLineByName(string invoiceLineName)
        {
            var invoiceLine = _invoiceLines.FirstOrDefault(il => il.Description == invoiceLineName);
            if (invoiceLine == null)
                return Result.Failure<InvoiceLine>("Invoice.GetInvoiceLineByName", $"Invoice line with name '{invoiceLineName}' not found.");

            return Result.Success(invoiceLine);
        }

        public IReadOnlyCollection<InvoiceLine> GetAllInvoiceLines()
        {
            return _invoiceLines.AsReadOnly();
        }

        public Result<bool> AddTaxTotal(TaxTotal taxTotal)
        {
            if (taxTotal == null)
                return Result.Failure<bool>("Invoice.AddTaxTotal", "Tax total cannot be null.");

            _taxTotals.Add(taxTotal);
            return Result.Success(true);
        }

        public Result<TaxTotal> GetTaxTotalByCode(string taxCode)
        {
            var taxTotal = _taxTotals.FirstOrDefault(tt => tt.Code == taxCode);
            if (taxTotal == null)
                return Result.Failure<TaxTotal>("Invoice.GetTaxTotalByCode", $"Tax total with code '{taxCode}' not found.");

            return Result.Success(taxTotal);
        }

        public Result<bool> RemoveTaxTotal(TaxTotal taxTotal)
        {
            if (taxTotal == null)
                return Result.Failure<bool>("Invoice.RemoveTaxTotal", "Tax total cannot be null.");

            var removed = _taxTotals.Remove(taxTotal);
            if (removed)
                return Result.Success(true);
            else
                return Result.Failure<bool>("Invoice.RemoveTaxTotal", "Tax total not found.");
        }
    }
}
