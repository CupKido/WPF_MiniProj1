using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace targil2 
{
    class BLines : IEnumerable
    {
        ArrayList buslines = new ArrayList();
        public BLines()
        {
            ArrayList buslines = new ArrayList();
        }
        public IEnumerator GetEnumerator()
        {
            return buslines.GetEnumerator();
        }
        public ArrayList GSbuslines
        {
            get
            {
                return buslines;
            }
            set
            {
                buslines = value;
            }
        }
        public int searchLine(BusLine Line)
        {
            int i = 0;
            foreach (BusLine busLine in buslines)
            {
                if ((busLine.GSID == Line.GSID))
                {
                    if ((busLine.GSLStation == Line.GSLStation) && (busLine.GSFStation == Line.GSFStation))
                    {
                        return i; // line found
                    }
                }
                i += 1;
            }
            return -1;
        }
        public BusLine searchLine(string ID, string area)
        {

            foreach (BusLine busLine in buslines)
            {
                if ((busLine.GSID == ID && busLine.AreaToString() == area))
                {
                    return busLine;
                }

            }
            return null;
        }
        public BusLine searchLine(int location)
        {
            if (location > buslines.Count)
            {
                return null;
            }
            int i = 0;
            foreach (BusLine busLine in buslines)
            {
                if (i == location)
                {
                    return busLine;
                }
                i += 1;
            }
            return null;
        }
        public int LegitForAdd(BusLine Line)
        {
            int i = 0;
            foreach (BusLine busLine in buslines)
            {
                if ((busLine.GSID == Line.GSID))
                {
                    if ((busLine.GSLStation == Line.GSLStation) && (busLine.GSFStation == Line.GSFStation))
                    {
                        return -2; // line exists
                    }
                    if (!(busLine.GSLStation == Line.GSFStation) || !(busLine.GSFStation == Line.GSLStation))
                    {
                        return -3; //ID taken
                    }
                }
                i += 1;
            }
            return 1;
        }
        public int AddLine(BusLine line)
        {

            if (buslines.Count != 0)
            {
                int legit = LegitForAdd(line);
                if (legit != 1)
                {
                    return legit;
                }
            }
            buslines.Add(line);
            //BusLine[] temp = new BusLine[buslines.Length + 1];
            //int i = 0;
            //foreach(BusLine bus in buslines)
            //{
            //    temp[i] = bus;
            //    i++;
            //}
            //temp[i] = line;
            //buslines = temp;
            return 0;
        }
        public int RemoveLine(BusLine line)
        {
            int location = searchLine(line);
            if (location == -1)
            { return -1; }
            buslines.RemoveAt(location);
            return 0;
            //BusLine[] temp = new BusLine[buslines.Length-1];
            //int i = 0;
            //int j = 0;
            //foreach(BusLine busLine in buslines)
            //{
            //    if(i != location)
            //    {
            //        temp[j] = busLine;
            //        j += 1;
            //    }
            //    i += 1;
            //}
            //buslines = temp;
        }
        public BLines PAS(BuStation stat) // passes in stations
        {
            BLines temp = new BLines();
            foreach (BusLine busline in buslines)
            {
                foreach (BuStationLine bustat in busline.GStations)
                {
                    if (bustat.GSStation.GSID == stat.GSID)
                    {
                        temp.AddLine(busline);
                    }
                }
            }
            return temp;
        }
        public BLines sortbytime()
        {
            BLines n = new BLines();
            n.buslines = buslines;
            n.buslines.Sort();
            return n;
        }
        public BusLine addrandom()
        {
            Random r = new Random();
            int id;
            BusLine temp = new BusLine();
            do
            {
                id = r.Next() % 1000000;
                temp = new BusLine(id.ToString());
            } while (LegitForAdd(temp) < 0);
            temp.add40randomstation();
            AddLine(temp);
            return temp;
        }
        public void add10random()
        {
            for(int i = 0; i<10;i++)
            {
                addrandom();
            }
        }
        public void add10randomFromLine(BusLine allstats)
        {
                                 
            for (int i = 0; i < 10; i++)
            {
                addrandomnostats();
            }
            foreach(BuStationLine stat in allstats.GStations)
            {
                GetRandomLine().add(stat);
            }
            foreach (BuStationLine stat in allstats.GStations)
            {
                GetRandomLine(stat).add(stat);
            }
        }
        public BusLine addrandomnostats()
        {
            Random r = new Random();
            int id;
            BusLine temp = new BusLine();
            do
            {
                id = r.Next() % 1000000;
                temp = new BusLine(id.ToString());
            } while (LegitForAdd(temp) < 0);
            AddLine(temp);
            return temp;
        }
        public BusLine GetRandomLine()
        {
            Random r = new Random();
            int num = r.Next() % buslines.Count;
            int i = 0;
            foreach(BusLine line in buslines)
            {
                if(i == num)
                {
                    return line;
                }
                i++;
            }
            return null;
        }
        public BusLine GetRandomLine(BuStationLine stat)
        {
            Random r = new Random();
            int num= 0;
            int i = 0;
            bool allready = true;
            while(allready)
            {
                num = r.Next() % buslines.Count;
                i = 0;
                foreach (BusLine line in buslines)
                {
                    if (i == num)
                    {
                        if(line.SIS(stat.GSStation.GSID) > -1)
                        {
                            allready = true;
                        }
                        else { 
                            return line;
                            allready = false;
                        }
                    }
                    i++;
                }
            }
            
            return null;
        }
        public int amount()
        {
            return buslines.Count;
        }
        public BLines gothroughstat(BuStation stat)
        {
            if(buslines.Count == 0)
            {
                Console.WriteLine("ERROR!\nNO BUSES FOUND");
                return null;
            }
            BLines buses = new BLines();
            foreach(BusLine line in buslines)
            {
                if(line.SIS(stat.GSID) > -1)
                {
                    buses.AddLine(line);
                }
            }
            return buses;
        }
        public BLines gothroughstat(string ID)
        {
            if (buslines.Count == 0)
            {
                Console.WriteLine("ERROR!\nNO BUSES FOUND");
                return null;
            }
            BLines buses = new BLines();
            foreach (BusLine line in buslines)
            {
                if (line.SIS(ID) > -1)
                {
                    buses.AddLine(line);
                }
            }
            return buses;
        }
        public string ReturnStringLines()
        {
            string res = "";
            foreach(BusLine line in buslines)
            {
                res = res + "\n" + line;
            }
            return res;
        }
    }
}
