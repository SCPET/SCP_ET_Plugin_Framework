using SCP_ET.API.Classes;
using SCP_ET.API.Enums;
using SCP_ET.API.Interfaces.EventsArgs;
using System;
using static SCP_ET.API.PluginSystem;

namespace SCP_ET.API.Interfaces
{
    public interface IEntity : IObject
    {
        bool IsPlayer { get; }
        bool IsScp { get; }
        bool IsNpc { get; }
        bool IsValidTarget { get; }
        void TakeDamage(float dmgAmount, IEntity damager, DeathTypes deathType);
        void KillEntity();
        void KillEntity(DeathTypes deathType, IEntity killer);
        DimensionType CurrentDimension { get; set; }
        event CustomEventHandler<EntityTakeDamageEvent> EntityTakeDamage;
        event CustomEventHandler<EntityKillEvent> EntityKill;
        void OnEntityTakeDamage(EntityTakeDamageEvent ev);
        void OnEntityKill(EntityKillEvent ev);
    }
}
