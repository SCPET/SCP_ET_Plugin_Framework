using SCP_ET.API.Classes;
using System;

namespace SCP_ET.API.Events.EventsArgs
{
    public class PlayerLeaveEvent : EventArgs
    {
        public Player Player { get; set; }
    }
}
