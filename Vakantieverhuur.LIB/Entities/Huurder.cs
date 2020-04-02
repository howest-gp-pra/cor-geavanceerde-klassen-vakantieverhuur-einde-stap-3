using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vakantieverhuur.LIB.Entities
{
    public class Huurder:Persoon
    {
        public bool IsBlackListed { get; set; } = false;

        public override string ToString()
        {
            return $"{Naam} {Voornaam} - {Land}";
        }
    }
}
