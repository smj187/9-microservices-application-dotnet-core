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
    public class PatchSetPriceCommandHandler : IRequestHandler<PatchSetPriceCommand, Set>
    {
        private readonly ISetRepository _setRepository;

        public PatchSetPriceCommandHandler(ISetRepository setRepository)
        {
            _setRepository = setRepository;
        }

        public async Task<Set> Handle(PatchSetPriceCommand request, CancellationToken cancellationToken)
        {
            var set = await _setRepository.FindAsync(request.SetId);

            if (set == null)
            {
                throw new AggregateNotFoundException(nameof(Set), request.SetId);
            }

            set.ChangePrice(request.Price);

            return await _setRepository.PatchAsync(request.SetId, set);
        }
    }
}
