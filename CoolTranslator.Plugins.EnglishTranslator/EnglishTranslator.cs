using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using CoolTranslator.Core.Abstract;

namespace CoolTranslator.Plugins.EnglishTranslator
{
    public class EnglishTranslator : ITranslator
    {
        private Dictionary<string, string> _vocabulary;

        public EnglishTranslator()
        {
            _vocabulary = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            LoadDictionary();
        }

        public string LanguageCode => "En";

        public string Translate(string userInput)
        {
            var words = userInput.Split(new[] {" ", ",", ".", "!", "?", ":", ";", "\n", "\r\n"}, StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToLower());
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

        private void LoadDictionary()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string data;
            using (var stream = assembly.GetManifestResourceStream("CoolTranslator.Plugins.EnglishTranslator.Vocabulary.xml"))
            using (var reader = new StreamReader(stream))
            {
                data = reader.ReadToEnd();
            }
            
            var xml = new XmlDocument();
            xml.LoadXml(data);
            var words = xml.SelectNodes("data/word");

            if (words == null)
            {
                return;
            }

            foreach (XmlNode word in words)
            {
                if (word.Attributes != null)
                {
                    _vocabulary.Add(word.Attributes["key"].Value, word.InnerText);
                }
            }
        }
    }
}
