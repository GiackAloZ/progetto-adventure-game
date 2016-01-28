using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
	class ApriPassaggio : Azione
	{
		public override string Nome { get; set; }

		public override string Descrizione { get; set; }

		public int IndiceAmbientePartenza { get; set; }

		public int IndiceAmbienteArrivo { get; set; }

		public ApriPassaggio(int iap, int iaa)
		{
			Nome = "Open";
			Descrizione = "Apre il passaggio tra due ambienti";
			IndiceAmbientePartenza = iap;
			IndiceAmbienteArrivo = iaa;
		}
		public override string Esegui(GestoreMappa m)
		{
			for(int i = 0; i < 4; i++)
			{
				if (m.Mappa[IndiceAmbientePartenza].Passaggi[i] != null && m.Mappa[IndiceAmbientePartenza].Passaggi[i].IndiceAmbienteDestinazione == IndiceAmbienteArrivo)
					m.Mappa[IndiceAmbientePartenza].Passaggi[i].Aperto = true;
			}
            return "Hai aperto il passaggio da " + m.Mappa[IndiceAmbientePartenza].Descrizione + " a " + m.Mappa[IndiceAmbienteArrivo].Descrizione + "\n";
        }
	}
}
