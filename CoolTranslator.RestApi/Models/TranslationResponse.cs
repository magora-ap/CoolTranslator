using System.Net;

namespace CoolTranslator.RestApi.Models
{
    public class TranslationResponse
    {
        public HttpStatusCode Code { get; set; }

        public string Translation { get; set; }

        public string Language { get; set; }
    }
}