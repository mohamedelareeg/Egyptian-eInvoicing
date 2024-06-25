using BuildingBlocks.Messaging;
using BuildingBlocks.Results;
using EgyptianeInvoicing.Core.Data.Repositories.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgyptianeInvoicing.Core.Features.Companies.Commands.DeleteCompany
{
    public class DeleteCompanyCommandHandler : ICommandHandler<DeleteCompanyCommand, bool>
    {
        private readonly ICompanyRepository _repository;

        public DeleteCompanyCommandHandler(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<bool>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var existingCompany = await _repository.GetByIdAsync(request.Id);
            if (existingCompany == null)
                return Result.Failure<bool>("DeleteCompanyCommand", "Company not found.");

            await _repository.DeleteAsync(existingCompany);

            return Result.Success(true);
        }
    }
}
