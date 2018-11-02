using System.Net;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Http;
using CoolTranslator.Core.Concrete;
using CoolTranslator.RestApi.Models;
using CoolTranslator.RestApi.Filters;
using System.Configuration;
using CoolTranslator.Core.Abstract;

namespace CoolTranslator.RestApi.Controllers
{
    public class TranslatorController : ApiController
    {
        private IPathMapper _mapper;

        public TranslatorController(IPathMapper mapper) : base()
        {
            _mapper = mapper;
        }

        [ValidateModel]
        public TranslationResponse Get([FromUri]TranslationRequest request)
        {
            var plugins = PluginLoader.Load(_mapper.MapPath(ConfigurationManager.AppSettings["PluginsDirectory"]));
            var engine = new TranslatorEngine(plugins, ConfigurationManager.AppSettings["DefaultLanguage"]);
            var plugin = engine[request.Language];
            var response = new TranslationResponse();
            
            if (plugin is NoAvailableLanguagesTranlastor)
            {
                response.Code = HttpStatusCode.NotFound;
            }
            else
            {
                response.Code = HttpStatusCode.OK;
            }
            response.Translation = plugin.Translate(request.Text);
            response.Language = plugin.LanguageCode;

            return response;
        }
    }
}
