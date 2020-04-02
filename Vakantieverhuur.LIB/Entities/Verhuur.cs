using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Vakantieverhuur.LIB.Entities
{
    public class Verhuur
    {
        public Verblijf Vakantieverblijf { get; set; }
        public Huurder DeHuurder { get; set; }
        public DateTime DatumVan { get; set; }
        public DateTime DatumTot { get; set; }
        public bool WaarborgGestort { get; set; }
        public decimal Betaald { get; set; }
        public decimal Tebetalen { get; set; }



    }
}
