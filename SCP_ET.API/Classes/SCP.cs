using SCP_ET.API.Enums;
using SCP_ET.API.Interfaces;
using System.Collections.Generic;

namespace SCP_ET.API.Classes
{
    public abstract class SCP : IEntity
    {
        public abstract DimensionType CurrentDimension { get; set; }
        public static List<SCP> Scps { get; set; } = new List<SCP>();
        public abstract bool IsValidTarget { get; }
        public abstract object GameObject { get; }
        public abstract void OnDestroy();
        public abstract Vector Position { get; set; }
        public abstract Vector Rotation { get; set; }
        public abstract string ScpName { get; }
    }
}
