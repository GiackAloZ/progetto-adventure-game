using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
    class Persona : EssereVivente
    {
        public Dialogo Dial { get; set; }

        public Persona() { }

        public Persona(string n, string d, int v, Dialogo dial)
        {
            Nome = n;
            Descrizione = d;
            Vita = v;
            Dial = dial;
        }
    }
}
