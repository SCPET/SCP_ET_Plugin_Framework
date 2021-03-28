using System;
using System.Collections;

namespace SCP_ET.API.Events.EventsArgs
{
    public class PlayerAuthEvent : EventArgs
    {
        public object Connection { get; set; }
    }
}
