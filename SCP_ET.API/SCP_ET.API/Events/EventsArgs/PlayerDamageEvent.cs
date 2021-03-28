using SCP_ET.API.Interfaces;
using System;

namespace SCP_ET.API.Events.EventsArgs
{
    public class PlayerDamageEvent : EventArgs
    {
        public IEntity Victim { get; set; }

        public IEntity Attacker { get; set; }
    }
}
