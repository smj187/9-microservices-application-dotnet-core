using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationService.Core.Aggregates;

namespace TranslationService.Application.Commands
{
    public class PatchMultilingualCommand : IRequest<Translation>
    {
        public string Key { get; set; } = default!;
        public string Locale { get; set; } = default!;
        public string Value { get; set; } = default!;
    }
}
