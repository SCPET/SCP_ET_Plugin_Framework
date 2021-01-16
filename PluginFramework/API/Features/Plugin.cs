using System;
using System.Reflection;
using PluginFramework.API.Interfaces;

namespace PluginFramework.API.Features
{
    /// <summary>
    /// The plugin class.
    /// </summary>
    /// <typeparam name="T">The plugin's config.</typeparam>
    public class Plugin<T> where T : Config, new()
    {
        /// <summary>
        /// The plugin's assembly.
        /// </summary>
        public Assembly Assembly { get; internal set; }
        
        /// <summary>
        /// The plugin's version.
        /// </summary>
        public Version Version { get; internal set; }
        
        /// <summary>
        /// The plugin's author.
        /// </summary>
        public string Author { get; internal set; }
        
        /// <summary>
        /// The plugin's name.
        /// </summary>
        public string Name { get; internal set; }
        
        /// <summary>
        /// The plugin's config.
        /// </summary>
        public T Config { get; internal set; }

        public void OnEnabled()
        {
            Log.Info(Name+" (version "+Version+") by "+Author+" has been enabled!");
        }

        public void OnDisabled()
        {
            Log.Info(Name+" (version "+Version+") by "+Author+" has been disabled!");
        }
    }
}