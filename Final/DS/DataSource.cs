using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using DO;

namespace DS
{
    static public class DataSource
    {
        public static List<BUS> ListBuses;
        public static List<Line> ListLines;
        public static List<Trip> ListTrips;
        public static List<BusOnTrip> ListBusesOnTrips;
        public static List<Station> ListStations;
        public static List<User> ListUsers;
        public static List<LineStation> ListLineStations;
        public static List<LineTrip> ListLineTrip;
        public static List<AdjacentStations> ListAdjacent;

        static DataSource()
        {
            //insert random generator here
            clear();
        }

        static void clear()
        {
            ListBuses = new List<BUS>();
            ListBuses.Add(new BUS(12345678, new DateTime(), new DateTime(), 123));
            ListLines =  new List<Line>();
            ListTrips = new List<Trip>();
            ListBusesOnTrips = new List<BusOnTrip>();
            ListStations = new List<Station>();
            ListUsers = new List<User>();
            ListLineStations = new List<LineStation>();
            ListLineTrip = new List<LineTrip>();
            ListAdjacent = new List<AdjacentStations>();
        }

    }
}
