using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vakantieverhuur.LIB.Entities
{
    public class Vakantiehuis:Verblijf
    {
        public bool? Vaatwas { get; set; }
        public bool? Wasmachine { get; set; }
        public bool? Houtkachel { get; set; }

        public override string ToString()
        {
            return $"H - {HuisNaam} - {Gemeente}";
        }
    }

}
