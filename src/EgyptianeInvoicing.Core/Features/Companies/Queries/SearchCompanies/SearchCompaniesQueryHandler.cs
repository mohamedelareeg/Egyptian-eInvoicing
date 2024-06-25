using AutoMapper;
using BuildingBlocks.Extentions;
using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Data.Repositories.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Companies.Queries.SearchCompanies
{
    public class SearchCompaniesQueryHandler : IListQueryHandler<SearchCompaniesQuery, CompanyDto>
    {
        private readonly ICompanyRepository _repository;
        private readonly IMapper _mapper;

        public SearchCompaniesQueryHandler(ICompanyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<CustomList<CompanyDto>>> Handle(SearchCompaniesQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllCompaniesAsync(request.Options);

            if (!result.IsSuccess)
            {
                return Result.Failure<CustomList<CompanyDto>>(result.Error);
            }
            var companyDtos = _mapper.Map<List<CompanyDto>>(result.Value);
            return Result.Success(companyDtos.ToCustomList());
        }
    }
}
