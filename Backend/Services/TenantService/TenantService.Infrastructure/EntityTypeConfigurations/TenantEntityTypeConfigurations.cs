using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantService.Core.Entities;

namespace TenantService.Infrastructure.EntityTypeConfigurations
{
    public class TenantEntityTypeConfigurations : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
