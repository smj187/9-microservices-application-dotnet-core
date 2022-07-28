using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationService.Core.Aggregates;

namespace TranslationService.Application.Commands
{
    public class CreateTranslationCommand : IRequest<IReadOnlyCollection<Translation>>
    {
        public List<Translation> Translations { get; set; } = new();
    }
}
