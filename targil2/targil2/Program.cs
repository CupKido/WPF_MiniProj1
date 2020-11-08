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
            BuStation num1 = new BuStation("123456", null);
            Console.WriteLine(num1.ToString());
            Console.ReadKey();
        }
    }
}
