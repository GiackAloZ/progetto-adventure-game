using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
	/// <summary>
	/// Classe derivata da Azione che apre un passaggio
	/// </summary>
	class ApriPassaggio : Azione
	{
		/// <summary>
		/// Nome dell'azione
		/// </summary>
		public override string Nome { get; set; }

		/// <summary>
		/// Descrizione dell'azione
		/// </summary>
		public override string Descrizione { get; set; }

		/// <summary>
		/// Indice che rappresenta la stanza dalla quale parte il passaggio da aprire
		/// </summary>
		public int IndiceAmbientePartenza { get; set; }

		/// <summary>
		/// Indice che rappresenta la stanza alla quale arriva il passaggio da aprire
		/// </summary>
		public int IndiceAmbienteArrivo { get; set; }

		/// <summary>
		/// Costruttore della classe
		/// </summary>
		/// <param name="iap">Indice partenza</param>
		/// <param name="iaa">Indice arrivo</param>
		public ApriPassaggio(int iap, int iaa)
		{
			Nome = "Open";
			Descrizione = "Apre il passaggio tra due ambienti";
			IndiceAmbientePartenza = iap;
			IndiceAmbienteArrivo = iaa;
		}

		/// <summary>
		/// Override del metodo della classe Azione che ci permette di eseguire l'azione di questa classe, ovvero aprire un passaggio
		/// </summary>
		/// <param name="m">Mappa cu sui applicare l'azione</param>
		/// <returns>Messaggo da stampare nel log</returns>
		public override string Esegui(GestoreMappa m)
		{
			for(int i = 0; i < 4; i++)
			{
				if (m.Mappa[IndiceAmbientePartenza].Passaggi[i] != null && m.Mappa[IndiceAmbientePartenza].Passaggi[i].IndiceAmbienteDestinazione == IndiceAmbienteArrivo && m.Mappa[IndiceAmbientePartenza].Passaggi[i].Aperto != true)
				{
					m.Mappa[IndiceAmbientePartenza].Passaggi[i].Aperto = true;
					return "Hai aperto il passaggio da " + m.Mappa[IndiceAmbientePartenza].Descrizione + " a " + m.Mappa[IndiceAmbienteArrivo].Descrizione + "\n";
				}
			}
			return "";
        }
	}
}
