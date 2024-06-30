using BuildingBlocks.Primitives;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.ValueObjects;
using EgyptianeInvoicing.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EgyptianeInvoicing.Core.Models
{
    public class InvoiceLine : BaseEntity, IAuditableEntity
    {
        private readonly List<TaxableItem>? _taxableItems = new();

        public Guid InvoiceId { get; private set; }
        public string Description { get; private set; }
        public string? InternationalCode { get; private set; }
        public UnitType UnitType { get; private set; }
        public double Quantity { get; private set; }
        public string? InternalCode { get; private set; }

        private UnitValue _unitValue;
        private Discount? _discount;
        public UnitValue UnitValue
        {
            get => _unitValue;
            private set
            {
                _unitValue = value;
                UpdateTotals();
            }
        }
        public Discount? Discount
        {
            get => _discount;
            private set
            {
                _discount = value;
                UpdateTotals();
            }
        }
        public IReadOnlyCollection<TaxableItem>? TaxableItems => _taxableItems;

        public double SalesTotal { get; private set; } = 0;
        public double NetTotal { get; private set; } = 0;
        public double TotalTaxableFees { get; private set; } = 0;
        public double Total { get; private set; } = 0;


        private InvoiceLine() { }

        internal InvoiceLine(
            Guid id,
            string description,
            string? itemCode,
            UnitType unitType,
            double quantity,
            string? internationalCode,
            UnitValue unitValue,
            Discount? discount,
            List<TaxableItem>? taxableItems)
            : base(id)
        {
            Description = description;
            InternationalCode = itemCode;
            UnitType = unitType;
            Quantity = quantity;
            InternalCode = internationalCode;
            _unitValue = unitValue;
            _discount = discount;
            _taxableItems = taxableItems ?? new List<TaxableItem>();
            UpdateTotals();
        }

        public static Result<InvoiceLine> Create(
            string description,
            string? internationalCode,
            UnitType unitType,
            double quantity,
            string? internalCode,
            UnitValue unitValue,
            Discount? discount,
            List<TaxableItem>? taxableItems)
        {

            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<InvoiceLine>("InvoiceLine.Create", "Description is required.");

            if (string.IsNullOrWhiteSpace(internationalCode))
                return Result.Failure<InvoiceLine>("InvoiceLine.Create", "International Code is required.");

            if (!Enum.IsDefined(typeof(UnitType), unitType))
                return Result.Failure<InvoiceLine>("InvoiceLine.Create", $"Invalid unit type '{unitType}'.");

            if (quantity <= 0)
                return Result.Failure<InvoiceLine>("InvoiceLine.Create", "Quantity must be greater than zero.");

            var id = Guid.NewGuid();
            var invoiceLine = new InvoiceLine(
                id,
                description,
                internationalCode,
                unitType,
                quantity,
                internalCode,
                unitValue,
                discount,
                taxableItems);

            return Result.Success(invoiceLine);
        }
        #region Calculation
        private void UpdateTotals()
        {
            SalesTotal = CalculateSalesTotal();
            NetTotal = CalculateNetTotal();
            TotalTaxableFees = CalculateTotalTaxableFees();
            Total = CalculateTotal();
        }
        private double CalculateSalesTotal()
        {
            return Math.Round(UnitValue.Amount * Quantity, 5);
        }

        private double CalculateNetTotal()
        {
            if (Discount.Rate > 0)
            {
                Discount.UpdateAmount(SalesTotal);
            }

            return Math.Round((double)((SalesTotal) - Discount.Amount), 5);
        }


        private double CalculateTotalTaxableFees()
        {
            double totalTaxableFees = 0;

            foreach (var item in _taxableItems)
            {
                item.UpdateAmount(SalesTotal);
                totalTaxableFees += item.Amount;
            }

            return Math.Round(totalTaxableFees, 5);
        }

        private double CalculateTotal()
        {
            return Math.Round(NetTotal + TotalTaxableFees, 5);
        }

        #endregion
        public Result<bool> AddTaxableItem(TaxableItem taxableItem)
        {
            if (taxableItem == null)
                return Result.Failure<bool>("InvoiceLine.AddTaxableItem", "Taxable item cannot be null.");

            _taxableItems.Add(taxableItem);
            return Result.Success(true);
        }

        public Result<bool> RemoveTaxableItem(TaxableItem taxableItem)
        {
            if (taxableItem == null)
                return Result.Failure<bool>("InvoiceLine.RemoveTaxableItem", "Taxable item cannot be null.");

            var removed = _taxableItems.Remove(taxableItem);
            if (removed)
                return Result.Success(true);
            else
                return Result.Failure<bool>("InvoiceLine.RemoveTaxableItem", "Taxable item not found.");
        }

        public Result<TaxableItem> GetTaxableItemByCode(string itemCode)
        {
            var taxableItem = _taxableItems.FirstOrDefault(ti => ti.Code == itemCode);
            if (taxableItem == null)
                return Result.Failure<TaxableItem>("InvoiceLine.GetTaxableItemByCode", $"Taxable item with code '{itemCode}' not found.");

            return Result.Success(taxableItem);
        }

        public IReadOnlyCollection<TaxableItem> GetAllTaxableItems()
        {
            return _taxableItems.AsReadOnly();
        }
        public Result<bool> UpdateUnitValue(UnitValue newUnitValue)
        {
            if (newUnitValue == null)
                return Result.Failure<bool>("InvoiceLine.UpdateUnitValue", "UnitValue cannot be null.");

            UnitValue = newUnitValue;
            return Result.Success(true);
        }

        public Result<bool> UpdateDiscount(Discount newDiscount)
        {
            if (newDiscount == null)
                return Result.Failure<bool>("InvoiceLine.UpdateDiscount", "Discount cannot be null.");

            Discount = newDiscount;
            return Result.Success(true);
        }

    }
}
