using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_ET.API.Enums
{
    public enum PlayerEventType
    {
        OnPlayerDeath,
        OnPlayerPreJoin,
        OnPlayerJoinFinal,
        OnPlayerLeave,
        OnPlayerDamage,
        OnPlayerEffect,
        OnPlayerAuth,
        OnPlayerClassChange,
        OnItemPickup,
        OnItemDrop,
        OnCleanRoomActivate,
    }
}
