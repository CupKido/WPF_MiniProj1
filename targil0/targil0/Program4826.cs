using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcome4826();
            welcome8139();
            Console.ReadKey();
        }
        static partial void welcome8139();
        private static void welcome4826()
        {
            Console.Write("enter your name: ");
            string name = Console.ReadLine();
            Console.Write("{0} welcome to my first console application", name);
        }
    }
}
