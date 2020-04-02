using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vakantieverhuur.LIB.Entities
{
    public class Caravan:Verblijf
    {
        public bool? PersoonlijkSanitair { get; set; }
        public override string ToString()
        {
            return $"C - {HuisNaam} - {Gemeente}";
        }
    }
}
