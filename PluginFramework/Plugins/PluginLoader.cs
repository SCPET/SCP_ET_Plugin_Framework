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
        public static Dictionary<string, Plugin<IConfig>> loadedPlugins = new Dictionary<string, Plugin<IConfig>>();
        
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
                        if (type.IsSubclassOf(typeof(Plugin)) && type != typeof(Plugin))
                        {
                            var plugin = (Plugin<IConfig>) Activator.CreateInstance(type);
                            
                            plugin.Config = ConfigManager.AddConfig(plugin.Name, Activator.CreateInstance(plugin.Config.GetType()));
                            plugin.OnEnabled();
                            
                            loadedPlugins.Add(fileName, plugin);
                            
                            Logger.Info("PS", string.Concat(new object[]
                            {
                                "Plugin ",
                                fileName,
                                " loaded. (",
                                index,
                                "/",
                                count,
                                ")"
                            }));
                        }
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
    }
}