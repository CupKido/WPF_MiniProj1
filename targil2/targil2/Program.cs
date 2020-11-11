using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace targil2
{
    class Program
    {
        static void Main(string[] args)
        {
            BLines Buses = new BLines();
            string choice = null;
            bool exit = false;
            do {
                Console.WriteLine("1. Add Bus station/line\n2. Remove Bus station/line\n3. search\n4. print\n5. exit");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        do
                        {
                            Console.WriteLine("1. Add Bus line\n2. Add Bus station\n3. exit to main menu\n");
                            choice = Console.ReadLine();
                            switch(choice)
                            {
                                case "1":
                                    BusLine bl = new BusLine();
                                    int temp = 0;
                                    BuStationLine btemp = new BuStationLine();
                                    double KTtemp = 0.0;
                                    Console.WriteLine("please enter line ID:\n");
                                    bool check = false;
                                    while (!check)
                                    {
                                        check = bl.setIDL(Console.ReadLine()); //insert line ID and check if it's an valid ID
                                    }
                                    bl.GSFStation.add("first");// insert a first station and check if it's an valid station
                                    btemp.GSStation = bl.GSFStation;
                                    bl.add(btemp);// insert the station into line stations array
                                    btemp = new BuStationLine();// reset the btemp var
                                    bl.GSLStation.add("last");// insert a last statiton and check if its an valid station
                                    btemp.GSStation = bl.GSLStation;
                                    check = false;
                                    Console.WriteLine("please enter distance from prev satition:\n");
                                    while (!check)
                                    {
                                        check = double.TryParse(Console.ReadLine() ,out KTtemp);
                                        if(!check)
                                            Console.WriteLine("#ERROR!#\nunvalid input!\n");
                                    }
                                    btemp.GSKFL = KTtemp;
                                    check = false;
                                    Console.WriteLine("please enter time from prev station:\n");
                                    while (!check)
                                    {
                                        check = double.TryParse(Console.ReadLine(), out KTtemp);
                                        if (!check)
                                            Console.WriteLine("#ERROR!#\nunvalid input!\n");
                                    }
                                    btemp.GSTFL = KTtemp;
                                    bl.add(btemp);
                                    Buses.AddLine(bl);// insert the new line into the buses array
                                    break;
                                case "2":

                                    break;
                                case "3":
                                    exit = true;
                                    break;
                                default:
                                    Console.WriteLine("#ERROR!#\nunvalid input!\n");
                                    break;
                            }
                        } while (!exit);
                        exit = false;
                        break;
                    case "2":
                        do
                        {
                            Console.WriteLine("1. Remove Bus line\n2. Remove Bus station\n3. exit to main menu\n");
                            choice = Console.ReadLine();
                            switch (choice)
                            {
                                case "1":
                                    break;
                                case "2":
                                    break;
                                case "3":
                                    exit = true;
                                    break;
                                default:
                                    Console.WriteLine("#ERROR!#\nunvalid input!\n");
                                    break;
                            }
                        } while (!exit);
                        exit = false;
                        break;
                    case "3":
                        do
                        {
                            Console.WriteLine("1. search by station number\n2. search for route between two stations \n3. exit to main menu\n");
                            choice = Console.ReadLine();
                            switch (choice)
                            {
                                case "1":

                                    break;
                                case "2":

                                    break;
                                case "3":
                                    exit = true;
                                    break;
                                default:
                                    Console.WriteLine("#ERROR!#\nunvalid input!\n");
                                    break;
                            }
                        } while (!exit);
                        exit = false;
                        break;
                    case "4":
                        do
                        {
                            Console.WriteLine("1. print all the lines\n2. print all the stations with info \n3. exit to main menu\n");
                            choice = Console.ReadLine();
                            switch (choice)
                            {
                                case "1":

                                    break;
                                case "2":

                                    break;
                                case "3":
                                    exit = true;
                                    break;
                                default:
                                    Console.WriteLine("#ERROR!#\nunvalid input!\n");
                                    break;
                            }
                        } while (!exit);
                        exit = false;
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("#ERROR!#\nunvalid input!\n");
                        break;
                }    

              


            } while (!exit);
            
        }
    }
}
