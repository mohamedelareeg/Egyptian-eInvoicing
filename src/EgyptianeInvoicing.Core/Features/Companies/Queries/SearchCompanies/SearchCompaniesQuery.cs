using BuildingBlocks.Results;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.InvoiceSubmission.Details;
using EgyptianeInvoicing.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.Messaging;

namespace EgyptianeInvoicing.Core.Features.Companies.Queries.SearchCompanies
{
    public class SearchCompaniesQuery : IListQuery<CompanyDto>
    {
        public DataTableOptionsDto Options { get; set; }
    }

}
