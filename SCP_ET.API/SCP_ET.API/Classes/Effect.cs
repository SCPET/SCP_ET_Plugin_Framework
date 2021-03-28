using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCP_ET.API.Classes
{
    public abstract class Effect
    {
        public abstract string id { get; }
        public abstract bool Temporary { get; set; }
        public abstract float TimeLeft { get; set; }
        public abstract float Intensity { get; set; }
    }
}
