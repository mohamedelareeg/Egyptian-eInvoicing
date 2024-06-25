using EgyptianeInvoicing.MVC.Base;
using EgyptianeInvoicing.MVC.Clients.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using EgyptianeInvoicing.Shared.Dtos;
using EgyptianeInvoicing.Shared.Requests;

namespace EgyptianeInvoicing.MVC.Clients
{
    public class CompanyClient : BaseClient, ICompanyClient
    {
        public CompanyClient(IHttpClientFactory httpClientFactory, ILogger<CompanyClient> logger, IHttpContextAccessor httpContextAccessor)
          : base(httpClientFactory.CreateClient("ApiClient"), logger, httpContextAccessor)
        {
        }

        public async Task<BaseResponse<CompanyDto>> CreateCompanyAsync(CreateCompanyRequestDto request)
        {
            return await PostAsync<CreateCompanyRequestDto, BaseResponse<CompanyDto>>("api/v1/companies", request);
        }

        public async Task<BaseResponse<CompanyDto>> GetCompanyAsync(Guid id)
        {
            return await GetAsync<BaseResponse<CompanyDto>>($"api/v1/companies/{id}");
        }

        public async Task<BaseResponse<CustomList<CompanyDto>>> SearchCompaniesAsync(DataTableOptionsDto options)
        {
            return await PostAsync<DataTableOptionsDto, BaseResponse<CustomList<CompanyDto>>>("api/v1/companies/search", options);
        }

        public async Task<BaseResponse<bool>> UpdateCompanyAsync(UpdateCompanyRequestDto request)
        {
            return await PutAsync<UpdateCompanyRequestDto, BaseResponse<bool>>($"api/v1/companies/{request.Id}", request);
        }

        public async Task<BaseResponse<bool>> DeleteCompanyAsync(Guid id)
        {
            return await DeleteAsync<BaseResponse<bool>>($"api/v1/companies/{id}");
        }
    }
}
