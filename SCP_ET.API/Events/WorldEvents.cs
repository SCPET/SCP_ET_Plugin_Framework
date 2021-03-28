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
    public class WorldEvents
    {
        public static void InvokeDoorOpen(IEntity ent, Door door)
        {
            DoorToggleEvent ev = new DoorToggleEvent()
            {
                Door = door,
                Entity = ent
            };
            PluginSystem.EventManager.ExecuteDoorOpen(ev);
        }

        public static void InvokeRoundEnd(int escapedplayers, bool forceended)
        {
            RoundEndEvent ev = new RoundEndEvent()
            {
                EscapedPlayers = escapedplayers,
                ForceEnded = forceended
            };
            PluginSystem.EventManager.ExecuteRoundEnd(ev);
        }

        public static void InvokeRoundStart()
        {
            PluginSystem.EventManager.ExecuteRoundStart();
        }

        public static void InvokeRoundRestart()
        {
            PluginSystem.EventManager.ExecuteRoundRestart();
        }
    }
}
