using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Core.Entities
{
    public class ImageUrl : ValueObject
    {
        public int Breakpoint { get; set; }
        public string Url { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Breakpoint;
            yield return Url;
        }
    }
}
