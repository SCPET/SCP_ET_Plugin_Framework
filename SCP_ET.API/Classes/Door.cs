using System.Collections.Generic;

namespace SCP_ET.API.Classes
{
    public abstract class Door
    {
        public static List<Door> Doors { get; set; } = new List<Door>();
        public abstract Vector Position { get; set; }
        public abstract object GameObject { get; }
        public abstract bool IsOpen { get; }
        public abstract void Close();
        public abstract void Open();
    }
}
