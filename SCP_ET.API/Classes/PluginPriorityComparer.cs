using SCP_ET.API.Interfaces;
using System.Collections.Generic;

namespace SCP_ET.API.Classes
{
    public sealed class PluginPriorityComparer : IComparer<IPlugin<IConfig>>
    {
        public static readonly PluginPriorityComparer Instance = new PluginPriorityComparer();
        public int Compare(IPlugin<IConfig> x, IPlugin<IConfig> y)
        {
            var value = y.Priority.CompareTo(x.Priority);
            if (value == 0)
                value = x.GetHashCode().CompareTo(y.GetHashCode());
            return value == 0 ? 1 : value;
        }
    }
}