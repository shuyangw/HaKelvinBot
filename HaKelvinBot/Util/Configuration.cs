using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using YamlDotNet.RepresentationModel;

namespace HaKelvinBot.Util
{
    public class Configuration
    {
        private static Dictionary<string, string> Values { get; set; }

        public static void Load(string fileName)
        {
            Logger.Info("Ingesting config file...");
            string wantedPath = AppContext.BaseDirectory;
            if (!File.Exists(Path.Combine(wantedPath, fileName)))
                return;
            Values = new Dictionary<string, string>();

            using (StreamReader sr = new StreamReader(fileName))
            {
                var yaml = new YamlStream();
                yaml.Load(sr);

                var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
                var initMapping = (YamlMappingNode)mapping.Children[new YamlScalarNode("init")];

                foreach(var item in initMapping)
                    Values.Add(item.Key.ToString().ToLowerInvariant(), item.Value.ToString());
            }
            Logger.Info("Successfully loaded config file!");
        }

        public static string Get(string key)
        {
            key = key.ToLowerInvariant();
            if (Values.ContainsKey(key))
                return Values[key];
            else
                return null;
        }
    }
}
