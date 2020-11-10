using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace targil2
{
    class Program
    {
        static void Main(string[] args)
        {
            BLines Buses;
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
                            Console.WriteLine("1. Add Bus line\n2. Add Bus station\n3. exit to main menu");
                            choice = Console.ReadLine();
                            switch(choice)
                            {
                                case "1":
                                    
                                    break;
                                case "2":

                                    break;
                                case "3":
                                    exit = true;
                                    break;
                                default:
                                    Console.WriteLine("#ERROR!#\nunvalid input!");
                                    break;
                            }
                        } while (!exit);
                        exit = false;
                        break;
                    case "2":
                        do
                        {
                            Console.WriteLine("1. Remove Bus line\n2. Remove Bus station\n3. exit to main menu");
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
                                    Console.WriteLine("#ERROR!#\nunvalid input!");
                                    break;
                            }
                        } while (!exit);
                        exit = false;
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("#ERROR!#\nunvalid input!");
                        break;
                }    

              


            } while (!exit);
            
        }
    }
}
