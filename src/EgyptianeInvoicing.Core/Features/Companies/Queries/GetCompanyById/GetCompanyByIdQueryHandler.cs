using AutoMapper;
using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Data.Abstractions.Repositories;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Companies.Queries.GetCompanyById
{
    public class GetCompanyByIdQueryHandler : IQueryHandler<GetCompanyByIdQuery, CompanyDto>
    {
        private readonly ICompanyRepository _repository;
        private readonly IMapper _mapper;

        public GetCompanyByIdQueryHandler(ICompanyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<CompanyDto>> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _repository.GetByIdAsync(request.Id);
            if (company == null)
                return Result.Failure<CompanyDto>("GetCompanyByIdQuery", "Company not found.");

            var companyDto = _mapper.Map<CompanyDto>(company);

            return Result.Success(companyDto);
        }
    }
}
