using SCP_ET.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_ET.API.Interfaces.EventsArgs
{
    public class EntityKillEvent : EventArgs
    {
        public EntityKillEvent(IEntity victim, IEntity attacker, DeathTypes deathType)
        {
            this.Victim = victim;
            this.Attacker = attacker;
            this.DeathType = deathType;
        }

        public IEntity Victim { get; }
        public IEntity Attacker { get; }
        public DeathTypes DeathType { get; set; }
    }
}
