using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace targil3B
{
    class BUSES
    {
        ArrayList Buses = new ArrayList();

        public BUSES()
        {
            Buses.Clear();
        }
        public int index(BUS line)
        {
            int i = 0;
            foreach(BUS bus in Buses)
            {
                if(bus == line)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        public BUS index(int line)
        {
            if (line > Buses.Count)
                return null;
            return (BUS)Buses[line];
        }

        public int indexByID(string ID)
        {
            int i = 0;
            foreach (BUS bus in Buses)
            {
                if (bus.currentID == ID)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }
            static Random r = new Random();
        private BUS RRBus(DateTime today) //Return Random Bus
        {

            int day, month, year;

            year = (r.Next() % 11) + today.Year - 10;
            month = r.Next() % 12 +1;
            if(month == 2)
            {
                day = r.Next() % 28 + 1;
            }
            else if(month == 4 || month == 6 || month == 9 || month == 11)
            {
                day = r.Next() % 30 +1;
            }
            else
            {
                day = r.Next() % 31 +1;
            }
            DateTime StartDate = new DateTime(year, month, day);
            int timeActive = (today.Year - StartDate.Year);
            if(timeActive == 0)
            {
                timeActive = 1;
            }
            int added = r.Next() % timeActive;
            year = (added) + StartDate.Year;
            month = r.Next() % 12 +1;
            if (month == 2)
            {
                day = r.Next() % 28 +1;
            }
            else if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                day = r.Next() % 30 +1;
            }
            else
            {
                day = r.Next() % 31 +1;
            }
            
            DateTime lastCareDate = new DateTime(year, month, day);
            if (StartDate > lastCareDate)
            {
                lastCareDate = StartDate;
            }

            double km;
            km = r.Next() % 100000 + r.NextDouble();

            string ID;
            int intID;
            bool exist = true;
            do {
                if (year >= 2018)
                {
                    intID = r.Next(9999999, 100000000);
                }
                else
                {
                    intID = r.Next(999999, 10000000);
                }
                ID = intID.ToString();
                int IDl = ID.Length;
                //for (int i = 0; i < IDsize - IDl; i++)
                //{
                //   ID = ID.Insert(0, "0");
                //}
                if(indexByID(ID) == -1)
                {
                    exist = false;
                }
            } while (exist);
            

            BUS temp;

            temp = new BUS(ID, km, StartDate, lastCareDate);
            temp.addride();
            return temp;
        }
        public void Add10Randoms(DateTime today)
        {
            
            for(int i = 0; i < 10; i++)
            {
                Buses.Add(RRBus(today));
            }
        }
        public BUSES AddBus()
        {
            BUS[] temp = new BUS[Buses.Count + 1];
            

            BUS tempo = new BUS();

            DateTime bustart = new DateTime(); //starting date;
            int day, month, year = 0;
            bool isokay = false;
            while (!isokay)
            {
                isokay = true;
                Console.WriteLine("type date:");
                if (!(int.TryParse(Console.ReadLine(), out day))) { isokay = false; }
                if (!(int.TryParse(Console.ReadLine(), out month))) { isokay = false; }
                if (!(int.TryParse(Console.ReadLine(), out year))) { isokay = false; }
                if (isokay) { string str = day.ToString() + "/" + month.ToString() + "/" + year.ToString(); bustart = Convert.ToDateTime(str); }

                if (!isokay) { Console.WriteLine("\n******\nERROR\n******\n"); }
            }

            isokay = false;
            string ID = null;
            int trashint;
            while (!isokay)
            {
                Console.WriteLine("type ID:");
                ID = Console.ReadLine();
                isokay = int.TryParse(ID, out trashint);
                if (!isokay) { Console.WriteLine("\n******\nERROR\nONLY NUMBERS\n******\n"); }
                if (isokay)
                {
                    if (year < 2018 && ID.Length != 7)
                    {
                        Console.WriteLine("\n******\nERROR\n7 NUMBERS REQUIRED\n******\n");
                        isokay = false;
                    }
                    if (year >= 2018 && ID.Length != 8)
                    {
                        Console.WriteLine("\n******\nERROR\n8 NUMBERS REQUIRED\n******\n");
                        isokay = false;
                    }
                    if (Buses.Count > 0)
                        if (indexByID(ID) == -1)
                        {
                            Console.WriteLine("\n******\nERROR\nBUS IS ALREADY EXIST\n******\n");
                            isokay = false;
                        }
                }
            }

            tempo.SetBus(ID, bustart, true);
            Buses.Add(tempo);
            return this;
        }
        public List<BUS> ToList()
        {
            List<BUS> nLIST = new List<BUS>();
            foreach (BUS line in Buses)
            {
                nLIST.Add(line);
            }
            return nLIST;
        }
    }
}
