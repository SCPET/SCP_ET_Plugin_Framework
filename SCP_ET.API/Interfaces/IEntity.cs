using SCP_ET.API.Classes;
using SCP_ET.API.Enums;

namespace SCP_ET.API.Interfaces
{
    public interface IEntity
    {
        bool IsValidTarget { get; }
        object GameObject { get; }
        Vector Position { get; set; }
        Vector Rotation { get; set; }
        DimensionType CurrentDimension { get; set; }
        void OnDestroy();
    }
}
