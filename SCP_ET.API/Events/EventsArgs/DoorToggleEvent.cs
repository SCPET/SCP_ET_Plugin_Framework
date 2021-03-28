using SCP_ET.API.Classes;
using SCP_ET.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_ET.API.Events.EventsArgs
{
    public class DoorToggleEvent : EventArgs
    {
        public IEntity Entity { get; set; }
        public Door Door { get; set; }
    }
}
