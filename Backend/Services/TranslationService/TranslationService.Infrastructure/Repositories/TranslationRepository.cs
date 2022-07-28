using BuildingBlocks.Mongo.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationService.Core.Aggregates;

namespace TranslationService.Infrastructure.Repositories
{
    public class TranslationRepository : MongoRepository<Translation>, ITranslationRepository
    {
        public TranslationRepository(IConfiguration configuration) 
            : base(configuration)
        {

        }
    }
}
