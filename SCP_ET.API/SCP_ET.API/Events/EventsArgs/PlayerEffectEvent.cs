
using SCP_ET.API.Classes;
using System;

namespace SCP_ET.API.Events.EventsArgs
{
    public class PlayerEffectEvent : EventArgs
    {
        public Player Player { get; set; }
        public object effect { get; set; }
    }
}
