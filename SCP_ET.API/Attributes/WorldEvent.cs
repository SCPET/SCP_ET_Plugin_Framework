using SCP_ET.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_ET.API.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class WorldEvent : Attribute
    {
        public WorldEventType EventType { get; }
        public WorldEvent(WorldEventType type)
        {
            EventType = type;
        }
    }
}
