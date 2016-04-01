using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
	class Nemico : Combattente
	{
		public int DropExp { get; private set; }

		public Oggetto[] DropItems { get; private set; }

		public int[] DropItemsPercentage { get; private set; }

		public Nemico(string n, string d, int s, int dif, int a, int p, int l, int de, Oggetto[] di, int[] dip)
			: base(n, d, s, dif, a, p, l)
		{
			DropExp = de;
			DropItems = (Oggetto[])di.Clone();
			DropItemsPercentage = dip;
		}

		public List<Oggetto> Drop()
		{
			List<Oggetto> l = new List<Oggetto>();
			Random r = new Random();
			for (int i = 0; i < DropItems.Length; i++)
			{
				if (r.Next(0, 101) < DropItemsPercentage[i])
					l.Add(DropItems[i]);
			}
			return l;
		}
	}
}
