using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationService.Contracts.v1.Contracts;
using TranslationService.Core.Aggregates;
using TranslationService.Core.ValueObjects;

namespace TranslationService.API.Profiles
{
    public class TranslationProfile : Profile
    {
        public TranslationProfile()
        {
            // requests
            CreateMap<KeyValuePair<string, string>, Multilingual>()
                .ConstructUsing(src => new Multilingual(src.Key, src.Value));

            CreateMap<CreateTranslationRequest, Translation>()
                .ConstructUsing((src, ctx) =>
                {
                    var tenantId = (string) ctx.Items["tenant-id"];
                    var service = (string) ctx.Items["service"];
                    var resource = (string) ctx.Items["resource"];
                    var identifier = (string) ctx.Items["identifier"];

                    var multilinguals = ctx.Mapper.Map<List<Multilingual>>(src.Multilinguals);
                    return new Translation(tenantId, service, resource, identifier, src.Field, multilinguals);
                });


            // responses
            CreateMap<Translation, CreateTranslationResponse>()
                .ForMember(dest => dest.Multilinguals, opts => opts.Ignore());

            CreateMap<Multilingual, KeyValuePair<string, string>>()
                .ConstructUsing((src, ctx) => new KeyValuePair<string, string>(src.Locale, src.Value));
        }
    }
}
