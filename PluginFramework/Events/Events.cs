using System.Reflection;

namespace PluginFramework.Events
{
    public static class Events
    {
        public static EventInfo GetEvent(EventType eventType)
        {
            return typeof(Events).GetEvent(eventType.ToString());
        }
    }
}