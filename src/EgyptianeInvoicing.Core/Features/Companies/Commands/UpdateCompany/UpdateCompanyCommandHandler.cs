using Amazon.SecurityToken.Model;
using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Data.Repositories.Abstractions;
using EgyptianeInvoicing.Core.ValueObjects;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommandHandler : ICommandHandler<UpdateCompanyCommand, bool>
    {
        private readonly ICompanyRepository _repository;

        public UpdateCompanyCommandHandler(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<bool>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var existingCompany = await _repository.GetByIdAsync(request.Id);
            if (existingCompany == null)
                return Result.Failure<bool>("UpdateCompanyCommand", "Company not found.");

            if (!string.IsNullOrEmpty(request.Name))
            {
                var nameModificationResult = existingCompany.ModifyName(request.Name);
                if (nameModificationResult.IsFailure)
                    return Result.Failure<bool>(nameModificationResult.Error);
            }

            if (!string.IsNullOrEmpty(request.Phone))
            {
                var phoneModificationResult = existingCompany.ModifyPhone(request.Phone);
                if (phoneModificationResult.IsFailure)
                    return Result.Failure<bool>(phoneModificationResult.Error);
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                var emailModificationResult = existingCompany.ModifyEmail(request.Email);
                if (emailModificationResult.IsFailure)
                    return Result.Failure<bool>(emailModificationResult.Error);
            }

            if (!string.IsNullOrEmpty(request.TaxNumber))
            {
                var taxNumberModificationResult = existingCompany.ModifyTaxNumber(request.TaxNumber);
                if (taxNumberModificationResult.IsFailure)
                    return Result.Failure<bool>(taxNumberModificationResult.Error);
            }

            if (!string.IsNullOrEmpty(request.CommercialRegistrationNo))
            {
                var commercialRegistrationNoModificationResult = existingCompany.ModifyCommercialRegistrationNo(request.CommercialRegistrationNo);
                if (commercialRegistrationNoModificationResult.IsFailure)
                    return Result.Failure<bool>(commercialRegistrationNoModificationResult.Error);
            }

            if (request.Address != null)
            {
                var addressResult = Address.Create(request.Address.BranchId, request.Address.Country, request.Address.Governorate, request.Address.RegionCity, request.Address.Street, request.Address.BuildingNumber, request.Address.PostalCode, request.Address.Floor, request.Address.Room, request.Address.Landmark, request.Address.AdditionalInformation);
                if (addressResult.IsFailure)
                    return Result.Failure<bool>(addressResult.Error);

                var address = addressResult.Value;
                var addressModificationResult = existingCompany.ModifyAddress(address);
                if (addressModificationResult.IsFailure)
                    return Result.Failure<bool>(addressModificationResult.Error);
            }

            if (request.Type != existingCompany.Type)
            {
                var typeModificationResult = existingCompany.ModifyType(request.Type);
                if (typeModificationResult.IsFailure)
                    return Result.Failure<bool>(typeModificationResult.Error);
            }

            if (request.Credentials != null)
            {
                var credentialsResult = ClientCredentials.Create(request.Credentials.ClientId, request.Credentials.ClientSecret1, request.Credentials.ClientSecret2, request.Credentials.TokenPin, request.Credentials.Certificate);

                if (credentialsResult.IsFailure)
                    return Result.Failure<bool>(credentialsResult.Error);

                var credentials = credentialsResult.Value;

                var credentialsModificationResult = existingCompany.ModifyCredentials(credentials);
                if (credentialsModificationResult.IsFailure)
                    return Result.Failure<bool>(credentialsModificationResult.Error);
            }
            if (request.Payments != null)
            {
                existingCompany.CleARPayments();

                foreach (var item in request.Payments)
                {

                    var paymentResult = Payment.Create(item.BankName, item.BankAddress, item.BankAccountNo, item.BankAccountIBAN, item.SwiftCode, item.Terms);
                    if (paymentResult.IsFailure)
                        return Result.Failure<bool>(paymentResult.Error);

                    var payment = paymentResult.Value;

                    var addPaymentResult = existingCompany.AddPayment(payment);
                    if (addPaymentResult.IsFailure)
                        return Result.Failure<bool>(addPaymentResult.Error);
                }

            }

            await _repository.UpdateAsync(existingCompany);

            return Result.Success(true);
        }

    }
}
