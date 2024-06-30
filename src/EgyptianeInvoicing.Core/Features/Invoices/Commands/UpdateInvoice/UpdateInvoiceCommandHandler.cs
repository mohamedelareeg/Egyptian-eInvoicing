using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Data.Abstractions;
using EgyptianeInvoicing.Core.Data.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Invoices.Commands.UpdateInvoice
{
    public class UpdateInvoiceCommandHandler : ICommandHandler<UpdateInvoiceCommand, bool>
    {
        private readonly IInvoiceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateInvoiceCommandHandler(IInvoiceRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var existingInvoice = await _repository.GetByIdAsync(request.Id);
            if (existingInvoice == null)
                return Result.Failure<bool>("UpdateInvoiceCommand", "Invoice not found.");


            //await _repository.UpdateAsync(existingInvoice);
            //await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(true);
        }
    }
}
