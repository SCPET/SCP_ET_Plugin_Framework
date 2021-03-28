using SCP_ET.API.Enums;
using System.Reflection;

namespace SCP_ET.API.Logging
{
    public static class Log
    {
        public static void Info(object message) => PluginSystem.Logger.Info($"{Assembly.GetCallingAssembly().GetName().Name}", $"{message}");

        public static void Debug(object message, bool canBeSent = true)
        {
            if (canBeSent)
                PluginSystem.Logger.Debug($"{Assembly.GetCallingAssembly().GetName().Name}", $"{message}");
        }

        public static void Warn(object message) => PluginSystem.Logger.Warn($"{Assembly.GetCallingAssembly().GetName().Name}", $"{message}");

        public static void Error(object message) => PluginSystem.Logger.Error($"{Assembly.GetCallingAssembly().GetName().Name}", $"{message}");
    }
}