using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vakantieverhuur.LIB.Entities;

namespace Vakantieverhuur.LIB.Services
{
    public class Huurders
    {
        public static List<Huurder> AlleHuurders = new List<Huurder>();
        public static void Initialiseer()
        {
            AlleHuurders.Add(new Huurder() { Naam = "Jovi", Voornaam = "Bon", Adres = "Buckinhamstreet 1", Gemeente = "LND001 London", Land = "UK", Telefoon = "", Email = "bon.jovi@doedelzakplayers.co.uk" });
            AlleHuurders.Add(new Huurder() { Naam = "Jolie", Voornaam = "Angelina", Adres = "Holywoodroad 1", Gemeente = "SF123 San Fransisco", Land = "USA", Telefoon = "", Email = "angie.jolie@botoxpromotors.com" });
            AlleHuurders.Add(new Huurder() { Naam = "Cas", Voornaam = "Goossens", Adres = "Billiardplein 123", Gemeente = "1000 Brussel", Land = "België", Telefoon = "0497695847", Email = "cas.goossens@exbrt.be" });

        }
    }
}
