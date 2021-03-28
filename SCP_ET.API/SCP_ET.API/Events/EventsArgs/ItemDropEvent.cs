using SCP_ET.API.Classes;
using System;

namespace SCP_ET.API.Events.EventsArgs
{
    public class ItemDropEvent : EventArgs
    {
        public Player Player { get; set; }
        public Item Item { get; set; }
    }
}