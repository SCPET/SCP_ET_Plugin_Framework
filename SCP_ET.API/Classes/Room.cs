using SCP_ET.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_ET.API.Classes
{
    public abstract class Room : IObject
    {
        public abstract void OnDestroy();
        public static List<Room> Rooms { get; set; } = new List<Room>();
        public abstract Vector Position { get; set; }
        public abstract Vector Rotation { get; set; }
        public abstract object GameObject { get; }
        public abstract string RoomName { get; }
        public abstract string ZoneName { get; }
    }
}
