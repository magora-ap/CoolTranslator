using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using CoolTranslator.Core.Concrete;

namespace CoolTranslator.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var pluginsDirectory = Path.Combine(currentDirectory, ConfigurationManager.AppSettings["PluginsDirectory"]);
            var plugins = PluginLoader.Load(pluginsDirectory).ToArray();
            var engine = new TranslatorEngine(plugins);
            Console.WriteLine(engine["De"].Translate("All your base are belong to us"));
            Console.ReadKey(true);
        }
    }
}
