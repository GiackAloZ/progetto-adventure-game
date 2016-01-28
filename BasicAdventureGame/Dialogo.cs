using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
	class Dialogo
	{
		public string Descrizione { get; set; }

		public int IndiceCorrente { get; set; }

		public string[] Risposte { get; set; }

		public Scelta[] Scelte { get; set; }
 
		public bool IsEnabled { get; set; }

		public Dialogo(string desc, string[] risp, Scelta[] sc, bool en)
		{
			Descrizione = desc;
			risp = Risposte;
			Scelte = sc;
			IsEnabled = en;
			IndiceCorrente = 0;
		}

		string Scegli(int n, out bool last)
		{
			IndiceCorrente = Scelte[IndiceCorrente].Destinazioni[n];
			string s = Risposte[IndiceCorrente];
			if (Scelte[IndiceCorrente].Destinazioni == null)
				last = true;
			else
				last = false;
			return s;
		}
	}
}
