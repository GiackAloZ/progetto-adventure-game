using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
	abstract class Entità
	{
		public Entità()
		{
			Nome = "";
			Descrizione = "";
		}
		public Entità(string n, string d)
		{
			Nome = n;
			Descrizione = d;
		}
		abstract public string Nome { get; set; }

        abstract public string Descrizione { get; set; }
	}
}
