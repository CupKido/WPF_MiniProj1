using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace targil2
{
    class BusLine
    {
        int IDL = new int();
        BuStationLine FirstStation = new BuStationLine();
        BuStationLine LastStation = new BuStationLine();
        areacode area = new int();
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
        public areacode GSarea //gs for Get Set  
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
        public void add(int x)
        {
            stations.Insert(x,this);
        }
        public void delete(int x)
        {
            int i = 0;
            IEnumerator e = stations.GetEnumerator();
            ArrayList temp = new ArrayList();
            while (e.MoveNext())
            {
                i++;
                if (i != x)
                {
                   temp.Add(e.Current);
                }
            }
        }
        
        public bool search()
        {
            if ()
            {
                return true;
            }
        }

        public override string ToString()
        {
            string temp = null;
            switch (this.area)
            {
                case areacode.general:
                    temp = "general";
                    break;
                case areacode.North:
                    temp = "North";
                    break;
                case areacode.south:
                    temp = "South";
                    break;
                case areacode.center:
                    temp = "Center";
                    break;
                case areacode.jerusalem:
                    temp = "Jerusalem";
                    break;
                default:
                    temp = "general";
                    break;
            }
            return "line: " + IDL + '\n' + "area: " + temp.ToString() ;
        }
    }
   
}
