//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Emit;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace targil2
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            BLines Buses = new BLines();
//            BusLine allstats = new BusLine("XXXXXX", null, null);
//            allstats.add40randomstation();
//            Buses.add10randomFromLine(allstats);
//            string choice = null;
//            bool exit = false;
//            bool check = false;
//            int tempi = 0;
//            double tempd = 0.0;
//            bool tempb = false;
//            do
//            {
//                Console.WriteLine("1. Add Bus station/line\n2. Remove Bus station/line\n3. search\n4. print\n5. exit");
//                choice = Console.ReadLine();
//                switch (choice)
//                {
//                    case "1":
//                        do
//                        {
//                            Console.WriteLine("1. Add Bus line\n2. Add Bus station \n3. exit to main menu\n");
//                            choice = Console.ReadLine();
//                            switch (choice)
//                            {
//                                case "1":
//                                    BusLine bl = new BusLine();
//                                    BuStationLine btemp = new BuStationLine();
//                                    Console.WriteLine("please enter line ID:\n");
//                                    while (!check)
//                                    {
//                                        check = bl.setIDL(Console.ReadLine()); //insert line ID and check if it's an valid ID
//                                    }
//                                    bl.GSFStation.add("first");// insert a first station and check if its an valid station
//                                    btemp.GSStation = bl.GSFStation;
//                                    bl.add(btemp);// insert the station into line stations array
//                                    btemp = new BuStationLine();// reset the btemp var
//                                    btemp.add("last"); //check if the station is an valid station
//                                    bl.GSLStation = btemp.GSStation;
//                                    bl.add(btemp);// insert a last statiton 
//                                    do
//                                    {
//                                        Console.WriteLine("do you want to set an area ?\n1. yes \n2. no\n------------------------------------------------------\nNOTICE:if you will not set the area,\nit will be automaticly set to general\n------------------------------------------------------\n");
//                                        choice = Console.ReadLine();
//                                        switch (choice)
//                                        {
//                                            case "1":
//                                                do
//                                                {
//                                                    Console.WriteLine("which area the line is pass throw?\n1. general \n2. North\n3. South\n4. Center\n5. Jerusalem\n6. exit to main menu");
//                                                    choice = Console.ReadLine();
//                                                    switch (choice)
//                                                    {
//                                                        case "1":
//                                                            bl.GSarea = BusLine.areacode.general;
//                                                            exit = true;
//                                                            break;
//                                                        case "2":
//                                                            bl.GSarea = BusLine.areacode.North;
//                                                            exit = true;
//                                                            break;
//                                                        case "3":
//                                                            bl.GSarea = BusLine.areacode.south;
//                                                            exit = true;
//                                                            break;
//                                                        case "4":
//                                                            bl.GSarea = BusLine.areacode.center;
//                                                            exit = true;
//                                                            break;
//                                                        case "5":
//                                                            bl.GSarea = BusLine.areacode.jerusalem;
//                                                            exit = true;
//                                                            break;
//                                                        case "6":
//                                                            exit = true;
//                                                            break;
//                                                        default:
//                                                            Console.WriteLine("#ERROR!#\nunvalid input!\n");
//                                                            break;
//                                                    }
//                                                } while (!exit);
//                                                break;
//                                            case "2":
//                                                exit = true;
//                                                break;
//                                            default:
//                                                Console.WriteLine("#ERROR!#\nunvalid input!\n");
//                                                break;
//                                        }
//                                    } while (!exit);
//                                    Buses.AddLine(bl);// insert the new line into the buses array
//                                    check = false;
//                                    break;
//                                case "2":
//                                    BuStationLine bsltemp = new BuStationLine();
//                                    BuStationLine bsltemp1 = new BuStationLine();
//                                    BusLine bus = new BusLine();
//                                    string ID = null;
//                                    string area = null;
//                                    while (!check)
//                                    {
//                                        Console.WriteLine("please enter line ID\n");
//                                        ID = Console.ReadLine();
//                                        bus.GSID = ID;
//                                        if (bus.GSID == ID)
//                                            check = true;
//                                    }
//                                    check = false;
//                                    while (!check)
//                                    {
//                                        Console.WriteLine("please enter line area\n");
//                                        area = Console.ReadLine();
//                                        bus.GSarea = BusLine.ToAreacode(area);
//                                        if (bus.AreaToString() == area)
//                                            check = true;
//                                    }
//                                    bus = Buses.searchLine(bus.GSID,bus.AreaToString());
//                                    if (bus != null)
//                                    {
//                                        bsltemp.GSStation.add("the new");   
//                                        do
//                                        {
//                                            Console.WriteLine("where do you want to add the station?\n1. at the start of the line\n2. at the middle\n3. at the end of the line\n");
//                                            choice = Console.ReadLine();
//                                            switch (choice)
//                                            {
//                                                case "1":
//                                                    bus.GSFStation = bsltemp.GSStation;
//                                                    bsltemp1 = (BuStationLine)bus.GStations[0];
//                                                    bsltemp1.addkt("to next");
//                                                    bus.GStations.Insert(0, bsltemp);
//                                                    Console.WriteLine("--------------------------\nstation added succesfuly!\n--------------------------\n");
//                                                    exit = true;
//                                                    break;
//                                                case "2":
//                                                    break;
//                                                case "3":
//                                                    exit = true;
//                                                    break;
//                                                default:
//                                                    Console.WriteLine("#ERROR!#\nunvalid input!\n");
//                                                    break;
//                                            }
//                                        } while (!exit);
//                                    }
//                                    else
//                                    {
//                                        Console.WriteLine("#ERROR!#\nbus not found! please try again\n");
//                                    }
//                                    break;
//                                case "3":
//                                    exit = true;
//                                    break;
//                                default:
//                                    Console.WriteLine("#ERROR!#\nunvalid input!\n");
//                                    break;
//                            }
//                        } while (!exit);
//                        exit = false;
//                        break;
//                    case "2":
//                        do
//                        {
//                            Console.WriteLine("1. Remove Bus line\n2. Remove Bus station\n3. exit to main menu\n");
//                            choice = Console.ReadLine();
//                            switch (choice)
//                            {
//                                case "1":
//                                    removebusline(Buses);
//                                    break;
//                                case "2":
//                                    removestatfromline(Buses);
//                                    break;
//                                case "3":
//                                    exit = true;
//                                    break;
//                                default:
//                                    Console.WriteLine("#ERROR!#\nunvalid input!\n");
//                                    break;
//                            }
//                        } while (!exit);
//                        exit = false;
//                        break;
//                    case "3":
//                        do
//                        {
//                            Console.WriteLine("1. search by station number\n2. search for route between two stations \n3. exit to main menu");
//                            choice = Console.ReadLine();
//                            switch (choice)
//                            {
//                                case "1":
//                                    gothroughstat(Buses, allstats);
                                    
//                                    break;
//                                case "2":
//                                    TimeB2(Buses, allstats);
//                                    break;
//                                case "3":
//                                    exit = true;
//                                    break;
//                                default:
//                                    Console.WriteLine("#ERROR!#\nunvalid input!\n");
//                                    break;
//                            }
//                        } while (!exit);
//                        exit = false;
//                        break;
//                    case "4":
//                        do
//                        {
//                            Console.WriteLine("1. print all the lines\n2. print all the stations with info \n3. exit to main menu");
//                            choice = Console.ReadLine();
//                            switch (choice)
//                            {
//                                case "1":
//                                    allbusesprint(Buses);
//                                    break;
//                                case "2":
//                                    gothroughallstat(Buses, allstats);
//                                    break;
//                                case "3":
//                                    exit = true;
//                                    break;
//                                default:
//                                    Console.WriteLine("#ERROR!#\nunvalid input!\n");
//                                    break;
//                            }
//                        } while (!exit);
//                        exit = false;
//                        break;
//                    case "5":
//                        exit = true;
//                        break;
//                    default:
//                        Console.WriteLine("#ERROR!#\nunvalid input!\n");
//                        break;
//                }




//            } while (!exit);

//        }
//        public static void removebusline(BLines Buses)
//        {
//            if(Buses.amount() == 0)
//            {
//                Console.WriteLine("ERROR\nNo Buses to remove");
//                return;
//            }
//            bool found = false;
//            string ID;
//            do
//            {
//                Console.WriteLine("Please enter Line ID to remove:");
//                ID = Console.ReadLine();
//                foreach(BusLine line in Buses)
//                {
//                    if(line.GSID == ID)
//                    {
//                        Buses.RemoveLine(line);
//                        found = true;
//                        Console.WriteLine("Line removed succesfully!");
//                        break;
//                    }   
                    
//                }
//                if (!found)
//                {
//                    Console.WriteLine("ERROR\nNOT FOUND");
//                }
//            } while (!found);
//        }
//        public static void removestatfromline(BLines Buses)
//        {
//            if (Buses.amount() == 0)
//            {
//                Console.WriteLine("ERROR\nNo Buses to remove");
//                return;
//            }
//            bool foundLine = false;
//            bool foundstat = false;
//            bool statinline = true;
//            string lineID;
//            string statID;
//            int i = 0;

//            do
//            {
//                statinline = true;
//                Console.WriteLine("Please enter Line ID to remove station from:");
//                lineID = Console.ReadLine();

//                foreach (BusLine line in Buses)
//                {

//                    if (line.GSID == lineID)
//                    {
//                        foundLine = true;
//                        if (line.GStations.Count > 0)
//                        {
//                            do
//                            {
//                                Console.WriteLine("Please enter station ID to remove from Line {0}:", lineID);
//                                statID = Console.ReadLine();

//                                foreach (BuStationLine station in line.GStations)
//                                {
//                                    if (station.GSStation.GSID == statID)
//                                    {

//                                        foundstat = true;
//                                        break;
//                                    }
//                                    i++;
//                                }
//                                if (!foundstat)
//                                {
//                                    Console.WriteLine("ERROR\nSTATION NOT FOUND");
//                                }
//                            } while (!foundstat);
//                        }
//                        else
//                        {  
//                            Console.WriteLine("ERROR\nNO STATIONS IN LINE");
//                            statinline = false;
//                            foundLine = false;
//                        }
//                        if (foundstat) { line.newdelete(i); }
//                        break;
//                    }
//                }

//                if (!foundLine && statinline)
//                {
//                    Console.WriteLine("ERROR\nLINE NOT FOUND");
//                }

//            } while (!foundLine);
//        }
//        public static void gothroughstat(BLines Buses, BusLine allstats)
//        {
//            bool foundstat = false;
//            string ID = null;
//            while (!foundstat)
//            {
//                Console.WriteLine("please enter the wanted station's ID number: ");
//                ID = Console.ReadLine();
//                if (allstats.SIS(ID) < 0)
//                {
//                    Console.WriteLine("ERROR! \n station not found");
//                }
//                else { foundstat = true; }
//            }
//            Console.WriteLine("for station number {0}: {1}", ID, Buses.gothroughstat(ID).ReturnStringLines());
//        }
//        public static void gothroughallstat(BLines Buses, BusLine allstats)
//        {
//            bool foundstat = false;
//            foreach(BuStationLine stat in allstats.GStations)
//            {
//                Console.WriteLine("for station number {0}: {1}", stat.GSStation.GSID, Buses.gothroughstat(stat.GSStation.GSID).ReturnStringLines());
//            }
//        }
//        public static void allbusesprint(BLines Buses)
//        {
//            if(Buses.GSbuslines.Count == 0)
//            {
//                Console.WriteLine("ERROR! \nNO LINES FOUND!");
//                return;
//            }
//            Console.WriteLine(Buses.allLines() + "\n");
//        }
//        public static void TimeB2(BLines Buses, BusLine allstats)
//        {
//            bool foundstat1 = false, foundstat2 = false;
//            string ID1 =null, ID2 = null;
//            while(!foundstat1 || !foundstat2)
//            {
//                if(!foundstat1)
//                {
//                    Console.WriteLine("please enter the first station's ID:");
//                    ID1 = Console.ReadLine();
//                    if(allstats.SIS(ID1) > -1)
//                    {
//                        foundstat1 = true;
//                    }
//                }
//                if (!foundstat2)
//                {
//                    Console.WriteLine("please enter the second station's ID:");
//                    ID2 = Console.ReadLine();
//                    if (allstats.SIS(ID2) > -1)
//                    {
//                        foundstat2 = true;
//                    }
//                }
//                if(!foundstat1)
//                {
//                    Console.WriteLine("ERROR\ncouldnt find the first station");
//                }
//                if (!foundstat2)
//                {
//                    Console.WriteLine("ERROR\ncouldnt find the second station");
//                }
//            }
//            Console.WriteLine(Buses.timeB2stats(ID1, ID2));

//        }
//    }
//}
