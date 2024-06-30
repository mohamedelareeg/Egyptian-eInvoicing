using AutoMapper;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Data.Abstractions.Repositories;
using EgyptianeInvoicing.Core.Data.Abstractions;
using EgyptianeInvoicing.Core.Models;
using EgyptianeInvoicing.Core.Services.Abstractions;
using EgyptianeInvoicing.Core.ValueObjects;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EgyptianeInvoicing.Shared.Enums;

namespace EgyptianeInvoicing.Core.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(ICompanyRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CompanyDto>> CreateCompanyAsync(
            string name,
            string phone,
            string email,
            string taxNumber,
            string commercialRegistrationNo,
            string branchID,
            string country,
            string governate,
            string regionCity,
            string street,
            string buildingNumber,
            string postalCode,
            string floor,
            string room,
            string landmark,
            string additionalInformation,
            string activityCode,
            string type,
            string clientId,
            string clientSecret1,
            string clientSecret2,
            string tokenPin,
            string certificate,
            List<PaymentDto> payments
        )
        {
            Address address = null;
            var addressResult = Address.Create(Int32.Parse(branchID), country, governate, regionCity, street, buildingNumber, postalCode, floor, room, landmark, additionalInformation);

            if (addressResult.IsFailure)
                return Result.Failure<CompanyDto>(addressResult.Error);

            address = addressResult.Value;
            List<Payment> paymentEntities = new List<Payment>();
            if (payments is not null)
            {
                foreach (var paymentDto in payments)
                {
                    var paymentResult = Payment.Create(paymentDto.BankName, paymentDto.BankAddress, paymentDto.BankAccountNo, paymentDto.BankAccountIBAN, paymentDto.SwiftCode, paymentDto.Terms);
                    if (paymentResult.IsFailure)
                        return Result.Failure<CompanyDto>(paymentResult.Error);

                    paymentEntities.Add(paymentResult.Value);
                }
            }

            ClientCredentials credentials = null;
            if (!string.IsNullOrEmpty(clientId) &&
                !string.IsNullOrEmpty(clientSecret1) &&
                !string.IsNullOrEmpty(clientSecret2) &&
                !string.IsNullOrEmpty(tokenPin) &&
                !string.IsNullOrEmpty(certificate))
            {
                var credentialsResult = ClientCredentials.Create(clientId, clientSecret1, clientSecret2, tokenPin, certificate);

                if (credentialsResult.IsFailure)
                    return Result.Failure<CompanyDto>(credentialsResult.Error);

                credentials = credentialsResult.Value;

            }
            if (!Enum.TryParse<CompanyType>(type, out var companyType))
            {
                return Result.Failure<CompanyDto>("CreateCompanyAsync", "Invalid company type specified.");
            }
            var companyResult = Company.Create(name, phone, email, taxNumber, commercialRegistrationNo, address, activityCode, companyType, credentials, paymentEntities);

            if (companyResult.IsFailure)
                return Result.Failure<CompanyDto>(companyResult.Error);

            await _repository.AddAsync(companyResult.Value);
            await _unitOfWork.SaveChangesAsync(default);

            var createdCompanyDto = _mapper.Map<CompanyDto>(companyResult.Value);
            return Result.Success(createdCompanyDto);
        }
    }
}
