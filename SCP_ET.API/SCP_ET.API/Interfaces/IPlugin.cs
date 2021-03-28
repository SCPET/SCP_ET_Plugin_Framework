using SCP_ET.API.Enums;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SCP_ET.API.Interfaces
{
    public interface IPlugin<out TConfig> : IComparable<IPlugin<IConfig>>
        where TConfig : IConfig
    {
        string PluginDirectory { get; set; }
        Assembly Assembly { get; }
        string Name { get; }
        string Author { get; }
        PluginPriority Priority { get; }
        Version Version { get; }
        TConfig Config { get; }
        void OnEnabled();
        void OnDisabled();
    }
}