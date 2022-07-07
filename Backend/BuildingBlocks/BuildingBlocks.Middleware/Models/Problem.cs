using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BuildingBlocks.Middleware.Models
{
    public class ProblemResponse
    {
        public string? detail { get; set; } = null!;
        public string? instance { get; set; } = null!;
        public int status { get; set; }
        public string? title { get; set; } = null!;
        public string? type { get; set; } = null!;
        public string? traceId { get; set; } = null!;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
