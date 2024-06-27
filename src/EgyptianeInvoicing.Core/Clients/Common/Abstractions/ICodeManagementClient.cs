using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.CreateEGSCode.Request;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.GetCodeDetailsByItemCode.Response;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.RequestCodeReuse.Response;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.SearchCodeUsage.Request;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.SearchPublishedCodes.Request;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.UpdateCode.Request;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Codes.UpdateEGSCodeUsage.Request;

namespace EgyptianeInvoicing.Core.Clients.Common.Abstractions
{
    public interface ICodeManagementClient
    {
        Task<HttpResponseMessage> CreateEGSCodeUsageAsync(Guid companyId, List<CreateEGSCodeUsageItemDto> request);
        Task<List<CodeUsageRequestDetailsDto>> SearchCodeUsageRequestsAsync(Guid companyId, string active, string status, string pageSize, string pageNumber, string orderDirection);
        Task<HttpResponseMessage> RequestCodeReuseAsync(Guid companyId, List<CodeUsageItemDto> request);
        Task<List<PublishedCodeDto>> SearchPublishedCodesAsync(Guid companyId, string codeType, string parentLevelName, bool onlyActive, DateTime activeFrom, int pageSize, int pageNumber);
        Task<GetCodeDetailsResponseDto> GetCodeDetailsByItemCodeAsync(Guid companyId, string codeType, string itemCode);
        Task<HttpResponseMessage> UpdateEGSCodeUsageAsync(Guid companyId, int codeUsageRequestId, UpdateEGSCodeUsageRequestDto request);
        Task<HttpResponseMessage> UpdateCodeAsync(Guid companyId, string codeType, string itemCode, CodeUpdateRequestDto codeUpdateRequest);
    }
}
