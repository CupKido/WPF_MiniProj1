﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace targil2 
{
    class BLines : IEnumerable
    {
        BusLine[] buslines = new BusLine[0];
        public BLines()
        {
            buslines = new BusLine[0];
        }
        public IEnumerator GetEnumerator()
        {
            return buslines.GetEnumerator();
        }
        public BusLine[] GSbuslines
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
            if(buslines.Length != 0)
            {
                int legit = LegitForAdd(line);
                if (legit != 1)
                {
                    return legit;
                }    
            }

            BusLine[] temp = new BusLine[buslines.Length + 1];
            int i = 0;
            foreach(BusLine bus in buslines)
            {
                temp[i] = bus;
                i++;
            }
            temp[i] = line;
            buslines = temp;
            return 0;
        }
        public void RemoveLine(BusLine line)
        {
            int location = searchLine(line);
            if(location == -1)
            { return; }
            BusLine[] temp = new BusLine[buslines.Length-1];
            int i = 0;
            int j = 0;
            foreach(BusLine busLine in buslines)
            {
                if(i != location)
                {
                    temp[j] = busLine;
                    j += 1;
                }
                i += 1;
            }
            buslines = temp;
        }

    }
}
