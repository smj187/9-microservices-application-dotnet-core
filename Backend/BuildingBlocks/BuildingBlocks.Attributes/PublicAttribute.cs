using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class PublicAttribute : Attribute
    {
        private readonly string _description;

        public PublicAttribute(string description)
        {
            _description = description;
        }
    }
}
