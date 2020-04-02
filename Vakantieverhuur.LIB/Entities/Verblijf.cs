using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vakantieverhuur.LIB.Entities
{
    public class Verblijf
    {
		private string id;
		private int maxpersonen;
		private int postnummer;
		private decimal basisprijs;
		private decimal verminderdeprijs;
		private byte dagenvoorvermindering;
		private decimal waarborg;


		public string ID
		{
			get { return id; }
		}
		public decimal BasisPrijs
		{
			get { return basisprijs; }
			set 
			{
				if (value < 0) value = 0;
				basisprijs = value; 
			}
		}
		public decimal VerminderdePrijs
		{
			get { return verminderdeprijs; }
			set 
			{
				if (value < 0) value = 0;
				verminderdeprijs = value; 
			}
		}
		public byte DagenVoorVermindering
		{
			get { return dagenvoorvermindering; }
			set 
			{
				if (value > 100) value = 100;
				dagenvoorvermindering = value; 
			}
		}
		public decimal Waarborg
		{
			get { return waarborg; }
			set 
			{
				if (value < 0) value = 0;
				waarborg = value; 
			}
		}
		public int MaxPersonen
		{
			get { return maxpersonen; }
			set 
			{
				if (value < 1) value = 1;
				if (value > 20) value = 20;
				maxpersonen = value; 
			}
		}
		public string StraatEnNummer { get; set; }
		public string HuisNaam { get; set; }
		public string Gemeente { get; set; }
		public int Postnummer
		{
			get { return postnummer; }
			set 
			{
				if (value < 1000) value = 1000;
				if (value > 9999) value = 9999;
				postnummer = value; 
			}
		}
		public bool? Microgolf { get; set; }
		public bool? TV { get; set; }
		public bool Verhuurbaar { get; set; } = true;
		public Verblijf()
		{
			id = Guid.NewGuid().ToString();
		}


	}
}
