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
    }
}
