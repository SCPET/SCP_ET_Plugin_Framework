using SCP_ET.API.Classes;
using SCP_ET.API.Enums;
using SCP_ET.API.Interfaces.EventsArgs;
using System;
using static SCP_ET.API.PluginSystem;

namespace SCP_ET.API.Interfaces
{
    public interface IEntity
    {
        bool IsPlayer { get; }
        bool IsScp { get; }
        bool IsNpc { get; }
        bool IsValidTarget { get; }
        object GameObject { get; }
        Vector Position { get; set; }
        Vector Rotation { get; set; }
        void TakeDamage(float dmgAmount, IEntity damager, DeathTypes deathType);
        void KillEntity();
        void KillEntity(DeathTypes deathType, IEntity killer);
        DimensionType CurrentDimension { get; set; }
        void OnDestroy();
        event CustomEventHandler<EntityTakeDamageEvent> EntityTakeDamage;
        event CustomEventHandler<EntityKillEvent> EntityKill;
        void OnEntityTakeDamage(EntityTakeDamageEvent ev);
        void OnEntityKill(EntityKillEvent ev);
    }
}
