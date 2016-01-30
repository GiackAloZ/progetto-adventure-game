using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
	/// <summary>
	/// Questa è la classe usata per rappresentare un Ambiente
	/// </summary>
	class Ambiente
	{
		/// <summary>
		/// Una stringa che rappresenta la Descrizione dell'Ambiente
		/// </summary>
		public string Descrizione { get; set; }

		/// <summary>
		/// Ogni Ambiente deve avere un vettori di Passaggi che collegano vari Ambienti
		/// </summary>
		public Passaggio[] Passaggi { get; set; }

		/// <summary>
		/// Vettore di Azioni che contiene tutte le azione dell'Ambiente
		/// </summary>
        public Azione[] Azioni { get; set; }

        public List<Entità> Cose { get; set; }

        /// <summary>
        /// Il costruttore della classe Ambiente
        /// </summary>
        /// <param name="d">Descrizione dell'ambiente</param>
        /// <param name="pn">Passaggio a nord</param>
        /// <param name="pe">Passaggio ad est</param>
        /// <param name="ps">Passaggio a sud</param>
        /// <param name="po">Passaggio ad ovest</param>
        public Ambiente(string d, Passaggio pn, Passaggio pe, Passaggio ps, Passaggio po)
		{
			Descrizione = d;
			Passaggi = new Passaggio[] { pn, pe, ps, po };
		}
	}
}
