using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Companies.Queries.GetCompanyById
{
    public class GetCompanyByIdQuery : IQuery<CompanyDto>
    {
        public Guid Id { get; set; }
    }
}
