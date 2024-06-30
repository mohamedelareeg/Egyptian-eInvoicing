using AutoMapper;
using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Features.Invoices.Commands.CreateInvoice;
using EgyptianeInvoicing.Core.Services.Abstractions;
using EgyptianeInvoicing.Shared.Dtos;

public class CreateInvoiceCommandHandler : ICommandHandler<CreateInvoiceCommand, InvoiceDto>
{
    private readonly IInvoiceService _invoiceService;
    private readonly IMapper _mapper;

    public CreateInvoiceCommandHandler(IInvoiceService invoiceService, IMapper mapper)
    {
        _invoiceService = invoiceService;
        _mapper = mapper;
    }

    public async Task<Result<InvoiceDto>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var createdInvoiceResult = await _invoiceService.CreateInvoiceAsync(
                   request.IssuerId,
                   request.ReceiverId,
                   request.EinvoiceId,
                   request.PurchaseOrderId,
                   request.SalesOrderId,
                   request.InvoiceNumber,
                   request.InvoiceLines,
                   request.Payment,
                   request.Delivery,
                   request.Status,
                   request.DocumentType,
                   request.Currency,
                   request.ExtraDiscountAmount,
                   request.Receiver
               );

        if (createdInvoiceResult.IsFailure)
        {
            return Result.Failure<InvoiceDto>(createdInvoiceResult.Error);
        }
        var invoiceDto = _mapper.Map<InvoiceDto>(createdInvoiceResult.Value);
        return invoiceDto;
    }
}