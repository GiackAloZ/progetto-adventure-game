using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
    class Persona : EssereVivente
    {
        public Dialogo Dial { get; set; }

		public Persona() : base() { }

        public Persona(string n, string d, int s, Dialogo dial) : base(n, d, s)
        {
            Dial = dial;
        }
    }
}
