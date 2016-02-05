using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
    class Scelta
    {
        public string Entrata { get; set; }

        public List<Tuple<string, int>> Opzioni { get; set; }

        public Scelta() { }

        public Scelta(string en, List<Tuple<string, int>> opz)
        {
            Entrata = en;
            Opzioni = opz;
        }
    }
}
