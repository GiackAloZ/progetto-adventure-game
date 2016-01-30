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

        public int Vita { get; set; }

        public EssereVivente() { }

        public EssereVivente(string n, string d, int v)
        {
            Nome = n;
            Descrizione = d;
            Vita = v;
        }
    }
}
