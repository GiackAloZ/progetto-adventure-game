using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
	class Giocatore : Combattente
	{
		private List<int> _esperienzeSalitaLivello;

		public int Stamina { get; set; }

		public int MaxStamina { get; set; }

		public int MaxSalute { get; set; }

		public int MaxPrecisione { get; set; }

		public int Esperienza { get; set; }

        public Inventario Inv { get; set; }

		public List<Arma> ArmiEquipaggiate { get; set; }

		public List<Indumento> IndumentiEquipaggiati { get; set; }

		public Giocatore(string n, string d, int s, int dif, int a, int p)
			: base(n, d, s, dif, a, p, 1)
		{
			MaxPrecisione = Precisione;
			MaxSalute = Salute;
			Stamina = 100;
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

		public int EsperienzaPassaggioLivello()
		{
			return _esperienzeSalitaLivello[Livello];
		}

		public string EquipaggiaArma(Arma a)
		{
			int count = 0;
			foreach (Arma arm in ArmiEquipaggiate)
			{
				count += (int)arm.Impugnatura;
			}
			count += (int)a.Impugnatura;
			if (count > 100)
				return "Non hai più spazio per equipaggiare quest'arma!\n";
			ArmiEquipaggiate.Add(a);
			Inv.Oggetti.Remove(a);
			Attacco += a.BonusAttacco;
			return "Arma : " + a.Nome + " equipaggiato!\n";
		}

		public string EquipaggiaIndumento(Indumento i)
		{
			foreach (Indumento ind in IndumentiEquipaggiati)
			{
				if (ind.Tipo == i.Tipo)
					return "Stai gia indossando un indumento di questo tipo!\n";
			}
			IndumentiEquipaggiati.Add(i);
			Inv.Oggetti.Remove(i);
			Difesa += i.BonusDifesa;
			MaxStamina += i.BonusStamina;
			Stamina += i.BonusStamina;
			return i.Tipo.ToString() + " : " + i.Nome + " equipaggiato!\n";
		}

		public string DisequipaggiaArma(Arma a)
		{
			ArmiEquipaggiate.Remove(a);
			Inv.Oggetti.Add(a);
			Attacco -= a.BonusAttacco;
			return "Arma : " + a.Nome + " disequipaggiata!\n";
		}

		public string DisequipaggiaIndumento(Indumento i)
		{
			IndumentiEquipaggiati.Remove(i);
			Inv.Oggetti.Add(i);
			Difesa -= i.BonusDifesa;
			MaxStamina -= i.BonusStamina;
			Stamina -= i.BonusStamina;
			return i.Tipo.ToString() + " : " + i.Nome + " disequipaggiato!\n";
		}

		public Inventario RitornaInventario()
		{
			return Inv;
		}

		public string Combatti(Combattente c, out int result)
		{
			Random r = new Random();
			int rand = r.Next(85, 101);
			int dannoInflitto = (rand / 50) * (((Attacco / c.Difesa) * ((Livello / 5) + 1)) + 1);
			rand = r.Next(85, 101);
			int dannoRicevuto = (rand / 50) * (((c.Attacco / Difesa) * ((c.Livello / 5) + 1)) + 1);

			bool vivo = true, nemicoVivo = true;
			string res = "";

			rand = r.Next(0, 101);
			if (Precisione >= rand)
			{
				nemicoVivo = c.Danneggia(dannoInflitto);
				res += String.Format("Hai inflitto {0} danni a {1}!\n", dannoInflitto, c.Nome);
			}
			else
				res += String.Format("Hai mancato {0}!\n", c.Nome);
			rand = r.Next(0, 101);
			if (c.Precisione >= rand)
			{
				vivo = Danneggia(dannoRicevuto);
				res += String.Format("{0} ti ha inflitto {1} danni!\n", c.Nome, dannoRicevuto);
			}
			else
				res += String.Format("{0} ti ha mancato!\n", c.Nome);

			if (vivo)
			{
				if (nemicoVivo)
				{
					result = 1;
					return res;
				}
				else
				{
					result = 0;
					res += String.Format("Hai sconfitto {0}!\n", c.Nome);
					return res;
				}
			}
			else
			{
				if (nemicoVivo)
				{
					result = -1;
					res += String.Format("Sei morto contro {0}!\n", c.Nome);
					return res;
				}
				else
				{
					res += String.Format("Sei moribondo, ma sei riuscito a sconfiggere {0}!\n", c.Nome);
					Salute = 1;
					result = 0;
					return res;
				}
			}
		}
	}
}
