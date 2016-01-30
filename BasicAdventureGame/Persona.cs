using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
    class Persona : EssereVivente
    {
        public List<string[]> Dialogo { get; set; }

        public Persona() { }

        public Persona(string n, string d, int v, List<string[]> dial)
        {
            Nome = n;
            Descrizione = d;
            Vita = v;
            Dialogo = dial;
        }
    }
}
