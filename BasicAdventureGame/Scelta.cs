using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
    class Scelta
    {
        public string Risposta { get; set; }

        public List<Tuple<string, int>> Opzioni { get; set; }

        public Scelta() { }

        public Scelta(string r, List<Tuple<string, int>> opz)
        {
            Risposta = r;
            Opzioni = opz;
        }
    }
}
