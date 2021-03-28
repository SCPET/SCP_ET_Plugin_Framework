
using SCP_ET.API.Classes;
using System;

namespace SCP_ET.API.Events.EventsArgs
{
    public class CleanRoomActivateEvent : EventArgs
    {
        public Player Player { get; set; }
        public object Controller { get; set; }
        public object Trigger { get; set; }
    }
}
