using BuildingBlocks.Primitives;
using BuildingBlocks.Results;
using System.Collections.Generic;

namespace EgyptianeInvoicing.Core.ValueObjects
{
    public class TaxableItem : ValueObject
    {
        public string ParentCode { get; }
        public string Code { get; }
        public double Rate { get; }
        public double Amount { get; private set; }

        private TaxableItem() { }

        private TaxableItem(string parentCode, string code, double rate, double amount = 0)
        {
            ParentCode = parentCode;
            Code = code;
            Rate = rate;
            Amount = amount;
        }

        public static Result<TaxableItem> Create(string parentCode, string code, double rate, double amount = 0)
        {
            if (string.IsNullOrWhiteSpace(parentCode))
                return Result.Failure<TaxableItem>("TaxableItem.Create", "Parent code is required.");

            if (string.IsNullOrWhiteSpace(code))
                return Result.Failure<TaxableItem>("TaxableItem.Create", "Code is required.");

            if (rate < 0)
                return Result.Failure<TaxableItem>("TaxableItem.Create", "Rate cannot be negative.");

            var taxableItem = new TaxableItem(parentCode, code, rate, amount);
            return Result.Success(taxableItem);
        }
        public Result<bool> UpdateAmount(double itemTotal)
        {
            if (itemTotal < 0)
                return Result.Failure<bool>("TaxableItem.UpdateAmount", "Item total cannot be negative.");

            double newAmount;
            if (ParentCode == "T1" || ParentCode == "T2" || ParentCode == "T3")
            {
                newAmount = itemTotal * (Rate / 100);
            }
            else if (ParentCode == "T4")
            {
                newAmount = -(itemTotal * (Rate / 100));
            }
            else
            {
                newAmount = itemTotal * (Rate / 100);
            }

            Amount = newAmount;
            return Result.Success(true);
        }
        public override IEnumerable<object> GetAtomicValues()
        {
            yield return ParentCode;
            yield return Code;
            yield return Rate;
            yield return Amount;
        }
    }
}
