using CoolTranslator.Core.Abstract;

namespace CoolTranslator.Core.Concrete
{
    public sealed class NoAvailableLanguagesTranlastor : ITranslator
    {
        public string LanguageCode => "None";

        public string Translate(string userInput)
        {
            return "Cannot find proper translation";
        }
    }
}