using BuildingBlocks.Mongo;
using CatalogService.Application.Commands;
using CatalogService.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.CommandHandlers
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Group>
    {
        private readonly IMongoRepository<Group> _mongoRepository;

        public CreateGroupCommandHandler(IMongoRepository<Group> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task<Group> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            await _mongoRepository.AddAsync(request.NewGroup);
            return request.NewGroup;
        }
    }
}
