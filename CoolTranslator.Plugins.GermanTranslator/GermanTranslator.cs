using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoolTranslator.Core.Abstract;

namespace CoolTranslator.Plugins.GermanTranslator
{
    public class GermanTranslator : ITranslator
    {
        private readonly Dictionary<string, string> _vocabulary;

        public string LanguageCode => "De";

        public GermanTranslator()
        {
            _vocabulary = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
                          {
                              {"hallo", "привет"},
                              {"Welt", "мир"},
                              {"alle", "все"},
                              {"sind", "находятся"},
                              {"Basis", "базы"},
                              {"gehört", "принадлежат"},
                              {"zu", "к"},
                              {"uns", "нам"},
                              {"ihre", "ваши"}
                          };
        }

        public string Translate(string userInput)
        {
            var words = userInput.Split(new[] { " ", ",", ".", "!", "?", ":", ";", "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToLower());
            var translation = new StringBuilder();
            foreach (var word in words)
            {
                if (_vocabulary.ContainsKey(word))
                {
                    translation.Append(_vocabulary[word]);
                }
                else
                {
                    translation.Append(word);
                }
                translation.Append(" ");
            }

            return translation.ToString().Trim();
        }
    }
}
