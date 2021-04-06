using SCP_ET.API.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_ET.API.Interfaces
{
    public interface IObject
    {
        object GameObject { get; }
        Vector Position { get; set; }
        Vector Rotation { get; set; }
        void OnDestroy();

    }
}
