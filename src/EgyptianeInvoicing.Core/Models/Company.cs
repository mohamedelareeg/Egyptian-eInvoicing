using BuildingBlocks.Primitives;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.ValueObjects;
using EgyptianeInvoicing.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace EgyptianeInvoicing.Core.Models
{
    public class Company : AggregateRoot
    {
        private readonly List<Payment> _payments = new();
        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string? Email { get; private set; }
        public string? TaxNumber { get; private set; }
        public string CommercialRegistrationNo { get; private set; }
        public Address? Address { get; private set; }
        public string? ImagePath { get; private set; }
        public CompanyType Type { get; private set; } = CompanyType.B;
        public ClientCredentials? Credentials { get; private set; }
        public IReadOnlyCollection<Payment> Payments => _payments;

        private Company() { }

        private Company(
            Guid id,
            string name,
            string phone,
            string? email,
            string? taxNumber,
            string commercialRegistrationNo,
            Address address,
            CompanyType type = CompanyType.B,
            ClientCredentials? credentials = null,
            List<Payment> payments = null)
            : base(id)
        {

            Name = name;
            Phone = phone;
            Email = email;
            TaxNumber = taxNumber;
            CommercialRegistrationNo = commercialRegistrationNo;
            Address = address;
            Type = type;
            Credentials = credentials;
            _payments = payments ?? new List<Payment>();
        }

        public static Result<Company> Create(
            string name,
            string phone,
            string? email,
            string? taxNumber,
            string commercialRegistrationNo,
            Address address,
            CompanyType type = CompanyType.B,
            ClientCredentials? credentials = null,
            List<Payment> payments = null)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failure<Company>("Company.Create", "Company name is required.");

            if (!string.IsNullOrEmpty(email) && !IsValidEmail(email))
                return Result.Failure<Company>("Company.Create", "A valid email is required.");

            if (string.IsNullOrEmpty(commercialRegistrationNo))
                return Result.Failure<Company>("Company.Create", "Commercial registration number is required.");

            if (address == null)
                return Result.Failure<Company>("Company.Create", "Address is required.");

            if (!Enum.IsDefined(typeof(CompanyType), type))
                return Result.Failure<Company>("Company.Create", "Invalid company type.");

            if (credentials == null)
                return Result.Failure<Company>("Company.Create", "Client credentials are required.");

            var id = Guid.NewGuid();
            var company = new Company(
                id,
                name,
                phone,
                email,
                taxNumber,
                commercialRegistrationNo,
                address,
                type,
                credentials,
                payments);
            return Result.Success(company);
        }

        public Result<bool> ModifyName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failure<bool>("Company.ModifyName", "Company name cannot be null or empty.");

            Name = name;
            return Result.Success(true);
        }

        public Result<bool> ModifyPhone(string phone)
        {
            Phone = phone;
            return Result.Success(true);
        }

        public Result<bool> ModifyEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
                return Result.Failure<bool>("Company.ModifyEmail", "A valid email is required.");

            Email = email;
            return Result.Success(true);
        }

        public Result<bool> ModifyTaxNumber(string taxNumber)
        {
            if (string.IsNullOrEmpty(taxNumber))
                return Result.Failure<bool>("Company.ModifyTaxNumber", "Tax number cannot be null or empty.");

            TaxNumber = taxNumber;
            return Result.Success(true);
        }

        public Result<bool> ModifyCommercialRegistrationNo(string commercialRegistrationNo)
        {
            if (string.IsNullOrEmpty(commercialRegistrationNo))
                return Result.Failure<bool>("Company.ModifyCommercialRegistrationNo", "Commercial registration number cannot be null or empty.");

            CommercialRegistrationNo = commercialRegistrationNo;
            return Result.Success(true);
        }

        public Result<bool> ModifyAddress(Address address)
        {
            if (address == null)
                return Result.Failure<bool>("Company.ModifyAddress", "Address cannot be null.");

            Address = address;
            return Result.Success(true);
        }

        public Result<bool> SetImagePath(string? imagePath)
        {
            ImagePath = imagePath;
            return Result.Success(true);
        }

        public Result<bool> ModifyType(CompanyType type)
        {
            if (!Enum.IsDefined(typeof(CompanyType), type))
                return Result.Failure<bool>("Company.ModifyType", "Invalid company type.");

            Type = type;
            return Result.Success(true);
        }

        public Result<bool> ModifyCredentials(ClientCredentials? credentials)
        {
            if (credentials == null)
                return Result.Failure<bool>("Company.ModifyCredentials", "Credentials cannot be null.");

            Credentials = credentials;
            return Result.Success(true);
        }
        public Result<bool> AddPayment(Payment payment)
        {
            if (payment == null)
                return Result.Failure<bool>("Company.AddPayment", "Payment cannot be null.");

            _payments.Add(payment);
            return Result.Success(true);
        }

        public Result<bool> RemovePayment(Payment payment)
        {
            if (payment == null)
                return Result.Failure<bool>("Company.RemovePayment", "Payment cannot be null.");

            var paymentToRemove = Payments.FirstOrDefault(p => p.Equals(payment));
            if (paymentToRemove != null)
            {
                _payments.Remove(paymentToRemove);
                return Result.Success(true);
            }

            return Result.Failure<bool>("Company.RemovePayment", "Payment not found.");
        }
        public Result<bool> CleARPayments()
        {
            _payments.Clear();
            return Result.Success(true);
        }
        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
