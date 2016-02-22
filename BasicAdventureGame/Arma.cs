using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
    class Arma : Oggetto
    {
        public int BonusAttacco { get; set; }

        public Impugnature Impugnatura { get; set; }

        public Arma(string n, string d, int bonusAtk, Impugnature imp) : base(n, d)
        {
            BonusAttacco = bonusAtk;
            Impugnatura = imp;
        }

        public string Equipaggia(Giocatore g)
        {
            int count = 0;
            foreach(Arma a in g.Inv.Oggetti)
            {
                count += (int)a.Impugnatura;
            }
            count += (int)Impugnatura;
            if (count > 100)
                return "Non hai più spazio per equipaggiare quest'arma!\n";
            g.Attacco += BonusAttacco;
            return "Arma : " + Nome + " equipaggiato!\n";
        }
    }
}