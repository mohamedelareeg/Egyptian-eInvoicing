using BuildingBlocks.Primitives;
using BuildingBlocks.Results;
using System.Collections.Generic;

namespace EgyptianeInvoicing.Core.ValueObjects
{
    public sealed class Discount : ValueObject
    {
        public double Rate { get; private set; }
        public double? Amount { get; private set; }

        private Discount() { }

        private Discount(double rate, double? amount)
        {
            Rate = rate;
            Amount = amount;
        }

        public static Result<Discount> Create(double rate, double? amount)
        {
            if (rate < 0 || rate > 100)
                return Result.Failure<Discount>("Discount.Create", "Rate must be between 0 and 100.");

            //if (amount < 0)
            //    return Result.Failure<Discount>("Discount.Create", "Amount cannot be negative.");

            var discount = new Discount(rate, amount);
            return Result.Success(discount);
        }

        public Result<bool> UpdateAmount(double itemTotal)
        {

            if (itemTotal < 0)
                return Result.Failure<bool>("Discount.UpdateAmount", "Item total cannot be negative.");

            double newAmount = itemTotal * Rate / 100;
            Amount = newAmount;

            return Result.Success(true);
        }
        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Rate;
            yield return Amount;
        }
    }
}
