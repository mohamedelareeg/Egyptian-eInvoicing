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
        Task<HttpResponseMessage> CreateEGSCodeUsageAsync(List<CreateEGSCodeUsageItemDto> request);
        Task<List<CodeUsageRequestDetailsDto>> SearchCodeUsageRequestsAsync(string active, string status, string pageSize, string pageNumber, string orderDirection);
        Task<HttpResponseMessage> RequestCodeReuseAsync(List<CodeUsageItemDto> request);
        Task<List<PublishedCodeDto>> SearchPublishedCodesAsync(string codeType, string parentLevelName, bool onlyActive, DateTime activeFrom, int pageSize, int pageNumber);
        Task<GetCodeDetailsResponseDto> GetCodeDetailsByItemCodeAsync(string codeType, string itemCode);
        Task<HttpResponseMessage> UpdateEGSCodeUsageAsync(int codeUsageRequestId, UpdateEGSCodeUsageRequestDto request);
        Task<HttpResponseMessage> UpdateCodeAsync(string codeType, string itemCode, CodeUpdateRequestDto codeUpdateRequest);
    }
}
