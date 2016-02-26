using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
	/// <summary>
	/// Classe usata per le armi
	/// </summary>
    class Arma : Oggetto
    {
		/// <summary>
		/// Proprietà intera che descrive il bonus di attacco che conferisce l'arma una volta equipaggiata
		/// </summary>
        public int BonusAttacco { get; set; }

		/// <summary>
		/// Proprièta di tipo Impugnature che descrive il tipo di impugnatura dell'arma
		/// </summary>
        public Impugnature Impugnatura { get; set; }

		/// <summary>
		/// Costruttore della classe Arma
		/// </summary>
		/// <param name="n">Nome dell'arma</param>
		/// <param name="d">Descrizione dell'arma</param>
		/// <param name="bonusAtk">Bonus attacco dell'arma</param>
		/// <param name="imp">Tipo di impugnatura dell'arma</param>
        public Arma(string n, string d, int bonusAtk, Impugnature imp) : base(n, d)
        {
            BonusAttacco = bonusAtk;
            Impugnatura = imp;
        }
    }
}