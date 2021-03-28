using SCP_ET.API.Classes;
using System;

namespace SCP_ET.API.Events.EventsArgs
{
    public class PlayerJoinFinalEvent : EventArgs
    {
        public Player Player { get; set; }
    }
}
