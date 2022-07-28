using BuildingBlocks.Exceptions.Domain;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationService.Application.Commands;
using TranslationService.Core.Aggregates;

namespace TranslationService.Application.CommandHandlers
{
    public class AddMultilingualCommandHandler : IRequestHandler<AddMultilingualCommand, Translation>
    {
        private readonly ITranslationRepository _translationRepository;

        public AddMultilingualCommandHandler(ITranslationRepository translationRepository)
        {
            _translationRepository = translationRepository;
        }

        public async Task<Translation> Handle(AddMultilingualCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Translation>.Filter.Eq(x => x.Key, request.Key);
            var translation = await _translationRepository.FindAsync(filter);
            if (translation == null)
            {
                throw new AggregateNotFoundException($"key of '{request.Key}' does not exist");
            }

            translation.AddMultilingual(request.Locale, request.Value);

            return await _translationRepository.PatchAsync(translation);
        }
    }
}
