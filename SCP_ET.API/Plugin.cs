using SCP_ET.API.Enums;
using SCP_ET.API.Interfaces;
using SCP_ET.API.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace SCP_ET.API
{
    public abstract class Plugin<TConfig> : IPlugin<TConfig>
            where TConfig : IConfig, new()
    {
        public Plugin()
        {
            Name = Assembly.GetName().Name;
            Author = Assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company;
            Version = Assembly.GetName().Version;
        }

        public string PluginDirectory { get; set; }
        public Assembly Assembly { get; } = Assembly.GetCallingAssembly();
        public virtual string Name { get; }
        public virtual string Author { get; }
        public virtual PluginPriority Priority { get; }
        public virtual Version Version { get; }
        public TConfig Config { get; } = new TConfig();
        public virtual void OnEnabled()
        {
            Log.Info($"{Name} v{Version.Major}.{Version.Minor}.{Version.Build}, made by {Author}, has been enabled!");
        }

        public virtual void OnDisabled()
        {
            Log.Info($"{Name} has been disabled!");
        }
        public int CompareTo(IPlugin<IConfig> other) => -Priority.CompareTo(other.Priority);
    }
}
