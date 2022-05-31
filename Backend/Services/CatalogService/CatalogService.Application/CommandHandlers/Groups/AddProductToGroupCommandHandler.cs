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
    public class AddProductToGroupCommandHandler : IRequestHandler<AddProductToGroupCommand, Group>
    {
        private readonly IMongoRepository<Group> _mongoRepository;

        public AddProductToGroupCommandHandler(IMongoRepository<Group> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Group> Handle(AddProductToGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _mongoRepository.FindAsync(x => x.Id == request.GroupId);

            if (group == null)
            {
                throw new NotImplementedException();
            }

            group.AddProduct(request.ProductId);

            return await _mongoRepository.PatchAsync(request.GroupId, group);
        }
    }
}
