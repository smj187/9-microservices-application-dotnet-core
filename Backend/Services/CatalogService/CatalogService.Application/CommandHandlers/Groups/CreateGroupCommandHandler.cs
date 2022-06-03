using CatalogService.Application.Commands.Groups;
using CatalogService.Core.Domain.Group;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.CommandHandlers.Groups
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Group>
    {
        private readonly IGroupRepository _groupRepository;

        public CreateGroupCommandHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Group> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            await _groupRepository.AddAsync(request.NewGroup);
            return request.NewGroup;
        }
    }
}
