using System;
using System.Reflection;
using PluginFramework.Events.EventArgs;

namespace PluginFramework.Events
{
    internal static class Events
    {
        internal static EventInfo GetEvent(EventType eventType)
        {
            return typeof(Events).GetEvent(eventType.ToString());
        }

        internal static event System.EventHandler<CleanRoomActivateEventArgs> CleanRoomActivateEvent;
        internal static event EventHandler<DoorToggleEventArgs> DoorToggleEvent;
        internal static event EventHandler<ItemDropEventArgs> ItemDropEvent;
        internal static event EventHandler<ItemPickupEventArgs> ItemPickupEvent;
        internal static event EventHandler<PlayerAuthEventArgs> PlayerAuthEvent;
        internal static event EventHandler<PlayerClassChangeEventArgs> PlayerClassChangeEvent;
        internal static event EventHandler<PlayerDamageEventArgs> PlayerDamageEvent;
        internal static event EventHandler<PlayerDeathEventArgs> PlayerDeathEvent;
        internal static event EventHandler<PlayerEffectEventArgs> PlayerEffectEvent;
        internal static event EventHandler<PlayerJoinEventArgs> PlayerJoinEvent;
        internal static event EventHandler<PlayerLeaveEventArgs> PlayerLeaveEvent;
        internal static event EventHandler<PlayerPreJoinEventArgs> PlayerPreJoinEvent;
        internal static event EventHandler<RoundEndEventArgs> RoundEndEvent;
    }
}