using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
    class Dialogo
    {
        public List<Scelta> Scelte { get; set; }

		public Dialogo() { }

		public Dialogo(List<Scelta> sc)
		{
			Scelte = sc;
		}
    }
}
