using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using PluginFramework.API.Features;
using PluginFramework.API.Interfaces;

namespace PluginFramework.Plugins
{
    public static class PluginLoader
    {
        public static List<string> loadedDependencies = new List<string>();
        public static Dictionary<string, Plugin<Config>> loadedPlugins = new Dictionary<string, Plugin<Config>>();
        
        private static List<Plugin<Config>> tempPlugins = new List<Plugin<Config>>();
        
        public static void LoadPlugins(string pluginsDirectory)
        {
            var files = Directory.GetFiles(Path.Combine(pluginsDirectory, "dependencies"));
            var files2 = Directory.GetFiles(pluginsDirectory);
            
            Logger.Info("PS", "Loading " + files.Length + " dependencies.");
            
            var num = 1;
            foreach (var path in files)
            {
                LoadDependency(num, files.Length, path);
                num++;
            }

            Logger.Info("PS", "Loading " + files2.Length + " plugins.");
            
            num = 1;
            foreach (var path2 in files2)
            {
                LoadPlugin(num, files2.Length, path2);
                num++;
            }

            EnablePlugins();
        }

        private static void LoadDependency(int index, int count, string path)
        {
            if (!path.EndsWith(".dll")) return;
            
            var fileName = Path.GetFileName(path);
            if (!loadedDependencies.Contains(fileName))
            {
                loadedDependencies.Add(fileName);
                Logger.Info("PS", string.Concat(new object[]
                {
                    "Dependency ",
                    fileName,
                    " loaded. (",
                    index,
                    "/",
                    count,
                    ")"
                }));
                Assembly.Load(File.ReadAllBytes(path));
                return;
            }
            
            Logger.Warn("PS", string.Concat(new object[]
            {
                "Dependency ",
                fileName,
                " is already loaded. (",
                index,
                "/",
                count,
                ")"
            }));
        }

        private static void LoadPlugin(int index, int count, string path)
        {
            if (!path.EndsWith(".dll"))
            {
                return;
            }

            var fileName = Path.GetFileName(path);
            if (!loadedPlugins.ContainsKey(fileName))
            {
                var assembly = Assembly.Load(File.ReadAllBytes(path));

                try
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (!type.IsSubclassOf(typeof(Plugin)) || type == typeof(Plugin)) continue;
                        
                        var plugin = (Plugin<Config>) Activator.CreateInstance(type);
                            
                        plugin.Config = ConfigManager.AddConfig(plugin.Name, Activator.CreateInstance(plugin.Config.GetType()));
                        plugin.File = fileName;
                        plugin.Assembly = assembly;
                            
                        tempPlugins.Add(plugin);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("PS", string.Concat(new object[]
                    {
                        "Loading plugin ",
                        fileName,
                        " failed. (",
                        index,
                        "/",
                        count,
                        ")",
                        Environment.NewLine,
                        ex.ToString()
                    }));
                }
            }
            else
            {
                Logger.Warn("PS", string.Concat(new object[]
                {
                    "Plugin ",
                    fileName,
                    " is already loaded. (",
                    index,
                    "/",
                    count,
                    ")"
                }));
            }
        }

        private static void EnablePlugins()
        {
            tempPlugins.Sort();
            
            for (var i = 0; i < tempPlugins.Count; i++)
            {
                var plugin = tempPlugins[i];
                
                loadedPlugins.Add(plugin.File, plugin);
                
                plugin.Enable();

                Logger.Info("PS", string.Concat(new object[]
                {
                    "Plugin ",
                    plugin.File,
                    " loaded. (",
                    i+1,
                    "/",
                    tempPlugins.Count,
                    ")"
                }));
            }

            tempPlugins.Clear();
        }

        public static void Enable(this Plugin<Config> plugin)
        {
            foreach (var type in plugin.Assembly.GetTypes())
            {
                foreach (var methodInfo in type.GetMethods())
                {
                    var customAttribute = methodInfo.GetCustomAttribute<EventAttribute>();
                    if (customAttribute == null) continue;
                    
                    var eventInfo = Events.Events.GetEvent(customAttribute.EventType);
                    eventInfo.AddEventHandler(null, Delegate.CreateDelegate(eventInfo.EventHandlerType, methodInfo));
                }
            }

            plugin.OnEnabled();
        }

        public static void Disable(this Plugin<Config> plugin)
        {
            foreach (var type in plugin.Assembly.GetTypes())
            {
                foreach (var methodInfo in type.GetMethods())
                {
                    var customAttribute = methodInfo.GetCustomAttribute<EventAttribute>();
                    if (customAttribute == null) continue;
                    
                    var eventInfo = Events.Events.GetEvent(customAttribute.EventType);
                    eventInfo.RemoveEventHandler(null, Delegate.CreateDelegate(eventInfo.EventHandlerType, methodInfo));
                }
            }

            plugin.OnDisabled();
        }
    }
}