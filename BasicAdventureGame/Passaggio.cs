using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
	/// <summary>
	/// Classe che gestisce un passaggio tra ambienti
	/// </summary>
	class Passaggio
	{
		/// <summary>
		/// Proprietà booleana che indica se il passaggio è aperto (true) o chiuso (false)
		/// </summary>
		public bool Aperto { get; set; }

		/// <summary>
		/// Proprietà intera che indica l'indice dell'ambiente di destinazione
		/// </summary>
		public int IndiceAmbienteDestinazione { get; set; }

		/// <summary>
		/// Proprietà stringa che indica il titolo del passaggio
		/// </summary>
		public string Titolo { get; set; }

		/// <summary>
		/// Costruttore della classe Passaggio
		/// </summary>
		/// <param name="t">Titolo del passaggio</param>
		/// <param name="a">True = aperto, false = chiuso</param>
		/// <param name="i">Indice dell'ambiente di destinazione</param>
		public Passaggio(string t, bool a, int i)
		{
			Titolo = t;
			Aperto = a;
			IndiceAmbienteDestinazione = i;
		}
	}
}
