using BuildingBlocks.Mongo;
using CatalogService.Application.Commands.Groups;
using CatalogService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.CommandHandlers.Groups
{
    public class PatchGroupDescriptionCommandHandler : IRequestHandler<PatchGroupDescriptionCommand, Group>
    {
        private readonly IMongoRepository<Group> _mongoRepository;

        public PatchGroupDescriptionCommandHandler(IMongoRepository<Group> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Group> Handle(PatchGroupDescriptionCommand request, CancellationToken cancellationToken)
        {
            var group = await _mongoRepository.FindAsync(x => x.Id == request.GroupId);

            if (group == null)
            {
                throw new NotImplementedException();
            }


            group.ChangeDescription(request.Name, request.Description, request.PriceDescription, request.Tags);

            return await _mongoRepository.PatchAsync(request.GroupId, group);
        }
    }
}
