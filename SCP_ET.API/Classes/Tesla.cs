using SCP_ET.API.Enums;
using SCP_ET.API.Extensions;
using SCP_ET.API.Interfaces;
using SCP_ET.API.Interfaces.EventsArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_ET.API.Classes
{
    public abstract class Tesla : IEntity
    {
        public event PluginSystem.CustomEventHandler<EntityTakeDamageEvent> EntityTakeDamage;
        public event PluginSystem.CustomEventHandler<EntityKillEvent> EntityKill;
        public void OnEntityTakeDamage(EntityTakeDamageEvent ev) => EntityTakeDamage.InvokeSafely(ev);
        public void OnEntityKill(EntityKillEvent ev) => EntityKill.InvokeSafely(ev);

        public abstract DimensionType CurrentDimension { get; set; }
        public static List<Tesla> Teslas { get; set; } = new List<Tesla>();
        public abstract bool IsPlayer { get; }
        public abstract bool IsScp { get; }
        public abstract bool IsNpc { get; }
        public abstract bool IsValidTarget { get; }
        public abstract object TeslaController { get; }
        public abstract object GameObject { get; }
        public abstract void TakeDamage(float dmgAmount, IEntity damager, DeathTypes deathType);
        public abstract void KillEntity();
        public abstract void KillEntity(DeathTypes deathType, IEntity killer);
        public abstract void OnDestroy();
        public abstract Vector Position { get; set; }
        public abstract Vector Rotation { get; set; }
    }
}
