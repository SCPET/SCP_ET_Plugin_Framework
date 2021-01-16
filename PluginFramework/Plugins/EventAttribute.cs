using System;
using PluginFramework.Events;

namespace PluginFramework.Plugins
{
    public class EventAttribute : Attribute
    {
        internal EventType EventType;

        public EventAttribute(EventType eventType)
        {
            EventType = eventType;
        }
    }
}