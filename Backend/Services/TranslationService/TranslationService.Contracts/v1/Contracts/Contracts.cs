using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslationService.Contracts.v1.Contracts
{
    public record CreateTranslationRequest([Required] string Field, [Required] Dictionary<string, string> Multilinguals);


    public record RemovteTranslationRequest([Required] string Field, [Required] Dictionary<string, string> Multilinguals);

    public record CreateTranslationResponse(string Key, Dictionary<string, string> Multilinguals);

    public record AddMultilingualRequest(string Key, string Locale, string Value);
    public record RemoveMultilingualRequest(string Key, string Locale);
    public record ChangeMultilingualRequest(string Key, string Locale, string Value);
}
