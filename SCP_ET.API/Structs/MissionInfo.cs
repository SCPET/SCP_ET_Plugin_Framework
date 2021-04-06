using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_ET.API.Structs
{
    [Serializable]
    public struct MissionInfo
    {
        public string MissionGiver;
        public string MissionName;
        public string IDofCompleter;
        public string InitialGivenToID;
        public string Description;
        public bool Complete;
        public bool GlobalTask;
        public bool PlayerCanStealReward;
        public bool Started;
        public int FavorRequired;
        public int Favor;
    }
}
