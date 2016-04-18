using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
    class Inventario : IEnumerable<Oggetto>
    {
        public List<Oggetto> Oggetti { get; set; }

        public Inventario() { Oggetti = new List<Oggetto>(); }

        public Inventario(List<Oggetto> oggs)
        {
            Oggetti = new List<Oggetto>(oggs);
        }

        public string Aggiungi(Oggetto obj)
        {
            Oggetti.Add(obj);
            return "Hai aggiunto " + obj.Nome + "\n";
        }

        public string Elimina(Oggetto obj)
        {
            Oggetti.Remove(obj);
            return "Hai eliminato " + obj.Nome + "\n";
        }

        public string Lascia(Oggetto obj, Inventario inv)
        {
            Oggetti.Remove(obj);
            inv.Aggiungi(obj);
            return "Hai lasciato " + obj.Nome + "\n";
        }

		public string Prendi(Oggetto obj, Inventario inv)
		{
			inv.Elimina(obj);
			Oggetti.Add(obj);
			return "Hai preso " + obj.Nome + "\n";
		}

		public IEnumerator<Oggetto> GetEnumerator()
		{
			foreach(Oggetto o in Oggetti)
				yield return o;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
		
    }
}
