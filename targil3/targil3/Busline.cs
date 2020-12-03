using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace targil2
{
    class BusLine : IComparable
    {
        string IDL = null;
        BuStation FStation = new BuStation(); //first station
        BuStation LStation = new BuStation(); //last station
        areacode area = new int();
        ArrayList stations = new ArrayList();
        public BusLine()
        {
            IDL = null;
            FStation = new BuStation();
            LStation = new BuStation();
            stations = new ArrayList();
        }
        public BusLine(string ID)
        {
            IDL = ID;
            FStation = new BuStation();
            LStation = new BuStation();
            stations = new ArrayList();
        }
        public BusLine(string ID, BuStation F, BuStation L)
        {
            IDL = ID;
            FStation = F;
            LStation = L;
            stations = new ArrayList();
        }
        public ArrayList GStations //GS for Get Set  
        {
            get
            {
                return stations;
            }
            set { stations = value; }
        }
        public string GSID //GS for Get Set  
        {
            get
            {
                return IDL;
            }

            set
            {
                bool check = false;
                int tempi = 0;
                check = int.TryParse(value, out tempi);
                if (!check)
                    Console.WriteLine("#ERROR!#\nunvalid input!\n");
                if (check)
                    IDL = value;
            }
        }
        public BuStation GSFStation //GS for Get Set
        {
            get
            {
                return FStation;
            }

            set { FStation = value; }
        }
        public BuStation GSLStation //GS for Get Set
        {
            get
            {
                return LStation;
            }

            set { LStation = value; }
        }
        public areacode GSarea //GS for Get Set
        {
            get
            {
                return area;
            }

            set
            {
                switch (value)
                {
                    case areacode.general:
                        area = value;
                        break;
                    case areacode.North:
                        area = value;
                        break;
                    case areacode.south:
                        area = value;
                        break;
                    case areacode.center:
                        area = value;
                        break;
                    case areacode.jerusalem:
                        area = value;
                        break;
                    default:
                        Console.WriteLine("#ERROR!#\nunvalid input!\n");
                        break;
                }
            }
        }
        public int CompareTo(object obj)
        {
            BusLine temp = (BusLine)obj;
            double timeforthis = TimeB2(FStation, LStation);
            double other = TimeB2(temp.FStation, LStation);
            return timeforthis.CompareTo(other);
        }
        public enum areacode
        {
            general,
            North,
            south,
            center,
            jerusalem
        }
        public void add(BuStationLine station, int x)
        {
            if (x == 0)
            {
                if (stations.Count - 1 > x)
                {
                    FStation = station.GSStation;
                }
            }
            if (x == stations.Count - 1)
            {
                LStation = SIS(x - 1).GSStation;
            }

            stations.Insert(x, station);
        }
        public int add(BuStationLine station)
        {
            foreach (BuStationLine temp in stations)
            {
                if (station.GSStation.GSID == temp.GSStation.GSID)
                { return -1; }
            }
            if (stations.Count==0)
            {
                FStation = station.GSStation;
            }
            stations.Add(station);
            LStation = station.GSStation;
            return 0;
        }
        public void delete(int x)
        {
            stations.RemoveAt(x - 1);
        }
        public void newdelete(int x)
        {
            if(x == 0)
            {
                if (stations.Count - 1 > x)
                {
                    FStation = SIS(x + 1).GSStation;
                }
                else
                {
                    FStation = null;
                }
            }
            if(x == stations.Count - 1)
            {
                if (stations.Count > 1)
                {
                    LStation = SIS(x - 1).GSStation;
                }
                else { LStation = null; }
                
            }
            
            stations.RemoveAt(x);
        }
        public BuStationLine SIS(int num)
        {
            int i = 0;
            foreach(BuStationLine stat in stations)
            {
                if(num == i)
                {
                    return stat;
                }
                i++;
            }
            return null;
        }
        public int SIS(string ID) //Search In Stations
        {
            BuStationLine temp = new BuStationLine();
            temp.GSStation.GSID = ID;
            BuStationLine temp2 = new BuStationLine();
            IEnumerator e = stations.GetEnumerator(); // e for Enumerator
            int i = 0;
            while (e.MoveNext())
            {
                temp2 = (BuStationLine)e.Current;
                if (temp.GSStation.GSID == temp2.GSStation.GSID)
                {
                    return i;
                }
                i += 1;
            }
            return -1;
        }
        public bool checkifex(string ID)
        {
            if (SIS(ID) != -1)
            {
                return true;
            }
            return false;
        }

        public bool checkifex(BuStation current)
        {
            if (SIS(current.GSID) != -1)
            {
                return true;
            }
            return false;

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
            return "line: " + IDL + "   " + "area: " + temp.ToString();
        }
        public bool Legit(int max)
        {
            IEnumerator e = stations.GetEnumerator(); // e for Enumerator
            for (int i = 0; i < max; i++)
            {
                if (!e.MoveNext())
                {
                    return false;
                }
            }
            return true;
        }
        public double DistanceB2(int a, int b) //distance between 2
        {
            IEnumerator e = stations.GetEnumerator(); // e for Enumerator
            int max = Math.Max(a, b);
            if (!Legit(max)) { return -1; }
            int min = Math.Min(a, b);
            int SB = max - min; //SB for stations between

            for (int i = 0; i < min; i++)
            {
                e.MoveNext();
            }
            e.MoveNext();

            double sum = 0;
            BuStationLine temp;
            for (int i = 0; i < SB; i++, e.MoveNext())
            {
                temp = (BuStationLine)e.Current;
                sum += temp.GSKFL;
            }
            return sum;
        }
        public double DistanceB2(BuStationLine a, BuStationLine b) //distance between 2
        {
            int stat1 = SIS(a.GSStation.GSID) + 1;
            int stat2 = SIS(b.GSStation.GSID) + 1;
            return DistanceB2(stat1, stat2);
        }
        public double TimeB2(int a, int b) //distance between 2
        {
            int max = Math.Max(a, b);
            if (!Legit(max)) { return -1; }

            IEnumerator e = stations.GetEnumerator();
            e.Reset();
            int min = Math.Min(a, b);
            int SB = max - min; //SB for stations between

            for (int i = 0; i < min; i++)
            {
                e.MoveNext();
            }
            e.MoveNext();

            double sum = 0;
            BuStationLine temp;
            for (int i = 0; i < SB; i++, e.MoveNext())
            {
                temp = (BuStationLine)e.Current;
                sum += temp.GSTFL;
            }
            return sum;
        }
        public double TimeB2(BuStation a, BuStation b) //distance between 2
        {
            int stat1 = SIS(a.GSID) + 1; //stat for station
            int stat2 = SIS(b.GSID) + 1; //stat for station
            return TimeB2(stat1, stat2);
        }
        public BusLine SubLine(int stat1, int stat2)
        {
            IEnumerator e = stations.GetEnumerator(); // e for Enumerator
            int max = Math.Max(stat1, stat2);
            if (!Legit(max)) { return null; }
            int min = Math.Min(stat1, stat2);
            int SB = max - min; //SB for stations between

            for (int i = 0; i < min; i++)
            {
                e.MoveNext();
            }
            BusLine res = new BusLine();
            BuStationLine temp = (BuStationLine)e.Current;
            res.FStation = temp.GSStation;
            for (int i = 0; i < SB; i++, e.MoveNext())
            {

                res.add((BuStationLine)e.Current);
            }
            e.Reset();
            for (int i = 0; i < max; i++)
            {
                e.MoveNext();
            }
            temp = (BuStationLine)e.Current;
            res.LStation = temp.GSStation;
            return res;
        }

        public bool setIDL(string ID)
        {

            bool check = false;
            double temp = 0.0;
            check = double.TryParse(ID, out temp);
            if (check)
                IDL = ID;
            else
                Console.WriteLine("#ERROR!#\nunvalid input!\n");
            return check;
        }

        public static areacode ToAreacode(string area)
        {
            switch (area)
            {
                case "general":
                    return areacode.general;
                case "North":
                    return areacode.North;
                case "south":
                    return areacode.south;
                case "center":
                    return areacode.center;
                case "jerusalem":
                    return areacode.jerusalem;
                default:
                    Console.WriteLine("#ERROR!#\nunvalid input!\n");
                    return areacode.general;
            }
        }

        public string AreaToString()
        {
            areacode area = this.area;
            switch (area)
            {
                case areacode.general:
                    return "general";
                case areacode.North:
                    return "North";
                case areacode.south:
                    return "south";
                case areacode.center:
                    return "center";
                case areacode.jerusalem:
                    return "jerusalem";
                default:
                    return "general";
            }
        }
        public BuStationLine addrandomstation()
        {
            Random r = new Random();
            int id = (r.Next() % 1000000);
            double KFL = (r.Next() % 1000);
            double TFL = (r.Next() % 1000);
            BuStationLine temp = new BuStationLine(new BuStation(id.ToString(), null), KFL, TFL);
            while (add(temp) < 0)
            {
                id = (r.Next() % 1000000);
                temp = new BuStationLine(new BuStation(id.ToString(), null), KFL, TFL);
            }

            return temp;
        }
        public void add40randomstation()
        {
            for (int i = 0; i < 40; i++)
            {
                addrandomstation();
            }
        }
        
    }
}
