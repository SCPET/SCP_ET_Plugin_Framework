using SCP_ET.API.Classes;
using System.Collections;
using System.Collections.Generic;

namespace SCP_ET.API.Interfaces
{
    public interface ItemMelee
    {
        void Swing(Player plr);
        float FireRate { get; }
        float Range { get; }
    }
}
