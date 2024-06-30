using BuildingBlocks.Primitives;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.ValueObjects;
using EgyptianeInvoicing.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Models
{
    public class PurchaseOrder : AggregateRoot, IAuditableEntity
    {
        private readonly List<InvoiceLine> _orderLines = new();
        private readonly List<TaxTotal> _taxTotals = new();

        public Guid BuyerId { get; private set; }
        public Guid SellerId { get; private set; }
        public string? OrderNumber { get; private set; }
        public Payment? Payment { get; private set; }
        public Delivery? Delivery { get; private set; }
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public Currency Currency { get; private set; } = Currency.EGP;
        public IReadOnlyCollection<InvoiceLine> OrderLines => _orderLines;
        public IReadOnlyCollection<TaxTotal> TaxTotals => _taxTotals;
        public double ExtraDiscountAmount { get; private set; } = 0;
        [NotMapped]
        public double TotalDiscountAmount { get; private set; } = 0;
        [NotMapped]
        public double TotalOrderAmount { get; private set; }
        [NotMapped]
        public double NetAmount { get; private set; }
        [NotMapped]
        public double TotalItemsDiscountAmount { get; private set; } = 0;
        [NotMapped]
        public double TotalAmount { get; private set; }

        private PurchaseOrder() { }

        private PurchaseOrder(
            Guid id,
            Guid buyerId,
            Guid sellerId,
            string? orderNumber,
            Payment payment,
            Delivery delivery,
            List<InvoiceLine> orderLines,
            List<TaxTotal> taxTotals,
            Currency currency,
            double extraDiscountAmount)
            : base(id)
        {
            BuyerId = buyerId;
            SellerId = sellerId;
            OrderNumber = orderNumber;
            Payment = payment;
            Delivery = delivery;
            Currency = currency;
            _orderLines = orderLines ?? new List<InvoiceLine>();
            _taxTotals = taxTotals ?? new List<TaxTotal>();
            ExtraDiscountAmount = extraDiscountAmount;
            UpdateTotals();
        }

        public static Result<PurchaseOrder> Create(
            Guid buyerId,
            Guid sellerId,
            string? orderNumber,
            List<InvoiceLine> orderLines,
            List<TaxTotal> taxTotals,
            Payment payment = null,
            Delivery delivery = null,
            Currency currency = Currency.EGP,
            double extraDiscountAmount = 0)
        {
            // Validate required fields
            if (buyerId == Guid.Empty)
                return Result.Failure<PurchaseOrder>("PurchaseOrder.Create", "Buyer ID is required.");

            if (sellerId == Guid.Empty)
                return Result.Failure<PurchaseOrder>("PurchaseOrder.Create", "Seller ID is required.");

            if (string.IsNullOrEmpty(orderNumber))
                return Result.Failure<PurchaseOrder>("PurchaseOrder.Create", "Order number is required.");

            if (orderLines == null || orderLines.Count == 0)
                return Result.Failure<PurchaseOrder>("PurchaseOrder.Create", "At least one order line is required.");

            if (!Enum.IsDefined(typeof(Currency), currency))
                return Result.Failure<PurchaseOrder>("PurchaseOrder.Create", $"Invalid currency type '{currency}'.");

            var id = Guid.NewGuid();
            var purchaseOrder = new PurchaseOrder(
                id,
                buyerId,
                sellerId,
                orderNumber,
                payment,
                delivery,
                orderLines,
                taxTotals,
                currency,
                extraDiscountAmount);

            return Result.Success(purchaseOrder);
        }

        #region Calculation
        private double CalculateTotalOrderAmount()
        {
            double totalOrderAmount = 0;
            foreach (var line in _orderLines)
            {
                totalOrderAmount += line.SalesTotal;
            }
            return totalOrderAmount;
        }
        private double CalculateTotalDiscountAmount()
        {
            double totalDiscount = 0;
            foreach (var line in _orderLines)
            {
                totalDiscount += line.Discount?.Amount ?? 0;
            }
            return totalDiscount;
        }

        private double CalculateNetAmount()
        {
            return TotalOrderAmount - TotalDiscountAmount;
        }

        private double CalculateTotalItemsDiscountAmount()
        {
            double totalItemsDiscount = 0;
            foreach (var line in _orderLines)
            {
                totalItemsDiscount += line.Discount?.Amount ?? 0;
            }
            return totalItemsDiscount;
        }

        private Result<bool> UpdateTaxTotals()
        {
            _taxTotals.Clear();

            foreach (var line in _orderLines)
            {
                foreach (var tax in line.TaxableItems)
                {
                    var amount = line.UnitValue.Amount * tax.Rate;

                    var existingTax = _taxTotals.FirstOrDefault(tt => tt.Code == tax.Code);
                    if (existingTax != null)
                    {
                        var increaseResult = existingTax.IncreaseAmount(amount);
                        if (increaseResult.IsFailure)
                        {
                            return Result.Failure<bool>(increaseResult.Error);
                        }
                    }
                    else
                    {
                        var addedTaxResult = TaxTotal.Create(tax.Code, amount);
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

        private double CalculateTotalAmount()
        {
            return NetAmount + _taxTotals.Sum(tt => tt.Amount) + ExtraDiscountAmount;
        }
        private void UpdateTotals()
        {
            TotalOrderAmount = CalculateTotalOrderAmount();
            TotalDiscountAmount = CalculateTotalDiscountAmount();
            NetAmount = CalculateNetAmount();
            TotalItemsDiscountAmount = CalculateTotalItemsDiscountAmount();
            UpdateTaxTotals();
            TotalAmount = CalculateTotalAmount();
        }
        #endregion

        public Result<bool> AddOrderLine(InvoiceLine orderLine)
        {
            if (orderLine == null)
                return Result.Failure<bool>("PurchaseOrder.AddOrderLine", "Order line cannot be null.");

            _orderLines.Add(orderLine);
            UpdateTotals();
            return Result.Success(true);
        }

        public Result<bool> RemoveOrderLine(InvoiceLine orderLine)
        {
            if (orderLine == null)
                return Result.Failure<bool>("PurchaseOrder.RemoveOrderLine", "Order line cannot be null.");

            var removed = _orderLines.Remove(orderLine);
            if (removed)
            {
                UpdateTotals();
                return Result.Success(true);
            }
            else
                return Result.Failure<bool>("PurchaseOrder.RemoveOrderLine", "Order line not found.");
        }

        public Result<InvoiceLine> GetOrderLine(Guid orderLineId)
        {
            var orderLine = _orderLines.FirstOrDefault(ol => ol.Id == orderLineId);
            if (orderLine == null)
                return Result.Failure<InvoiceLine>("PurchaseOrder.GetOrderLine", $"Order line with ID '{orderLineId}' not found.");

            return Result.Success(orderLine);
        }

        public Result<InvoiceLine> GetOrderLineByName(string orderLineName)
        {
            var orderLine = _orderLines.FirstOrDefault(ol => ol.Description == orderLineName);
            if (orderLine == null)
                return Result.Failure<InvoiceLine>("PurchaseOrder.GetOrderLineByName", $"Order line with name '{orderLineName}' not found.");

            return Result.Success(orderLine);
        }

        public IReadOnlyCollection<InvoiceLine> GetAllOrderLines()
        {
            return _orderLines.AsReadOnly();
        }

        public Result<bool> AddTaxTotal(TaxTotal taxTotal)
        {
            if (taxTotal == null)
                return Result.Failure<bool>("PurchaseOrder.AddTaxTotal", "Tax total cannot be null.");

            _taxTotals.Add(taxTotal);
            return Result.Success(true);
        }

        public Result<TaxTotal> GetTaxTotalByCode(string taxCode)
        {
            var taxTotal = _taxTotals.FirstOrDefault(tt => tt.Code == taxCode);
            if (taxTotal == null)
                return Result.Failure<TaxTotal>("PurchaseOrder.GetTaxTotalByCode", $"Tax total with code '{taxCode}' not found.");

            return Result.Success(taxTotal);
        }

        public Result<bool> RemoveTaxTotal(TaxTotal taxTotal)
        {
            if (taxTotal == null)
                return Result.Failure<bool>("PurchaseOrder.RemoveTaxTotal", "Tax total cannot be null.");

            var removed = _taxTotals.Remove(taxTotal);
            if (removed)
                return Result.Success(true);
            else
                return Result.Failure<bool>("PurchaseOrder.RemoveTaxTotal", "Tax total not found.");
        }
    }
}
