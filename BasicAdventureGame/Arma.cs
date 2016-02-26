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
    }
}