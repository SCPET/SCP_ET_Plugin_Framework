using System;

namespace SCP_ET.API.Events.EventsArgs
{
    public class PlayerMissionModifiedEvent : EventArgs
    {
        public object missionInfo { get; set; }
    }
}
