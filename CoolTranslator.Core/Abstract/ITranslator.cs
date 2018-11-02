namespace CoolTranslator.Core.Abstract
{
    public interface ITranslator
    {
        string LanguageCode { get; }

        string Translate(string userInput);
    }
}