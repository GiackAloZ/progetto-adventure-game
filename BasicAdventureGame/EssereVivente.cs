using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
    class EssereVivente : Entità
    {
        public override string Descrizione { get; set; }

        public override string Nome { get; set; }

        public int Salute { get; set; }

        public EssereVivente() : base() { }

        public EssereVivente(string n, string d, int s) : base(n, d)
        {
            Salute = s;
        }

		public override string ToString()
		{
			return Nome;
		}
    }
}
