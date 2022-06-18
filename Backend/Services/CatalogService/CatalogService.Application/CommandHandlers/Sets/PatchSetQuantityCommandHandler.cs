using BuildingBlocks.Exceptions;
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
    public class PatchSetQuantityCommandHandler : IRequestHandler<PatchSetQuantityCommand, Set>
    {
        private readonly ISetRepository _setRepository;

        public PatchSetQuantityCommandHandler(ISetRepository setRepository)
        {
            _setRepository = setRepository;
        }

        public async Task<Set> Handle(PatchSetQuantityCommand request, CancellationToken cancellationToken)
        {
            var set = await _setRepository.FindAsync(request.SetId);

            if (set == null)
            {
                throw new AggregateNotFoundException(nameof(Set), request.SetId);
            }

            set.ChangeQuantity(request.Quantity);

            return await _setRepository.PatchAsync(request.SetId, set);
        }
    }
}
