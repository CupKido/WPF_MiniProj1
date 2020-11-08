using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace targil2
{
    class BuStationLine
    {
        BuStation current;
        double KFL; // Km From Last
        double TFL; // Time From Last
        public BuStation GSStation //GS for GetSet
        {
            get
            {
                return current;
            }
            set
            {
                current = value;
            }
        }
    }
}
