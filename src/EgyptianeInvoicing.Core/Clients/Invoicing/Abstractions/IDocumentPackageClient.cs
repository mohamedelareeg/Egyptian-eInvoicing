﻿using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentPackage.GetPackagesRequest.Response;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentPackage.RequestDocumentPackage.Request;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentPackage.RequestDocumentPackage.Response;

namespace EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions
{
    public interface IDocumentPackageClient
    {
        Task<PackageDownloadResponseDto> RequestDocumentPackageAsync(DocumentPackageRequestDto requestDto);
        Task<DocumentPackageResponseDto> GetPackagesRequestAsync(
            int pageSize,
            int pageNo,
            DateTime dateFrom,
            DateTime dateTo,
            string documentTypeName = "",
            string statuses = "",
            string productsInternalCodes = "",
            int receiverSenderType = 0,
            string receiverSenderId = "",
            string branchNumber = "",
            string itemCodes = ""
        );
        Task<byte[]> GetDocumentPackageAsync(string rid);
    }
}
