using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicAdventureGame
{
    class Indumento : Oggetto
    {
        public int BonusDifesa { get; set; }

        public int BonusStamina { get; set; }

        public TipoIndumento Tipo { get; set; }

        public Indumento(string n, string d, int bDif, int bSta, TipoIndumento t) : base(n, d)
        {
            BonusDifesa = bDif;
            BonusStamina = bSta;
            Tipo = t;
        }

        public string Indossa(Giocatore g)
        {
            foreach(Indumento i in g.Inv.Oggetti)
            {
                if (i.Tipo == Tipo)
                    return "Stai gia indossando un indumento di questo tipo!\n";
            }
            g.Difesa += BonusDifesa;
            g.MaxStamina += BonusStamina;
            return Tipo.ToString() + " : " + Nome + " equipaggiato!\n";
        }
    }
}
