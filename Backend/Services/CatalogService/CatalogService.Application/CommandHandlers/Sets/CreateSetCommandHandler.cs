using CatalogService.Application.Commands.Sets;
using CatalogService.Core.Domain.Sets;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.CommandHandlers.Sets
{
    public class CreateSetCommandHandler : IRequestHandler<CreateSetCommand, Set>
    {
        private readonly ISetRepository _setRepository;

        public CreateSetCommandHandler(ISetRepository setRepository)
        {
            _setRepository = setRepository;
        }

        public async Task<Set> Handle(CreateSetCommand request, CancellationToken cancellationToken)
        {
            var set = new Set(request.TenantId, Guid.NewGuid(), request.Name, request.Price, request.Description, request.PriceDescription, request.Tags, request.Quantity);

            await _setRepository.AddAsync(set);

            return set;
        }
    }
}
