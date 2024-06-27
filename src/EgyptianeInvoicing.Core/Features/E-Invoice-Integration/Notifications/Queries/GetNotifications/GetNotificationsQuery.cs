using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Common.Notifications.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Notifications.Queries.GetNotifications
{
    public class GetNotificationsQuery : IQuery<NotificationsResponseDto>
    {
        public GetNotificationsQuery(Guid companyId, int pageSize, int pageNo, DateTime? dateFrom, DateTime? dateTo, string type, string language, string status, string channel)
        {
            CompanyId = companyId;
            PageSize = pageSize;
            PageNo = pageNo;
            DateFrom = dateFrom;
            DateTo = dateTo;
            Type = type;
            Language = language;
            Status = status;
            Channel = channel;
        }

        public Guid CompanyId { get; set; }
        public int PageSize { get; set; }
        public int PageNo { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
        public string Status { get; set; }
        public string Channel { get; set; }
    }
}
