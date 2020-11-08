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
        public double GSKFL //GS for GetSet
        {
            get
            {
                return KFL;
            }
            set
            {
                KFL = value;
            }
        }
        public double GSTFL //GS for GetSet
        {
            get
            {
                return TFL;
            }
            set
            {
                TFL = value;
            }
        }
        public BuStationLine(BuStation newbustation, double newKFL, double newTFL)
        {
            current = newbustation;
            KFL = newKFL;
            TFL = newTFL;
        }
        public BuStationLine()
        {
            current = new BuStation();
            KFL = 0;
            TFL = 0;
        }
    }
}
