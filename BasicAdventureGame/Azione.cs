using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
	abstract class Azione
	{
		abstract public string Nome { get; set; }

		abstract public string Descrizione { get; set; }

		abstract public string Esegui(GestoreMappa m);
	}
}
