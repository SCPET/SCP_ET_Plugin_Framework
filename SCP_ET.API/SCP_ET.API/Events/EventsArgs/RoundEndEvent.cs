
using System;

namespace SCP_ET.API.Events.EventsArgs
{
    public class RoundEndEvent : EventArgs
    {
        public int EscapedPlayers { get; set; }
        public bool ForceEnded { get; set; }
    }
}
