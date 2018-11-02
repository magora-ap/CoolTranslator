using System.Collections.Generic;
using System.Linq;
using CoolTranslator.Core.Abstract;

namespace CoolTranslator.Core.Concrete
{
    public class TranslatorEngine
    {
        private readonly IEnumerable<ITranslator> _plugins;
        private readonly string _defaultLanguage;

        public TranslatorEngine(IEnumerable<ITranslator> plugins, string defaultLanguage = null)
        {
            _plugins = plugins;
            _defaultLanguage = defaultLanguage;
        }

        public ITranslator this[string name]
        {
            get
            {
                if (_plugins == null || string.IsNullOrWhiteSpace(name))
                {
                    return new NoAvailableLanguagesTranlastor();
                }
                var plugin = _plugins.FirstOrDefault(x => x.LanguageCode.ToLower() == name.ToLower());

                if (plugin != null)
                {
                    return plugin;
                }

                if (!string.IsNullOrWhiteSpace(_defaultLanguage))
                { 
                    var defaultPlugin = _plugins.FirstOrDefault(x => x.LanguageCode.ToLower() == _defaultLanguage.ToLower());
                    if (defaultPlugin != null)
                    {
                        return defaultPlugin;
                    }
                }

                return new NoAvailableLanguagesTranlastor();
            }
        }
    }
}