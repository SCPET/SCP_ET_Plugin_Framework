using SCP_ET.API.Classes;
using System;

namespace SCP_ET.API.Events.EventsArgs
{
    public class PlayerClassChangeEvent : EventArgs
    {
        public Player Player { get; set; }
        public int prevClassId { get; set; }
        public int newClassId { get; set; }
    }
}