using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SCP_ET.API.Logging;
using SCP_ET.API.Classes;
using SCP_ET.API.Attributes;
using SCP_ET.API.Enums;
using SCP_ET.API.Commands;
using SCP_ET.API.Interfaces;
using System.Linq;
using SCP_ET.API.Config;
using SCP_ET.API.Extensions;
using SCP_ET.API.Events;
using SCP_ET.API.Events.EventsArgs;

namespace SCP_ET.API
{
    public class PluginSystem
    {
        #region Misc
        public PluginSystem()
        {
            singleton = this;
        }

        public delegate void CustomEventHandler<TEventArgs>(TEventArgs ev)
            where TEventArgs : System.EventArgs;

        public delegate void CustomEventHandler();

        public static Dictionary<string, List<MethodInfo>> pluginsMethods = new Dictionary<string, List<MethodInfo>>();

        public static Dictionary<Assembly, string> Locations { get; set; } = new Dictionary<Assembly, string>();
        public static List<Assembly> Dependencies { get; set; } = new List<Assembly>();
        public static SortedSet<IPlugin<IConfig>> Plugins { get; set; } = new SortedSet<IPlugin<IConfig>>(PluginPriorityComparer.Instance);

        private static Logger logger;
        public static Logger Logger
        {
            get { return logger; }
            set
            {
                if (logger == null)
                {
                    logger = value;
                }
            }
        }

        private static EventManager eventmanager;

        public static EventManager EventManager
        {
            get { return eventmanager; }
            set
            {
                if (eventmanager == null)
                {
                    eventmanager = value;
                }
            }
        }

        private static PluginSystem singleton;
        public static PluginSystem Manager
        {
            get
            {
                return singleton;
            }
        }

        public static string PluginsDirectory;
        public static string DependenciesDirectory;

        public void LoadPlugins(string pluginsDirectory)
        {
            Locations = new Dictionary<Assembly, string>();
            Plugins = new SortedSet<IPlugin<IConfig>>(PluginPriorityComparer.Instance);
            Dependencies = new List<Assembly>();

            pluginsMethods = new Dictionary<string, List<MethodInfo>>();

            PluginsDirectory = pluginsDirectory;
            DependenciesDirectory = Path.Combine(PluginsDirectory, "dependencies");
            string[] dependencies = Directory.GetFiles(DependenciesDirectory);
            string[] plugins = Directory.GetFiles(PluginsDirectory);
            int index = 1;
            int count = dependencies.Length;
            Log.Info($"Loading {count} dependencies.");
            foreach (var file in dependencies)
            {
                LoadDependency(index, count, file);
                index++;
            }
            index = 1;
            count = plugins.Length;
            Log.Info($"Loading {count} plugins.");
            foreach (var file in plugins)
            {
                LoadPlugin(index, count, file);
                index++;
            }
            index = 1;
            count = Plugins.Count;
            Log.Info($"Enabling {count} plugins.");
            foreach (var pl in Plugins)
            {
                EnablePlugin(index, count, pl);
                index++;
            }
        }

        public static Dictionary<string, List<MethodInfo>> plugins = new Dictionary<string, List<MethodInfo>>();

        #endregion


        #region Loader

        public Assembly LoadAssembly(string path)
        {
            try
            {
                return Assembly.Load(File.ReadAllBytes(path));
            }
            catch (Exception exception)
            {
                Log.Error($"Error while loading an assembly at {path}! {exception}");
            }
            return null;
        }
        public IPlugin<IConfig> CreatePlugin(Assembly assembly)
        {
            try
            {
                foreach (Type type in assembly.GetTypes().Where(type => !type.IsAbstract && !type.IsInterface))
                {
                    if (!type.BaseType.IsGenericType || type.BaseType.GetGenericTypeDefinition() != typeof(Plugin<>))
                        continue;

                    Log.Debug($"Loading type {type.FullName}", true);

                    IPlugin<IConfig> plugin = null;

                    var constructor = type.GetConstructor(Type.EmptyTypes);
                    if (constructor != null)
                    {
                        Log.Debug("Public default constructor found, creating instance...", true);

                        plugin = constructor.Invoke(null) as IPlugin<IConfig>;
                    }
                    else
                    {
                        Log.Debug($"Constructor wasn't found, searching for a property with the {type.FullName} type...", true);

                        var value = Array.Find(type.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public), property => property.PropertyType == type)?.GetValue(null);

                        if (value != null)
                            plugin = value as IPlugin<IConfig>;
                    }

                    if (plugin == null)
                    {
                        Log.Error($"{type.FullName} is a valid plugin, but it cannot be instantiated! It either doesn't have a public default constructor without any arguments or a static property of the {type.FullName} type!");
                        continue;
                    }

                    foreach(Type typ in assembly.GetTypes())
                    {
                        if (typeof(IConsoleCommand).IsAssignableFrom(typ) && typ.GetCustomAttribute<ConsoleCommand>() != null)
                        {
                            IConsoleCommand cmd = Activator.CreateInstance(typ) as IConsoleCommand;
                            ConsoleCommandHandler.RegisterCommand(cmd.Name, cmd);
                        }
                        else if (typeof(IAdminCommand).IsAssignableFrom(typ) && typ.GetCustomAttribute<AdminCommand>() != null)
                        {
                            IAdminCommand cmd = Activator.CreateInstance(type) as IAdminCommand;
                            AdminChatCommandHandler.RegisterCommand(cmd.Name, cmd);
                        }
                        else if (typeof(IChatCommand).IsAssignableFrom(typ) && typ.GetCustomAttribute<ChatCommand>() != null)
                        {
                            IChatCommand cmd = Activator.CreateInstance(type) as IChatCommand;
                            TextChatCommandHandler.RegisterCommand(cmd.Name, cmd);
                        }
                        else if (typeof(IScript).IsAssignableFrom(typ))
                        {
                            List<MethodInfo> methds = new List<MethodInfo>();
                            foreach (var method in typ.GetMethods())
                            {
                                PlayerEvent ev = method.GetCustomAttribute<PlayerEvent>();
                                if (ev != null)
                                {
                                    methds.Add(method);
                                    Logger.Info("Plugin", "Loaded event " + ev.EventType.ToString());
                                    switch (ev.EventType)
                                    {
                                        case PlayerEventType.OnPlayerDeath:
                                            {
                                                EventInfo info = GetType().GetEvent(nameof(PlayerDeathEvent));
                                                info.AddEventHandler(null, Delegate.CreateDelegate(info.EventHandlerType, method));
                                            }
                                            break;
                                        case PlayerEventType.OnPlayerJoinFinal:
                                            {
                                                EventInfo info = GetType().GetEvent(nameof(PlayerJoinFinalEvent));
                                                info.AddEventHandler(null, Delegate.CreateDelegate(info.EventHandlerType, method));
                                            }
                                            break;
                                        case PlayerEventType.OnPlayerLeave:
                                            {
                                                EventInfo info = GetType().GetEvent(nameof(PlayerLeaveEvent));
                                                info.AddEventHandler(null, Delegate.CreateDelegate(info.EventHandlerType, method));
                                            }
                                            break;
                                        case PlayerEventType.OnPlayerPreJoin:
                                            {
                                                EventInfo info = GetType().GetEvent(nameof(PlayerPreJoinEvent));
                                                info.AddEventHandler(null, Delegate.CreateDelegate(info.EventHandlerType, method));
                                            }
                                            break;
                                        case PlayerEventType.OnPlayerDamage:
                                            {
                                                EventInfo info = GetType().GetEvent(nameof(PlayerDamageEvent));
                                                info.AddEventHandler(null, Delegate.CreateDelegate(info.EventHandlerType, method));
                                            }
                                            break;
                                        case PlayerEventType.OnPlayerEffect:
                                            {
                                                EventInfo info = GetType().GetEvent(nameof(PlayerEffectEvent));
                                                info.AddEventHandler(null, Delegate.CreateDelegate(info.EventHandlerType, method));
                                            }
                                            break;
                                        case PlayerEventType.OnPlayerAuth:
                                            {
                                                EventInfo info = GetType().GetEvent(nameof(PlayerAuthEvent));
                                                info.AddEventHandler(null, Delegate.CreateDelegate(info.EventHandlerType, method));
                                            }
                                            break;
                                        case PlayerEventType.OnPlayerClassChange:
                                            {
                                                EventInfo info = GetType().GetEvent(nameof(PlayerClassChangeEvent));
                                                info.AddEventHandler(null, Delegate.CreateDelegate(info.EventHandlerType, method));
                                            }
                                            break;
                                        case PlayerEventType.OnItemPickup:
                                            {
                                                EventInfo info = GetType().GetEvent(nameof(ItemPickupEvent));
                                                info.AddEventHandler(null, Delegate.CreateDelegate(info.EventHandlerType, method));
                                            }
                                            break;
                                        case PlayerEventType.OnItemDrop:
                                            {
                                                EventInfo info = GetType().GetEvent(nameof(ItemDropEvent));
                                                info.AddEventHandler(null, Delegate.CreateDelegate(info.EventHandlerType, method));
                                            }
                                            break;
                                        case PlayerEventType.OnCleanRoomActivate:
                                            {
                                                EventInfo info = GetType().GetEvent(nameof(CleanRoomActivateEvent));
                                                info.AddEventHandler(null, Delegate.CreateDelegate(info.EventHandlerType, method));
                                            }
                                            break;
                                    }
                                }

                                WorldEvent worldEvent = method.GetCustomAttribute<WorldEvent>();
                                if (worldEvent != null)
                                {
                                    methds.Add(method);
                                    Logger.Info("Plugin", "Loaded event " + worldEvent.EventType.ToString());
                                    switch (worldEvent.EventType)
                                    {
                                        case WorldEventType.OnRoundEnd:
                                            {
                                                EventInfo info = GetType().GetEvent(nameof(RoundEndEvent));
                                                info.AddEventHandler(null, Delegate.CreateDelegate(info.EventHandlerType, method));
                                            }
                                            break;
                                        /*case WorldEventType.OnRoundRestart:
                                            {
                                                EventInfo info = GetType().GetEvent(nameof(RoundRestartEvent));
                                                info.AddEventHandler(null, Delegate.CreateDelegate(info.EventHandlerType, method));
                                            }
                                            break;
                                        case WorldEventType.OnRoundStart:
                                            {
                                                EventInfo info = GetType().GetEvent(nameof(RoundStartEvent));
                                                info.AddEventHandler(null, Delegate.CreateDelegate(info.EventHandlerType, method));
                                            }
                                            break;*/
                                    }
                                }
                            }
                            if (pluginsMethods.ContainsKey(assembly.FullName))
                                pluginsMethods[assembly.FullName].AddRange(methds);
                            else
                                pluginsMethods.Add(assembly.FullName, methds);
                        }
                    }
                    plugin.PluginDirectory = Path.Combine(PluginsDirectory, plugin.Name);
                    if (!Directory.Exists(plugin.PluginDirectory))
                        Directory.CreateDirectory(plugin.PluginDirectory);
                    plugin.Config.CopyProperties(ConfigManager.InitConfig(plugin));
                    return plugin;
                }
            }
            catch (Exception exception)
            {
                Log.Error($"Error while initializing plugin {assembly.GetName().Name} (at {assembly.Location})! {exception}");
            }

            return null;
        }

        public void LoadPlugin(int index, int count, string path)
        {
            if (!path.EndsWith(".dll"))
                return;
            if (Locations.ContainsValue(path))
            {
                Log.Info($"Plugin {Path.GetFileNameWithoutExtension(path)} is already loaded.");
                return;
            }
            Assembly a = LoadAssembly(path);
            Locations[a] = path;
            IPlugin<IConfig> plugin = CreatePlugin(a);
            if (plugin == null)
                return;
            Plugins.Add(plugin);
            Log.Info($"Plugin {plugin.Name} loaded. ({index}/{count})");
        }

        public void EnablePlugin(int index, int count, IPlugin<IConfig> plugin)
        {
            plugin.OnEnabled();
            Log.Info($"Plugin {plugin.Name} enabled. ({index}/{count})");
        }

        public void DisablePlugin(int index, int count, IPlugin<IConfig> plugin)
        {
            plugin.OnDisabled();
            Log.Info($"Plugin {plugin.Name} disabled. ({index}/{count})");
        }

        public void LoadDependency(int index, int count, string path)
        {
            if (!path.EndsWith(".dll"))
                return;
            Assembly a = LoadAssembly(path);
            Locations[a] = path;
            Log.Info($"Dependency {a.FullName} loaded. ({index}/{count})");
        }

        #endregion
    }
}
