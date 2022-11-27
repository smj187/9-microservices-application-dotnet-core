using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GatewayAuthenticationAttribute : Attribute
    {
        private string _description;
        private string? _requiredUserRole = null;

        public GatewayAuthenticationAttribute(string description)
        {
            _description = description;
        }

        public GatewayAuthenticationAttribute(string description, string requiredUserRole)
        {
            _description = description;
            _requiredUserRole = requiredUserRole;
        }

        public virtual string? RequiredUserRole
        {
            get => _requiredUserRole;
            set => _requiredUserRole = value;
        }
    }
}
