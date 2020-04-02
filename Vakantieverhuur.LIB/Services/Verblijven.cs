using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vakantieverhuur.LIB.Entities;

namespace Vakantieverhuur.LIB.Services
{
    public class Verblijven
    {
        public static List<Verblijf> AlleVerblijven = new List<Verblijf>();
        public static void Initialiseer()
        {
            AlleVerblijven.Add(new Vakantiehuis() { StraatEnNummer = "Klaverstraat 1", Postnummer = 8000, Gemeente = "Brugge", HuisNaam = "'t Eeuwig leven", MaxPersonen = 2, BasisPrijs = 70M, DagenVoorVermindering = 7, VerminderdePrijs = 65M, Waarborg = 140M, Microgolf = true, TV = true, Vaatwas = false, Wasmachine = false, Houtkachel = false });
            AlleVerblijven.Add(new Vakantiehuis() { StraatEnNummer = "Steenstraat 123/7", Postnummer = 8000, Gemeente = "Brugge", HuisNaam = "Kiekekot", MaxPersonen = 4, BasisPrijs = 120M, DagenVoorVermindering = 7, VerminderdePrijs = 110M, Waarborg = 240M, Microgolf = true, TV = true, Vaatwas = true, Wasmachine=true, Houtkachel = false });
            AlleVerblijven.Add(new Vakantiehuis() { StraatEnNummer = "Stoofstraat 99", Postnummer = 8000, Gemeente = "Brugge", HuisNaam = "Zwaluwnest", MaxPersonen = 2, BasisPrijs = 85M, DagenVoorVermindering = 7, VerminderdePrijs = 75M, Waarborg = 170M, Microgolf = true, TV = true, Vaatwas = true,Wasmachine=true, Houtkachel = true });
            AlleVerblijven.Add(new Caravan() { StraatEnNummer = "Veltemweg 109 - P57", Postnummer = 8310, Gemeente = "Brugge", HuisNaam = "Krot & Co", MaxPersonen = 3, BasisPrijs = 45M, DagenVoorVermindering = 7, VerminderdePrijs = 40M, Waarborg = 90M, Microgolf = false, TV = true, PersoonlijkSanitair = false }) ;

        }
    }
}
