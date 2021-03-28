using SCP_ET.API.Events.EventsArgs;
using SCP_ET.API.Extensions;
using SCP_ET.API.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SCP_ET.API.PluginSystem;

namespace SCP_ET.API.Events
{
    public class EventManager
    {


        #region Player Events

        public static event CustomEventHandler<PlayerDeathEvent> PlayerDeath;
        public static void OnPlayerDeath(PlayerDeathEvent ev) => PlayerDeath.InvokeSafely(ev);

        public static event CustomEventHandler<PlayerPreJoinEvent> PlayerPreJoin;
        public static void OnPlayerPreJoin(PlayerPreJoinEvent ev) => PlayerPreJoin.InvokeSafely(ev);

        public static event CustomEventHandler<PlayerJoinFinalEvent> PlayerJoinFinal;
        public static void OnPlayerJoinFinal(PlayerJoinFinalEvent ev) => PlayerJoinFinal.InvokeSafely(ev);

        public static event CustomEventHandler<PlayerLeaveEvent> PlayerLeave;
        public static void OnPlayerLeave(PlayerLeaveEvent ev) => PlayerLeave.InvokeSafely(ev);

        public static event CustomEventHandler<PlayerDamageEvent> PlayerDamage;
        public static void OnPlayerDamage(PlayerDamageEvent ev) => PlayerDamage.InvokeSafely(ev);

        public static event CustomEventHandler<PlayerEffectEvent> PlayerEffect;
        public static void OnPlayerEffect(PlayerEffectEvent ev) => PlayerEffect.InvokeSafely(ev);

        public static event CustomEventHandler<PlayerAuthEvent> PlayerAuth;
        public static void OnPlayerAuth(PlayerAuthEvent ev) => PlayerAuth.InvokeSafely(ev);

        public static event CustomEventHandler<PlayerClassChangeEvent> PlayerClassChange;
        public static void OnPlayerClassChange(PlayerClassChangeEvent ev) => PlayerClassChange.InvokeSafely(ev);

        public static event CustomEventHandler<ItemPickupEvent> ItemPickup;
        public static void OnItemPickup(ItemPickupEvent ev) => ItemPickup.InvokeSafely(ev);

        public static event CustomEventHandler<ItemDropEvent> ItemDrop;
        public static void OnItemDrop(ItemDropEvent ev) => ItemDrop.InvokeSafely(ev);

        public static event CustomEventHandler<CleanRoomActivateEvent> CleanRoomActivate;
        public static void OnCleanRoomActivate(CleanRoomActivateEvent ev) => CleanRoomActivate.InvokeSafely(ev);

        public static event CustomEventHandler<PlayerMissionModifiedEvent> PlayerMissionModified;
        public static void OnPlayerMissionModified(PlayerMissionModifiedEvent ev) => PlayerMissionModified.InvokeSafely(ev);
        #endregion

        #region World Events
        public static event CustomEventHandler<RoundEndEvent> RoundEnd;
        public static void OnRoundEnd(RoundEndEvent ev) => RoundEnd.InvokeSafely(ev);

        public static event CustomEventHandler<RoundRestartEvent> RoundRestart;
        public static void OnRoundRestart(RoundRestartEvent ev) => RoundRestart.InvokeSafely(ev);

        public static event CustomEventHandler<RoundStartEvent> RoundStart;
        public static void OnRoundStart(RoundStartEvent ev) => RoundStart.InvokeSafely(ev);
        #endregion
    }
}
