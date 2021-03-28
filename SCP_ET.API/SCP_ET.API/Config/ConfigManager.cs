using Newtonsoft.Json;
using SCP_ET.API.Interfaces;
using System;
using System.IO;

namespace SCP_ET.API.Config
{
    internal static class ConfigManager
    {
        internal static IConfig InitConfig(IPlugin<IConfig> plugin)
        { 
            File.WriteAllText(Path.Combine(plugin.PluginDirectory, "config.json"), JsonConvert.SerializeObject(plugin.Config, Formatting.Indented));
            IConfig cfg = null;
            try
            {
                cfg = (IConfig)JsonConvert.DeserializeObject(File.ReadAllText(Path.Combine(plugin.PluginDirectory, "config.json")), plugin.Config.GetType());
            }
            catch(JsonException ex)
            {
                PluginSystem.Logger.Error("PS", $"{plugin.Name} configs could not be loaded, some of them are in a wrong format, default configs will be loaded instead! {ex}");
                return plugin.Config;
            }
            return cfg;
        }
    }
}