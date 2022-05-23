﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Core.Entities;

namespace TenantService.Application.Queries
{
    public class ListTenantsQuery : IRequest<IEnumerable<Tenant>>
    {

    }
}
