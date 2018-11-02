using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CoolTranslator.Core.Abstract;

namespace CoolTranslator.Core.Concrete
{
    public static class PluginLoader
    {
        public static List<ITranslator> Load(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException();
            }

            var pluginDlls = Directory.GetFiles(path, "*.dll");
            ICollection<Assembly> assemblies = new List<Assembly>(pluginDlls.Length);
            foreach (var dllFile in pluginDlls)
            {
                var an = AssemblyName.GetAssemblyName(dllFile);
                var assembly = Assembly.Load(an);
                assemblies.Add(assembly);
            }

            Type pluginType = typeof(ITranslator);
            ICollection<Type> pluginTypes = new List<Type>();
            foreach (var assembly in assemblies)
            {
                if (assembly != null)
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (!type.IsInterface && !type.IsAbstract && type.GetInterface(pluginType.FullName) != null)
                        {
                            pluginTypes.Add(type);
                        }
                    }
                }
            }

            var plugins = new List<ITranslator>(pluginTypes.Count);
            foreach (var type in pluginTypes)
            {
                var plugin = (ITranslator)Activator.CreateInstance(type);
                plugins.Add(plugin);
            }

            return plugins;
        }
    }
}