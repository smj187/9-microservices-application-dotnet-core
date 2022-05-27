using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.API
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
            => new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> GetApiScope()
            => new List<ApiScope>();

        public static IEnumerable<ApiResource> GetApis()
            => new List<ApiResource>();

        public static IEnumerable<Client> GetClients()
            => new List<Client>();
    }
}
