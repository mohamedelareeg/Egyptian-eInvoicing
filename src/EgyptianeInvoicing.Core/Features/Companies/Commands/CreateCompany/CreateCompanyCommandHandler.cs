using AutoMapper;
using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Data.Abstractions;
using EgyptianeInvoicing.Core.Data.Abstractions.Repositories;
using EgyptianeInvoicing.Core.Models;
using EgyptianeInvoicing.Core.Services.Abstractions;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyService _companyService;

        public CreateCompanyCommandHandler(ICompanyRepository repository, IMapper mapper, IUnitOfWork unitOfWork, ICompanyService companyService)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _companyService = companyService;
        }

        public async Task<Result<CompanyDto>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            return await _companyService.CreateCompanyAsync(
                request.Name,
                request.Phone,
                request.Email,
                request.TaxNumber,
                request.CommercialRegistrationNo,
                request.Address.BranchID,
                request.Address.Country,
                request.Address.Governate,
                request.Address.RegionCity,
                request.Address.Street,
                request.Address.BuildingNumber,
                request.Address.PostalCode,
                request.Address.Floor,
                request.Address.Room,
                request.Address.Landmark,
                request.Address.AdditionalInformation,
                request.ActivityCode,
                request.Type.ToString(),
                request.Credentials.ClientId,
                request.Credentials.ClientSecret1,
                request.Credentials.ClientSecret2,
                request.Credentials.TokenPin,
                request.Credentials.Certificate,
                request.Payments
            );
        }
    }
}
