﻿using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Clients.Invoicing.Abstractions;
using EgyptianeInvoicing.Shared.Dtos.ClientsDto.Invoicing.DocumentOperations.GetSubmission.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.DocumentOperations.Queries.GetSubmission
{
    public class GetSubmissionQueryHandler : IQueryHandler<GetSubmissionQuery, SubmissionResponseDto>
    {
        private readonly IDocumentOperationsClient _documentOperationsClient;

        public GetSubmissionQueryHandler(IDocumentOperationsClient documentOperationsClient)
        {
            _documentOperationsClient = documentOperationsClient;
        }

        public async Task<Result<SubmissionResponseDto>> Handle(GetSubmissionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _documentOperationsClient.GetSubmissionAsync(request.SubmissionUUID, request.PageSize, request.PageNo);

                return Result.Success(result);
            }
            catch (HttpRequestException ex)
            {
                return Result.Failure<SubmissionResponseDto>("GetSubmissionQuery", $"Failed to get submission: {ex.Message}");
            }
        }
    }
}