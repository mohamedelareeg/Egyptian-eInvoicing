using AutoMapper;
using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Data.Repositories.Abstractions;
using EgyptianeInvoicing.Core.Models;
using EgyptianeInvoicing.Core.ValueObjects;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommandHandler : ICommandHandler<CreateCompanyCommand, CompanyDto>
    {
        private readonly ICompanyRepository _repository;
        private readonly IMapper _mapper;
        public CreateCompanyCommandHandler(ICompanyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<CompanyDto>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            Address address = null;
            if (request.Address != null)
            {
                var addressResult = Address.Create(Int32.Parse(request.Address.BranchID), request.Address.Country, request.Address.Governate, request.Address.RegionCity, request.Address.Street, request.Address.BuildingNumber, request.Address.PostalCode, request.Address.Floor, request.Address.Room, request.Address.Landmark, request.Address.AdditionalInformation);

                if (addressResult.IsFailure)
                    return Result.Failure<CompanyDto>(addressResult.Error);

                address = addressResult.Value;
            }

            List<Payment> payments = null;
            if (request.Payments != null)
            {
                payments = new List<Payment>();
                foreach (var paymentDto in request.Payments)
                {
                    var paymentResult = Payment.Create(paymentDto.BankName, paymentDto.BankAddress, paymentDto.BankAccountNo, paymentDto.BankAccountIBAN, paymentDto.SwiftCode, paymentDto.Terms);
                    if (paymentResult.IsFailure)
                        return Result.Failure<CompanyDto>(paymentResult.Error);

                    payments.Add(paymentResult.Value);
                }
            }

            ClientCredentials credentials = null;
            if (request.Credentials != null)
            {
                var credentialsResult = ClientCredentials.Create(request.Credentials.ClientId, request.Credentials.ClientSecret1, request.Credentials.ClientSecret2, request.Credentials.TokenPin, request.Credentials.Certificate);

                if (credentialsResult.IsFailure)
                    return Result.Failure<CompanyDto>(credentialsResult.Error);

                credentials = credentialsResult.Value;
            }

            var companyResult = Company.Create(request.Name, request.Phone, request.Email, request.TaxNumber, request.CommercialRegistrationNo, address, request.ActivityCode, request.Type, credentials, payments);

            if (companyResult.IsFailure)
                return Result.Failure<CompanyDto>(companyResult.Error);

            await _repository.AddAsync(companyResult.Value);

            var createdCompanyDto = _mapper.Map<CompanyDto>(companyResult.Value);

            return Result.Success(createdCompanyDto);
        }
    }
}
