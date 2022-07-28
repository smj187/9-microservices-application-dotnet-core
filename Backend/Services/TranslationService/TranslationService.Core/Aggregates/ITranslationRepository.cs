using BuildingBlocks.Mongo.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationService.Core.Aggregates
{
    public interface ITranslationRepository : IMongoRepository<Translation>
    {

    }
}
