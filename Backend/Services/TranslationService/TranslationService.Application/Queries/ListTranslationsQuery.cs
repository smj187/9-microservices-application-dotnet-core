using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationService.Core.Aggregates;

namespace TranslationService.Application.Queries
{
    public class ListTranslationsQuery : IRequest<IReadOnlyCollection<Translation>>
    {
        public List<string> Keys { get; set; } = new();
        public string TenantId { get; set; } = default!;
        public string? Service { get; set; }
        public string? Resource { get; set; }
        public string? Identifier { get; set; }
        public string? Field { get; set; }
        public string? Locale { get; set; }
    }
}
