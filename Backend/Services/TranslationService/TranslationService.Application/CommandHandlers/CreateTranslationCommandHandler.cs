using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationService.Application.Commands;
using TranslationService.Core.Aggregates;

namespace TranslationService.Application.CommandHandlers
{
    public class CreateTranslationCommandHandler : IRequestHandler<CreateTranslationCommand, IReadOnlyCollection<Translation>>
    {
        private readonly ITranslationRepository _translationRepository;

        public CreateTranslationCommandHandler(ITranslationRepository translationRepository)
        {
            _translationRepository = translationRepository;
        }

        public async Task<IReadOnlyCollection<Translation>> Handle(CreateTranslationCommand request, CancellationToken cancellationToken)
        {
            await _translationRepository.AddManyAsync(request.Translations);

            return request.Translations;
        }
    }
}
