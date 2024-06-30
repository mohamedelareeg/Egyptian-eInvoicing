using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Data.Abstractions;
using EgyptianeInvoicing.Core.Data.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Invoices.Commands.DeleteInvoice
{
    public class DeleteInvoiceCommandHandler : ICommandHandler<DeleteInvoiceCommand, bool>
    {
        private readonly IInvoiceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteInvoiceCommandHandler(IInvoiceRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var existingInvoice = await _repository.GetByIdAsync(request.Id);
            if (existingInvoice == null)
                return Result.Failure<bool>("DeleteInvoiceCommand", "Invoice not found.");

            await _repository.RemoveAsync(existingInvoice);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(true);
        }
    }
}
