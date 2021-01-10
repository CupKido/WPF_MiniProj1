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
        }

    }
}
