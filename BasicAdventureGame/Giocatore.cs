using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
	class Giocatore : Combattente
	{
		private Tuple<string, string>[,] _tabellaCombattimenti = new Tuple<string, string>[,]{{new Tuple<string,string>("0", "M"), new Tuple<string,string>("0", "M"), new Tuple<string,string>("0", "8"), new Tuple<string,string>("0", "6"), new Tuple<string,string>("1", "6"), new Tuple<string,string>("2", "5"), new Tuple<string,string>("3", "5"), new Tuple<string,string>("4", "5"), new Tuple<string,string>("5", "4"), new Tuple<string,string>("6", "4"), new Tuple<string,string>("7", "4"), new Tuple<string,string>("8", "3"), new Tuple<string,string>("9", "3")},
																							  {new Tuple<string,string>("0", "M"), new Tuple<string,string>("0", "8"), new Tuple<string,string>("0", "7"), new Tuple<string,string>("1", "6"), new Tuple<string,string>("2", "5"), new Tuple<string,string>("3", "5"), new Tuple<string,string>("4", "4"), new Tuple<string,string>("5", "4"), new Tuple<string,string>("6", "3"), new Tuple<string,string>("7", "3"), new Tuple<string,string>("8", "3"), new Tuple<string,string>("9", "3"), new Tuple<string,string>("10", "2")},
																							  {new Tuple<string,string>("0", "8"), new Tuple<string,string>("0", "7"), new Tuple<string,string>("1", "6"), new Tuple<string,string>("2", "5"), new Tuple<string,string>("3", "5"), new Tuple<string,string>("4", "4"), new Tuple<string,string>("5", "4"), new Tuple<string,string>("6", "3"), new Tuple<string,string>("7", "3"), new Tuple<string,string>("8", "3"), new Tuple<string,string>("9", "2"), new Tuple<string,string>("10", "2"), new Tuple<string,string>("11", "2")},
																							  {new Tuple<string,string>("0", "8"), new Tuple<string,string>("1", "7"), new Tuple<string,string>("2", "6"), new Tuple<string,string>("3", "5"), new Tuple<string,string>("4", "4"), new Tuple<string,string>("5", "4"), new Tuple<string,string>("6", "3"), new Tuple<string,string>("7", "3"), new Tuple<string,string>("8", "2"), new Tuple<string,string>("9", "2"), new Tuple<string,string>("10", "2"), new Tuple<string,string>("11", "2"), new Tuple<string,string>("12", "2")},
																							  {new Tuple<string,string>("1", "7"), new Tuple<string,string>("6", "2"), new Tuple<string,string>("3", "5"), new Tuple<string,string>("4", "4"), new Tuple<string,string>("5", "4"), new Tuple<string,string>("6", "3"), new Tuple<string,string>("7", "2"), new Tuple<string,string>("8", "2"), new Tuple<string,string>("9", "2"), new Tuple<string,string>("10", "2"), new Tuple<string,string>("11", "2"), new Tuple<string,string>("12", "2"), new Tuple<string,string>("14", "1")},
																							  {new Tuple<string,string>("2", "6"), new Tuple<string,string>("3", "6"), new Tuple<string,string>("4", "5"), new Tuple<string,string>("5", "4"), new Tuple<string,string>("6", "3"), new Tuple<string,string>("7", "3"), new Tuple<string,string>("8", "2"), new Tuple<string,string>("9", "2"), new Tuple<string,string>("10", "2"), new Tuple<string,string>("11", "1"), new Tuple<string,string>("12", "1"), new Tuple<string,string>("14", "1"), new Tuple<string,string>("16", "1")},
																							  {new Tuple<string,string>("3", "5"), new Tuple<string,string>("4", "5"), new Tuple<string,string>("5", "4"), new Tuple<string,string>("6", "3"), new Tuple<string,string>("7", "2"), new Tuple<string,string>("8", "2"), new Tuple<string,string>("9", "1"), new Tuple<string,string>("10", "1"), new Tuple<string,string>("11", "1"), new Tuple<string,string>("12", "0"), new Tuple<string,string>("14", "0"), new Tuple<string,string>("16", "0"), new Tuple<string,string>("18", "0")},
																							  {new Tuple<string,string>("4", "4"), new Tuple<string,string>("5", "4"), new Tuple<string,string>("6", "3"), new Tuple<string,string>("7", "2"), new Tuple<string,string>("8", "1"), new Tuple<string,string>("9", "1"), new Tuple<string,string>("10", "0"), new Tuple<string,string>("11", "0"), new Tuple<string,string>("12", "0"), new Tuple<string,string>("14", "0"), new Tuple<string,string>("16", "0"), new Tuple<string,string>("18", "0"), new Tuple<string,string>("M", "0")},
																							  {new Tuple<string,string>("5", "3"), new Tuple<string,string>("6", "3"), new Tuple<string,string>("7", "2"), new Tuple<string,string>("8", "0"), new Tuple<string,string>("9", "0"), new Tuple<string,string>("10", "0"), new Tuple<string,string>("11", "0"), new Tuple<string,string>("12", "0"), new Tuple<string,string>("14", "0"), new Tuple<string,string>("16", "0"), new Tuple<string,string>("18", "0"), new Tuple<string,string>("M", "0"), new Tuple<string,string>("M", "0")},
																							  {new Tuple<string,string>("6", "0"), new Tuple<string,string>("7", "0"), new Tuple<string,string>("8", "0"), new Tuple<string,string>("9", "0"), new Tuple<string,string>("10", "0"), new Tuple<string,string>("11", "0"), new Tuple<string,string>("12", "0"), new Tuple<string,string>("14", "0"), new Tuple<string,string>("16", "0"), new Tuple<string,string>("18", "0"), new Tuple<string,string>("M", "0"), new Tuple<string,string>("M", "0"), new Tuple<string,string>("M", "0")}
																							 };

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

		public string DisequipaggiaArma(Arma a)
		{
			ArmiEquipaggiate.Remove(a);
			Attacco -= a.BonusAttacco;
			return "Arma : " + a.Nome + " disequipaggiata!\n";
		}

		public string DisequipaggiaIndumento(Indumento i)
		{
			IndumentiEquipaggiati.Remove(i);
			Difesa -= i.BonusDifesa;
			MaxStamina -= i.BonusStamina;
			return i.Tipo.ToString() + " : " + Nome + " disequipaggiato!\n";
		}

		public string Combatti(Combattente c, out int result)
		{
			Random r = new Random();
			int numeroDestino = r.Next(10) + 1;
			int diffAtk = Attacco - c.Attacco;
			int colonnaDestino = 0;
			if(diffAtk <= -11)
				colonnaDestino = 0;
			else if(diffAtk <= -9)
				colonnaDestino = 1;
			else if(diffAtk <= -7)
				colonnaDestino = 2;
			else if(diffAtk <= -5)
				colonnaDestino = 3;
			else if(diffAtk <= -3)
				colonnaDestino = 4;
			else if(diffAtk <= -1)
				colonnaDestino = 5;
			else if(diffAtk == 0)
				colonnaDestino = 6;
			else if(diffAtk <= 2)
				colonnaDestino = 7;
			else if(diffAtk <= 4)
				colonnaDestino = 8;
			else if(diffAtk <= 6)
				colonnaDestino = 9;
			else if(diffAtk <= 8)
				colonnaDestino = 10;
			else if(diffAtk <= 10)
				colonnaDestino = 11;
			else
				colonnaDestino = 12;
			Tuple<string, string> danni = _tabellaCombattimenti[numeroDestino, colonnaDestino];

			if (danni.Item1 == "M")
			{
				result = 2;
				return "Hai sconfitto " + c.Nome + "!\n";
			}

			if (danni.Item2 == "M")
			{
				result = 0;
				return "Sei morto contro " + c.Nome + "\n";
			}

			int danniNemico = int.Parse(danni.Item1);
			int danniGiocatore = int.Parse(danni.Item2);

			if (c.Danneggia(danniNemico))
			{
				result = 2;
				return "Hai sconfitto " + c.Nome + "!\n";
			}

			if (Danneggia(danniGiocatore))
			{
				result = 0;
				return "Sei morto contro " + c.Nome + "\n";
			}

			result = 1;
			return "Hai inflitto " + danniNemico + " danni a " + c.Nome + "\n" + c.Nome + " ti ha inflitto " + danniGiocatore + " danni\n";
		}
	}
}
