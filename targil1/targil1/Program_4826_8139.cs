using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace targil1
{
    class Program
    {
        static void Main(string[] args)
        {
            BUS[] buses = new BUS[0];
            int listsize = 0;
            bool inmenu = true;
            int numchosen = '\0';
            Date today = new Date();
            today.askDate("today's");
            while (inmenu)
            {
                numchosen = Pleasechoose();
                switch (numchosen)
                {
                    case 1:
                        addbus(ref buses, ref listsize);
                        break;
                    case 2:
                        addride(ref buses, today);
                        break;
                    case 3:
                        FillOrRepair(ref buses, today);
                        break;
                    case 4:
                        printBuses(buses);
                        break;
                    case 5:
                        inmenu = false;
                        break;
                    default:
                        numchosen = Pleasechoose();
                        break;
                }
            }
            Console.WriteLine("thank you\nhave a nice day!");
            Console.ReadKey();
        }
        public static int Pleasechoose()
        {
            int chosen = 0; //chosen choice
            string choice = null;
            bool CA = false; //correct answer
            bool ap; // approved
            string MW = "please choose an action:\n 1 - add new bus\n 2 - go for a ride\n 3 - refill gas/repair\n 4 - info about all buses\n 5 - exit "; // menu welcome
            while (!CA)
            {
                Console.WriteLine(MW);
                choice = Console.ReadLine();
                ap = int.TryParse(choice, out chosen);
                if(chosen >= 1 && chosen <= 5 && ap) 
                {
                CA = true; 
                }
                else
                {
                    Console.WriteLine("\n******\nERROR\n******\n");
                }
                ap = false;
            }
            return chosen;
        }
        public static void addbus(ref BUS[] buses, ref int size)
        {
            BUS[] temp = new BUS[size+1];
            for(int i = 0; i < size; i++)
            {
                temp[i] = buses[i];
            }
           

            BUS tempo = new BUS();

            Date bustart = new Date(); //starting date;
            int day, month, year = 0;
            bool isokay = false;
            while (!isokay)
            {
                isokay = true;
                Console.WriteLine("type date:");
                if (!(int.TryParse(Console.ReadLine(), out day))) { isokay = false; }
                if (!(int.TryParse(Console.ReadLine(), out month))) { isokay = false; }
                if (!(int.TryParse(Console.ReadLine(), out year))) { isokay = false; }
                if (isokay) { if (!bustart.IsOkay(day, month, year)) { isokay = false; } }
                
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
                    if(year < 2018 && ID.Length != 7) 
                    { 
                        Console.WriteLine("\n******\nERROR\n7 NUMBERS REQUIRED\n******\n");
                        isokay = false;
                    }
                    if (year >= 2018 && ID.Length != 8)
                    {
                        Console.WriteLine("\n******\nERROR\n8 NUMBERS REQUIRED\n******\n");
                        isokay = false;
                    }
                }
            }

            tempo.SetBus(ID, bustart, true);
            temp[size] = tempo;
            buses = temp;
            size = size + 1;

        }
        public static void addride(ref BUS[] buses, Date today)
        {
            if (buses.Length == 0)
            {
                Console.WriteLine("\n******\nERROR\nNO BUSES IN LIST\n******\n");
                return;
            }
            bool isokay = false;
            string ID = null;
            int trashint;
            int location = 0;
            while (!isokay)
            {
                Console.WriteLine("type ID:");
                ID = Console.ReadLine();
                if (ID == "x") { return; }
                isokay = int.TryParse(ID, out trashint);
                if (!isokay) { Console.WriteLine("\n******\nERROR\nONLY NUMBERS\n******\n"); }
                else
                {
                    if(ID.Length > 8 || ID.Length < 7) { 
                        Console.WriteLine("\n******\nERROR\n7-8 NUMBERS ONLY\n******\n");
                        isokay = false;
                    }
                }
                location = buses[0].find(ID, buses) - 1;
                if (location == -2) {
                    Console.WriteLine("\n******\nERROR\nCOULDNT FIND WANTED BUS\n******\n");
                    isokay = false;
                }

            }
            
            Random r = new Random();
            double km = r.Next(0, 1200);
            if (buses[location].addkm(km, today))
            {
                Console.WriteLine("ride distance: {0} kilometers\noverall km: {1}\nkm so far: {2}", km, buses[location].kmfs, buses[location].currentkm);
            }

            
        }
        public static void FillOrRepair(ref BUS[] buses, Date today)
        {
            if (buses.Length == 0)
            {
                Console.WriteLine("\n******\nERROR\nNO BUSES IN LIST\n******\n");
                return;
            }


            bool i3m = true; // in 3rd menu
            int choice = 0;
            bool good;
            while (i3m)
            {
                i3m = false;
                Console.WriteLine("please select action:\n1 for gaz refill\n2 for bus repair");
                good = int.TryParse(Console.ReadLine(), out choice);
                if(!good ||(choice != 1 && choice != 2))
                {
                    string onetwo = null;
                    if(choice != 1 && choice != 2)
                    {
                        onetwo = "BETWEEN 1 OR 2";
                    }
                    Console.WriteLine("******\nERROR\nONLY NUMBERS {0}\n******", onetwo);
                }
            }
            int location = findBus(buses);
            if (choice == 1)
            {
                if(buses[location].fillGaz() == -1) { Console.WriteLine("GAZ IS ALLREADY FULL!");
                    return;
                }
                Console.WriteLine("GAZ HAS BEEN FILLED!");
                if (buses[location].treatmentneeded(today))
                {
                    Console.WriteLine("REPAIR NEEDED!");
                }
            }
            if(choice == 2)
            {
                buses[location].repair(today);
                Console.WriteLine("The Bus is ready to go!");
            }
        }
        public static int findBus(BUS[] buses)
        {
            bool isokay = false;
            string ID = null;
            int trashint;
            int location = -1;
            while (!isokay)
            {
                Console.WriteLine("type ID:");
                ID = Console.ReadLine();
                if (ID == "x") { return -1; }
                isokay = int.TryParse(ID, out trashint);
                if (!isokay) { Console.WriteLine("\n******\nERROR\nONLY NUMBERS\n******\n"); }
                else
                {
                    if (ID.Length > 8 || ID.Length < 7)
                    {
                        Console.WriteLine("\n******\nERROR\n7-8 NUMBERS ONLY\n******\n");
                        isokay = false;
                    }
                }
                location = buses[0].find(ID, buses) - 1;
                if (location == -2)
                {
                    Console.WriteLine("\n******\nERROR\nCOULDNT FIND WANTED BUS\n******\n");
                    isokay = false;
                }

            }
            return location;
        }
        public static void printBuses(BUS[] buses)
        {
            if (buses.Length == 0)
            {
                Console.WriteLine("\n******\nERROR\nNO BUSES IN LIST\n******\n");
                return;
            }
            for (int i = 0; i<buses.Length;i++)
            {
                buses[i].printBus();
            }
            return;
        }
    }
}
