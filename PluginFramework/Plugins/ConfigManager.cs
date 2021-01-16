using System;
using System.IO;
using Newtonsoft.Json;
using PluginFramework.API.Interfaces;
using UnityEngine;

namespace PluginFramework.Plugins
{
    internal static class ConfigManager
    {
        internal static Config AddConfig(string name, object configType)
        {
            Config config;
            
            var str = Path.Combine(Application.dataPath, "../settings/plugins/"+name.Replace(".", "").Replace("/", "").Replace("\\", "")+".json");
            if (!File.Exists(str))
            {
                if (!Directory.Exists(Path.GetDirectoryName(str))) Directory.CreateDirectory(Path.GetDirectoryName(str));
                File.WriteAllText(str, JsonConvert.SerializeObject(configType, Formatting.Indented));
            }
            try
            {
                config = (Config) JsonConvert.DeserializeObject(File.ReadAllText(str));
            }
            catch (NullReferenceException)
            {
                File.WriteAllText(str, JsonConvert.SerializeObject(configType, Formatting.Indented));
                config = (Config) configType;
            }
            catch (JsonException)
            {
                File.Move(str, Path.Combine(Application.dataPath, "../settings/plugins/INVALID-"+name.Replace(".", "").Replace("/", "").Replace("\\", "")+".json"));
                File.WriteAllText(str, JsonConvert.SerializeObject(configType, Formatting.Indented));
                config = (Config) configType;
            }

            return config;
        }
    }
}