using AutoMapper;
using IdentityService.Contracts.v1;
using IdentityService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API.Profiles.ActionMappings
{
    public class SetRegistrationTokenExpirationMappingAction : IMappingAction<InternalUserModel, UserResponse>
    {
        public void Process(InternalUserModel source, UserResponse destination, ResolutionContext context)
        {
            if (source.ExpiresAt == null)
            {
                destination.ExpiresAt = null;
            }
        }
    }
}
