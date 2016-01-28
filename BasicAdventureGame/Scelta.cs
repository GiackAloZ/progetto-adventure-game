using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
	class Scelta
	{
		public string[] Descrizioni { get; set; }

		public int[] Destinazioni { get; set; }

		public bool[] IsAvailable { get; set; }

		public Scelta(string[] desc, int[] dest, bool[] av)
		{
			Descrizioni = desc;
			Destinazioni = dest;
			IsAvailable = av;
		}
	}
}
