using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace targil2 
{
    class BLines : IEnumerable, IComparable
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
            foreach(BusLine busLine in buslines)
            {
                if((busLine.GSID == Line.GSID)  )
                {
                    if((busLine.GSLStation == Line.GSLStation) && (busLine.GSFStation == Line.GSFStation))
                    {
                        return i; // line found
                    }
                }
                i += 1;
            }
            return -1;
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
            
            if(buslines.Count != 0)
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
        public void RemoveLine(BusLine line)
        {
            int location = searchLine(line);
            if(location == -1)
            { return; }

            buslines.RemoveAt(location);

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
            foreach(BusLine busline in buslines)
            {
                foreach(BuStationLine bustat in busline.GStations)
                {
                    if(bustat.GSStation.GSID == stat.GSID)
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
    }
}
