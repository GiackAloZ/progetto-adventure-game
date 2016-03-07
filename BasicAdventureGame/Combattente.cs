using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
	class Combattente : EssereVivente
	{
		public int Difesa { get; set; }

		public int Attacco { get; set; }

		public int Precisione { get; set; }

		public int Livello { get; set; }

		public Combattente() : base() { }

		public Combattente(string n, string d, int s, int dif, int a, int p, int l)
			: base(n, d, s)
		{
			Difesa = dif;
			Attacco = a;
			Precisione = p;
			Livello = l;
		}

		public bool Danneggia(int dmg)
		{
			Salute -= dmg;
			if (Salute <= 0)
				return false;
			return true;
		}
	}
}
