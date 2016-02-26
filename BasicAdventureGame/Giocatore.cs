using System;
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

		public List<Arma> ArmiEquipaggiate { get; set; }

		public List<Indumento> IndumentiEquipaggiati { get; set; }

		public Giocatore(string n, string d, int s, int dif, int a, int p)
			: base(n, d, s, dif, a, p, 1)
		{
			MaxStamina = 100;
			Esperienza = 0;
			_esperienzeSalitaLivello = new List<int>(new int[] { 0, 10, 30, 70, 150 });
            Inv = new Inventario();
			ArmiEquipaggiate = new List<Arma>();
			IndumentiEquipaggiati = new List<Indumento>();
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

		public string EquipaggiaArma(Arma a)
		{
			int count = 0;
			foreach (Arma arm in Inv.Oggetti)
			{
				count += (int)arm.Impugnatura;
			}
			count += (int)a.Impugnatura;
			if (count > 100)
				return "Non hai più spazio per equipaggiare quest'arma!\n";
			ArmiEquipaggiate.Add(a);
			Attacco += a.BonusAttacco;
			return "Arma : " + a.Nome + " equipaggiato!\n";
		}

		public string EquipaggiaIndumento(Indumento i)
		{
			foreach (Indumento ind in Inv.Oggetti)
			{
				if (ind.Tipo == i.Tipo)
					return "Stai gia indossando un indumento di questo tipo!\n";
			}
			IndumentiEquipaggiati.Add(i);
			Difesa += i.BonusDifesa;
			MaxStamina += i.BonusStamina;
			return i.Tipo.ToString() + " : " + Nome + " equipaggiato!\n";
		}
	}
}
