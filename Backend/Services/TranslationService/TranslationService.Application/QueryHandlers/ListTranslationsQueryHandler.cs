using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationService.Application.Queries;
using TranslationService.Core.Aggregates;

namespace TranslationService.Application.QueryHandlers
{
    public class ListTranslationsQueryHandler : IRequestHandler<ListTranslationsQuery, IReadOnlyCollection<Translation>>
    {
        private readonly ITranslationRepository _translationRepository;

        public ListTranslationsQueryHandler(ITranslationRepository translationRepository)
        {
            _translationRepository = translationRepository;
        }

        public async Task<IReadOnlyCollection<Translation>> Handle(ListTranslationsQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Translation>.Filter.In(x => x.Key, request.Keys);
            return await _translationRepository.ListAsync(filter);
        }
    }
}
