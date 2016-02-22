﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
	class Giocatore : Combattente
	{
		private List<int> _esperienzeSalitaLivello;

		public int MaxStamina { get; set; }

		public int Esperienza { get; set; }

        public Inventario Inv { get; set; }

		public Giocatore(string n, string d, int s, int dif, int a, int p)
			: base(n, d, s, dif, a, p, 1)
		{
			MaxStamina = 100;
			Esperienza = 0;
			_esperienzeSalitaLivello = new List<int>(new int[] { 0, 10, 30, 70, 150 });
            Inv = new Inventario();
		}

        public bool GuadagnaEsperienza(int exp)
        {
            Esperienza += exp;
            if(_esperienzeSalitaLivello[Livello] <= Esperienza)
            {
                Livello++;
                return true;
            }
            return false;
        }
	}
}
