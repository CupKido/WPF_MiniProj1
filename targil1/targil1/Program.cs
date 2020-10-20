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
            while (inmenu)
            {
                numchosen = Pleasechoose();
                switch (numchosen)
                {
                    case 1:
                        addbus(ref buses, ref listsize);
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
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
            string MW = "please choose an action: "; // menu welcome
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

            tempo.SetBus(ID, bustart);
            temp[size] = tempo;
            buses = temp;
            size = size + 1;
            for(int i = 0; i < size; i++)
            {
                Console.WriteLine("{0} , {1}", buses[i].currentkm, buses[i].currentID);
            }
        }
    }
}
