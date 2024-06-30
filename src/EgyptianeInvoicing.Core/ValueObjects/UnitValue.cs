using BuildingBlocks.Primitives;
using BuildingBlocks.Results;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EgyptianeInvoicing.Core.ValueObjects
{
    public class UnitValue : ValueObject
    {
        private double _amountSold;
        private decimal _currencyExchangeRate;

        public string CurrencySold { get; private set; }
        public double Amount { get; private set; }

        public double AmountSold
        {
            get => _amountSold;
            private set
            {
                _amountSold = value;
                CalculateAmount();
            }
        }

        public decimal CurrencyExchangeRate
        {
            get => _currencyExchangeRate;
            private set
            {
                _currencyExchangeRate = value;
                CalculateAmount();
            }
        }

        private UnitValue() { }

        public UnitValue(string currencySold, double amountSold, decimal currencyExchangeRate)
        {
            CurrencySold = currencySold;
            _amountSold = amountSold;
            _currencyExchangeRate = currencyExchangeRate;
            CalculateAmount();
        }
        public override IEnumerable<object> GetAtomicValues()
        {
            yield return CurrencySold;
            yield return Amount;
            yield return _amountSold;
            yield return _currencyExchangeRate;
        }
        private void CalculateAmount()
        {
            Amount = Math.Round(_amountSold * (double)_currencyExchangeRate, 5);
        }

        public static Result<UnitValue> Create(string currencySold, double amountSold, decimal currencyExchangeRate)
        {
            if (string.IsNullOrWhiteSpace(currencySold))
                return Result.Failure<UnitValue>("UnitValue.Create", "Currency sold is required.");

            if (amountSold < 0)
                return Result.Failure<UnitValue>("UnitValue.Create", "Amount sold cannot be negative.");

            if (currencyExchangeRate <= 0)
                return Result.Failure<UnitValue>("UnitValue.Create", "Currency exchange rate must be greater than zero.");

            var unitValue = new UnitValue(currencySold, amountSold, currencyExchangeRate);
            return Result.Success(unitValue);
        }

        public Result<bool> UpdateAmountSold(double newAmountSold)
        {
            if (newAmountSold < 0)
                return Result.Failure<bool>("UnitValue.UpdateAmountSold", "Amount sold cannot be negative.");

            _amountSold = newAmountSold;
            CalculateAmount();

            return Result.Success(true);
        }

        public Result<bool> UpdateCurrencyExchangeRate(decimal newCurrencyExchangeRate)
        {
            if (newCurrencyExchangeRate <= 0)
                return Result.Failure<bool>("UnitValue.UpdateCurrencyExchangeRate", "Currency exchange rate must be greater than zero.");

            _currencyExchangeRate = newCurrencyExchangeRate;
            CalculateAmount();

            return Result.Success(true);
        }
    }
}
