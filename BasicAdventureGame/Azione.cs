﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
	/// <summary>
	/// Classe astratta che serve da template per tutte le azioni disponibili
	/// </summary>
	abstract class Azione
	{
		/// <summary>
		/// Nome dell'azione
		/// </summary>
		abstract public string Nome { get; set; }

		/// <summary>
		/// Descrizione dell'azione
		/// </summary>
		abstract public string Descrizione { get; set; }

		/// <summary>
		/// Metodo che esegue l'azione
		/// </summary>
		/// <param name="m">Mappa sulla quale eseguire l'azione</param>
		/// <returns>Messaggio da stampare nel log</returns>
		abstract public string Esegui(GestoreMappa m);

		/// <summary>
		/// Costruttore di default
		/// </summary>
        public Azione()
        {
            Nome = "";
            Descrizione = "";
        }

		/// <summary>
		/// Costruttore con nome e descrizione
		/// </summary>
		/// <param name="n"><Nome azione/param>
		/// <param name="d">Descrizione azione</param>
        public Azione(string n, string d)
        {
            Nome = n;
            Descrizione = d;
        }
	}
}
