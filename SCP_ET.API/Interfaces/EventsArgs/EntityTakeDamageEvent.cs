using SCP_ET.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_ET.API.Interfaces.EventsArgs
{
    public class EntityTakeDamageEvent : EventArgs
    {
        public EntityTakeDamageEvent(IEntity victim, IEntity damager, float damageAmount, DeathTypes deathType)
        {
            this.Victim = victim;
            this.Damager = damager;
            this.DamageAmount = damageAmount;
            this.DeathType = deathType;
        }

        public IEntity Victim { get; }
        public IEntity Damager { get; }
        public float DamageAmount { get; set; }
        public DeathTypes DeathType { get; set; }
    }
}
