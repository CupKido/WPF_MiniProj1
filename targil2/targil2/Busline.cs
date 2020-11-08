using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace targil2
{
    class BusLine
    {
        int IDL = new int();
        BuStationLine FirstStation = new BuStationLine();
        BuStationLine LastStation = new BuStationLine();
        int area = new int();
        ArrayList stations;
        public int GSid //gs for Get Set  
        {
            get
            {
                return IDL;
            }

            set { IDL = value; }
        }
        public BuStationLine GSFStation
        {
            get
            {
                return FirstStation;
            }

            set { FirstStation = value; }
        }
        public BuStationLine GSLStation
        {
            get
            {
                return LastStation;
            }

            set { LastStation = value; }
        }
        public int GSarea //gs for Get Set  
        {
            get
            {
                return area;
            }

            set { area = value; }
        }
        public enum areacode
        {
            general,
            North,
            south,
            center,
            jerusalem

        }
        public override string ToString()
        {
            return "line: " + IDL + '\n' + "area: " + area.ToString();
        }
    }
}
