using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Contracts.v1.Requests
{
    public class CreateFileRequest
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
