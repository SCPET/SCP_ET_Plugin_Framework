using SCP_ET.API.Classes;
using System.Collections;
using System.Collections.Generic;

namespace SCP_ET.API.Interfaces
{
    public interface ItemWeapon
    {
        int GetAmmo();
        void SetAmmo(int ammo);
        void Shoot(Player plr);
        float FireRate { get; }
    }
}
