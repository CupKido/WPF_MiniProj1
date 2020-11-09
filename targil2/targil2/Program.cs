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
            BLines a = new BLines();
           BusLine e = new BusLine();
            BusLine f = new BusLine();
            f.add(new BuStationLine(new BuStation("1111", 0, 0, null), 5, 34));
            f.add(new BuStationLine(new BuStation("2222", 0, 0, null), 4, 76));
            f.add(new BuStationLine(new BuStation("3333", 0, 0, null), 8, 23));
            f.add(new BuStationLine(new BuStation("4444", 0, 0, null), 3, 54));
            f.add(new BuStationLine(new BuStation("5555", 0, 0, null), 9, 38));
            e.add(new BuStationLine(new BuStation("1111", 0, 0, null), 5, 34));
            e.add(new BuStationLine(new BuStation("2222", 0, 0, null), 4,76));
            e.add(new BuStationLine(new BuStation("3333", 0, 0, null), 8, 23));
            e.add(new BuStationLine(new BuStation("4444", 0, 0, null), 3, 54));
            e.add(new BuStationLine(new BuStation("5555", 0, 0, null), 9, 38));
            a.AddLine(e);
            a.AddLine(f);
            a.RemoveLine(e);
            a.AddLine(f);
            BusLine newline = e.SubLine(2, 5);
            Console.WriteLine(e.DistanceB2(3, 4));
            Console.WriteLine(newline.DistanceB2(2, 3));
            Console.ReadKey();
        }
    }
}
