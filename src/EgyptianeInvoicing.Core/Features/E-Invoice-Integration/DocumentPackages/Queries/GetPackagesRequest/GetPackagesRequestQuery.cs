using BuildingBlocks.Messaging;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentPackage.GetPackagesRequest.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentPackages.Queries.GetPackagesRequest
{
    public class GetPackagesRequestQuery : IQuery<DocumentPackageResponseDto>
    {
        public GetPackagesRequestQuery(Guid companyId, int pageSize, int pageNo, DateTime dateFrom, DateTime dateTo, string documentTypeName, string statuses, string productsInternalCodes, int receiverSenderType, string receiverSenderId, string branchNumber, string itemCodes)
        {
            CompanyId = companyId;
            PageSize = pageSize;
            PageNo = pageNo;
            DateFrom = dateFrom;
            DateTo = dateTo;
            DocumentTypeName = documentTypeName;
            Statuses = statuses;
            ProductsInternalCodes = productsInternalCodes;
            ReceiverSenderType = receiverSenderType;
            ReceiverSenderId = receiverSenderId;
            BranchNumber = branchNumber;
            ItemCodes = itemCodes;
        }

        public Guid CompanyId { get; set; }
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string DocumentTypeName { get; set; }
        public string Statuses { get; set; }
        public string ProductsInternalCodes { get; set; }
        public int ReceiverSenderType { get; set; }
        public string ReceiverSenderId { get; set; }
        public string BranchNumber { get; set; }
        public string ItemCodes { get; set; }
    }
}
