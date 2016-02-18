using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
    class Oggetto : Entità
    {
        public override string Nome { get; set; }

        public override string Descrizione { get; set; }

        public Oggetto() : base() { }

        public Oggetto(string n, string d) : base(n, d) { }

    }
}
