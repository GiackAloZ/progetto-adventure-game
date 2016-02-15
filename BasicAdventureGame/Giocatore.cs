using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
	class Giocatore : Combattente
	{
		private List<int> _esperienzeSalitaLivello;

		public int Stamina { get; set; }

		public int Esperienza { get; set; }

		public Giocatore(string n, string d, int s, int dif, int a, int p)
			: base(n, d, s, dif, a, p, 1)
		{
			Stamina = 100;
			Esperienza = 0;
			_esperienzeSalitaLivello = new List<int>(new int[] { 0, 10, 30, 70, 150 });
		}
	}
}
