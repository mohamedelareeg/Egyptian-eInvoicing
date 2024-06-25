using BuildingBlocks.Primitives;
using BuildingBlocks.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.ValueObjects
{
    public sealed class Payment : ValueObject
    {
        public string? BankName { get; private set; }
        public string? BankAddress { get; private set; }
        public string? BankAccountNo { get; private set; }
        public string? BankAccountIBAN { get; private set; }
        public string? SwiftCode { get; private set; }
        public string? Terms { get; private set; }

        private Payment() { }

        private Payment(string bankName, string bankAddress, string bankAccountNo, string bankAccountIBAN, string swiftCode, string terms)
        {
            BankName = bankName;
            BankAddress = bankAddress;
            BankAccountNo = bankAccountNo;
            BankAccountIBAN = bankAccountIBAN;
            SwiftCode = swiftCode;
            Terms = terms;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return BankName;
            yield return BankAddress;
            yield return BankAccountNo;
            yield return BankAccountIBAN;
            yield return SwiftCode;
            yield return Terms;
        }

        public static Result<Payment> Create(string bankName = null, string bankAddress = null, string bankAccountNo = null, string bankAccountIBAN = null, string swiftCode = null, string terms = null)
        {
            var payment = new Payment(bankName, bankAddress, bankAccountNo, bankAccountIBAN, swiftCode, terms);
            return Result.Success(payment);
        }

        public Result<bool> Modify(string bankName = null, string bankAddress = null, string bankAccountNo = null, string bankAccountIBAN = null, string swiftCode = null, string terms = null)
        {
            BankName = bankName;
            BankAddress = bankAddress;
            BankAccountNo = bankAccountNo;
            BankAccountIBAN = bankAccountIBAN;
            SwiftCode = swiftCode;
            Terms = terms;

            return Result.Success(true);
        }
    }
}
