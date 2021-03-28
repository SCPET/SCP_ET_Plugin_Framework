using SCP_ET.API.Events.EventsArgs;
using SCP_ET.API.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_ET.API.Events
{
    public class EventManager
    {


        #region Player Events

        public delegate void PlayerDeath(PlayerDeathEvent ev);
        public static event PlayerDeath PlayerDeathEvent;

        public delegate void PlayerPreJoin(PlayerPreJoinEvent ev);
        public static event PlayerPreJoin PlayerPreJoinEvent;

        public delegate void PlayerJoinFinal(PlayerJoinFinalEvent ev);
        public static event PlayerJoinFinal PlayerJoinFinalEvent;

        public delegate void PlayerLeave(PlayerLeaveEvent ev);
        public static event PlayerLeave PlayerLeaveEvent;

        public delegate void PlayerDamage(PlayerDamageEvent ev);
        public static event PlayerDamage PlayerDamageEvent;

        public delegate void PlayerEffect(PlayerEffectEvent ev);
        public static event PlayerEffect PlayerEffectEvent;

        public delegate void PlayerAuth(PlayerAuthEvent ev);
        public static event PlayerAuth PlayerAuthEvent;

        public delegate void PlayerClassChange(PlayerClassChangeEvent ev);
        public static event PlayerClassChange PlayerClassChangeEvent;

        public delegate void ItemPickup(ItemPickupEvent ev);
        public static event ItemPickup ItemPickupEvent;

        public delegate void ItemDrop(ItemDropEvent ev);
        public static event ItemDrop ItemDropEvent;

        public delegate void CleanRoomActivate(CleanRoomActivateEvent ev);
        public static event CleanRoomActivate CleanRoomActivateEvent;

        public delegate void PlayerMissionModified(PlayerMissionModifiedEvent ev);
        public static event PlayerMissionModified PlayerMissionModifiedEvent;
        #endregion

        #region World Events

        public delegate void RoundEnd(RoundEndEvent ev);
        public static event RoundEnd RoundEndEvent;

        public delegate void RoundStart();
        public static event RoundStart RoundStartEvent;

        public delegate void RoundRestart();
        public static event RoundRestart RoundRestartEvent;

        #endregion
    }
}
