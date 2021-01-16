using System;
using System.IO;
using Newtonsoft.Json;
using PluginFramework.API.Interfaces;
using UnityEngine;

namespace PluginFramework.Plugins
{
    internal static class ConfigManager
    {
        internal static IConfig AddConfig(string name, object configType)
        {
            IConfig config;
            
            var str = Path.Combine(Application.dataPath, "../settings/plugins/"+name.Replace(".", "").Replace("/", "").Replace("\\", "")+".json");
            if (!File.Exists(str))
            {
                if (!Directory.Exists(Path.GetDirectoryName(str))) Directory.CreateDirectory(Path.GetDirectoryName(str));
                File.WriteAllText(str, JsonConvert.SerializeObject(configType, Formatting.Indented));
            }
            try
            {
                config = (IConfig) JsonConvert.DeserializeObject(File.ReadAllText(str));
            }
            catch (NullReferenceException)
            {
                File.WriteAllText(str, JsonConvert.SerializeObject(configType, Formatting.Indented));
                config = (IConfig) configType;
            }
            catch (JsonException)
            {
                File.Move(str, Path.Combine(Application.dataPath, "../settings/plugins/INVALID-"+name.Replace(".", "").Replace("/", "").Replace("\\", "")+".json"));
                File.WriteAllText(str, JsonConvert.SerializeObject(configType, Formatting.Indented));
                config = (IConfig) configType;
            }

            return config;
        }
    }
}