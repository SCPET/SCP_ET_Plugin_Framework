using SCP_ET.API.Classes;
using SCP_ET.API.Events.EventsArgs;
using SCP_ET.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_ET.API.Events
{
    public class PlayerEvents
    {
        public static void InvokePlayerDeath(IEntity victim, IEntity killer)
        {
            PlayerDeathEvent ev = new PlayerDeathEvent()
            {
                Victim = victim,
                Attacker = killer
            };
            PluginSystem.EventManager.ExecutePlayerDeath(ev);
        }

        public static void InvokePlayerPreJoin(Player player)
        {
            PlayerPreJoinEvent ev = new PlayerPreJoinEvent()
            {
                Player = player
            };
            PluginSystem.EventManager.ExecutePlayerPreJoin(ev);
        }

        public static void InvokePlayerJoinFinal(Player player)
        {
            PlayerJoinFinalEvent ev = new PlayerJoinFinalEvent()
            {
                Player = player
            };
            PluginSystem.EventManager.ExecutePlayerJoinFinal(ev);
        }

        public static void InvokePlayerLeave(Player player)
        {
            PlayerLeaveEvent ev = new PlayerLeaveEvent()
            {
                Player = player
            };
            PluginSystem.EventManager.ExecutePlayerLeave(ev);
        }

        public static void InvokePlayerDamage(IEntity victim, IEntity damager)
        {
            PlayerDamageEvent ev = new PlayerDamageEvent()
            {
                Victim = victim,
                Attacker = damager
            };
            PluginSystem.EventManager.ExecutePlayerDamage(ev);
        }

        public static void InvokePlayerEffect(Player player, object effect)
        {
            PlayerEffectEvent ev = new PlayerEffectEvent()
            {
                Player = player
            };
            PluginSystem.EventManager.ExecutePlayerEffect(ev);
        }

        public static void InvokePlayerAuth(object connection)
        {
            PlayerAuthEvent ev = new PlayerAuthEvent()
            {
                Connection = connection
            };
            PluginSystem.EventManager.ExecutePlayerAuth(ev);
        }

        public static void InvokePlayerClassChange(Player player, int prevClassId, int newClassId)
        {
            PlayerClassChangeEvent ev = new PlayerClassChangeEvent()
            {
                Player = player,
                prevClassId = prevClassId,
                newClassId = newClassId
            };
            PluginSystem.EventManager.ExecutePlayerClassChange(ev);
        }

        public static void InvokeItemPickup(Player player, Item item)
        {
            ItemPickupEvent ev = new ItemPickupEvent()
            {
                Player = player,
                Item = item
            };
            PluginSystem.EventManager.ExecuteItemPickup(ev);
        }

        public static void InvokeItemDrop(Player player, Item item)
        {
            ItemDropEvent ev = new ItemDropEvent()
            {
                Player = player,
                Item = item
            };
            PluginSystem.EventManager.ExecuteItemDrop(ev);
        }

        public static void InvokeCleanRoomActivate(Player player, object controller, object trigger)
        {
            CleanRoomActivateEvent ev = new CleanRoomActivateEvent()
            {
                Player = player,
                Controller = controller,
                Trigger = trigger,
            };
            PluginSystem.EventManager.ExecuteCleanRoomActivate(ev);
        }

        public static void InvokeMissionModified(object mission)
        {
            PlayerMissionModifiedEvent ev = new PlayerMissionModifiedEvent()
            {
                missionInfo = mission
            };
            PluginSystem.EventManager.ExecutePlayerMissionModified(ev);
        }


    }
}
