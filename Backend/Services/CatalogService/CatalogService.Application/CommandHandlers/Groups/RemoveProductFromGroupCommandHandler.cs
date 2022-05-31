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
    public class RemoveProductFromGroupCommandHandler : IRequestHandler<RemoveProductFromGroupCommand, Group>
    {
        private readonly IMongoRepository<Group> _mongoRepository;

        public RemoveProductFromGroupCommandHandler(IMongoRepository<Group> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Group> Handle(RemoveProductFromGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _mongoRepository.FindAsync(x => x.Id == request.GroupId);

            if (group == null)
            {
                throw new NotImplementedException();
            }

            group.RemoveProduct(request.ProductId);

            return await _mongoRepository.PatchAsync(request.GroupId, group);
        }
    }
}
