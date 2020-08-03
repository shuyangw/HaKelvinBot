using HaKelvinBot.Structures;
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
        private static Dictionary<string, string> ConfigValues { get; set; }

        private static readonly string AppPath = AppContext.BaseDirectory;

        public static void LoadConfig(string fileName)
        {
            Logger.Info("Ingesting config file...");
            if (!File.Exists(Path.Combine(AppPath, fileName)))
                return;
            ConfigValues = new Dictionary<string, string>();

            using (StreamReader sr = new StreamReader(fileName))
            {
                var yaml = new YamlStream();
                yaml.Load(sr);

                var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
                var initMapping = (YamlMappingNode)mapping.Children[new YamlScalarNode("init")];

                foreach(var item in initMapping)
                    ConfigValues.Add(item.Key.ToString().ToLowerInvariant(), item.Value.ToString());
            }
            Logger.Info("Successfully loaded config file!");
        }

        public static string GetConfig(string key)
        {
            if (ConfigValues == null || ConfigValues.Count == 0)
            {
                Logger.Error("Config VALUES dictionary was null!");
                return null;
            }

            key = key.ToLowerInvariant();
            if (ConfigValues.ContainsKey(key))
                return ConfigValues[key];
            else
                return null;
        }

        public static void LoadPersistentTasks(string fileName)
        {

        }

        public static void AddPersistentTask(BotTask task, string serverName)
        {
            string fileName = Path.Combine(AppPath, "serverName.json");
            if (!File.Exists(fileName))
                File.Create(fileName);
        
            
        }
    }
}
