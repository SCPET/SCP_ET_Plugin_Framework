using SCP_ET.API.Interfaces;
using System.Collections.Generic;

namespace SCP_ET.API.Classes
{
    public abstract class SCP : IEntity
    {
        public static List<SCP> Scps { get; set; } = new List<SCP>();
        public abstract Vector Position { get; set; }
        public abstract string ScpName { get; }
    }
}
