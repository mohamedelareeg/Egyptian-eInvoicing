using BuildingBlocks.Primitives;
using BuildingBlocks.Results;
using System.Collections.Generic;

namespace EgyptianeInvoicing.Core.ValueObjects
{
    public sealed class TaxTotal : ValueObject
    {
        public string Code { get; private set; }
        public double Amount { get; private set; }

        private TaxTotal() { }

        private TaxTotal(string code, double amount)
        {
            Code = code;
            Amount = amount;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Code;
            yield return Amount;
        }

        public static Result<TaxTotal> Create(string code, double amount)
        {
            if (string.IsNullOrWhiteSpace(code))
                return Result.Failure<TaxTotal>("TaxTotal.Create", "Code is required.");

            if (amount < 0)
                return Result.Failure<TaxTotal>("TaxTotal.Create", "Amount cannot be negative.");

            var taxTotal = new TaxTotal(code, amount);
            return Result.Success(taxTotal);
        }

        public Result<bool> Modify(string code, double amount)
        {
            if (string.IsNullOrWhiteSpace(code))
                return Result.Failure<bool>("TaxTotal.Modify", "Code is required.");

            if (amount < 0)
                return Result.Failure<bool>("TaxTotal.Modify", "Amount cannot be negative.");

            Code = code;
            Amount = amount;

            return Result.Success(true);
        }
        public Result<bool> IncreaseAmount(double additionalAmount)
        {
            if (additionalAmount < 0)
                return Result.Failure<bool>("TaxTotal.IncreaseAmount", "Additional amount cannot be negative.");

            Amount += additionalAmount;
            return Result.Success(true);
        }
        public Result<bool> DecreaseAmount(double reductionAmount)
        {
            if (reductionAmount < 0)
                return Result.Failure<bool>("TaxTotal.DecreaseAmount", "Reduction amount cannot be negative.");

            if (Amount < reductionAmount)
                return Result.Failure<bool>("TaxTotal.DecreaseAmount", "Cannot reduce amount below zero.");

            Amount -= reductionAmount;
            return Result.Success(true);
        }
    }
}
