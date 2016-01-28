using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
	class Interlocutore
	{
		public string Nome { get; set; }

		public List<Dialogo> Dialoghi { get; set; }

		public string Descrizione { get; set; }

		public Interlocutore(string n, List<Dialogo> dials, string desc)
		{
			Nome = n;
			Dialoghi = dials;
			Descrizione = desc;
		}
	}
}
