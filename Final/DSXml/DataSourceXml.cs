using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DO;
namespace DSXml
{
    public class DataSourceXml
    {
        public static XElement ListBuses;
        public static string busesPath = @"BusesXml.xml";
        public static XElement ListLines;
        public static string linesPath = @"LinesXml.xml";
        public static XElement ListTrips;
        public static string TripsPath = @"TripsXml.xml";
        public static XElement BusOnTrip;
        public static string BusOnTripPath = @"BusOnTripXml.xml";
        public static XElement ListStations;
        public static string StationsPath = @"StationsXml.xml";
        public static XElement ListUsers;
        public static string UsersPath = @"UsersXml.xml";
        public static XElement ListLineStations;
        public static string LineStationPath = @"LineStationsXml.xml";
        public static XElement ListLineTrip;
        public static string LineTripPath = @"LineTripsXml.xml";
        public static XElement ListAdjacent;
        public static string AdjacentPath = @"AdjacentXml.xml";

        static DataSourceXml()
        {
            if (!File.Exists(busesPath))
                CreateFileBusOnTrip();
            if (!File.Exists(linesPath))
                CreateFileBuses();
            if (!File.Exists(TripsPath))
                CreateFileLines();
            if (!File.Exists(BusOnTripPath))
                CreateFileTrips();
            if (!File.Exists(StationsPath))
                CreateFileStations();
            if (!File.Exists(UsersPath))
                CreateFileUsers();
            if (!File.Exists(LineStationPath))
                CreateFileLineStations();
            if (!File.Exists(LineTripPath))
                CreateFileLineTrips();
            if (!File.Exists(AdjacentPath))
                CreateFileAdjacent();
            loadAll();
            InitAllElements();

        }

        #region Create  Files
        private static void CreateFileBusOnTrip()
        {
            BusOnTrip = new XElement("BusOnTrip");
            BusOnTrip.Save(busesPath);
        }

        private static void CreateFileBuses()
        {
            BusOnTrip = new XElement("Buses");
            BusOnTrip.Save(linesPath);
        }

        private static void CreateFileLines()
        {
            BusOnTrip = new XElement("Lines");
            BusOnTrip.Save(TripsPath);
        }

        static private void CreateFileTrips()
        {
            BusOnTrip = new XElement("Trips");
            BusOnTrip.Save(BusOnTripPath);
        }

        private static void CreateFileStations()
        {
            BusOnTrip = new XElement("Stations");
            BusOnTrip.Save(StationsPath);
        }

        private static void CreateFileUsers()
        {
            BusOnTrip = new XElement("users");
            BusOnTrip.Save(UsersPath);
        }

        private static void CreateFileLineStations()
        {
            BusOnTrip = new XElement("LineStations");
            BusOnTrip.Save(UsersPath);
        }
        private static void CreateFileLineTrips()
        {
            BusOnTrip = new XElement("LineTrips");
            BusOnTrip.Save(UsersPath);
        }
        private static void CreateFileAdjacent()
        {
            BusOnTrip = new XElement("Adjacent");
            BusOnTrip.Save(UsersPath);
        }
        #endregion

        private static void InitAllElements()
        {


        }


        public static void SaveCourse() { ListBuses.Save(busesPath); }
        static public void loadAll()
        {
            LoadBuses();
            LoadLines();
            LoadTrips();
            LoadBusOnTrip();
            LoadStations();
            LoadUsers();
        }

        public static void LoadUsers()
        {
            ListUsers = XElement.Load(UsersPath);
        }

        public static void LoadStations()
        {
            ListStations = XElement.Load(StationsPath);
        }

        public static void LoadTrips()
        {
            ListTrips = XElement.Load(TripsPath);
        }

        public static void LoadLines()
        {
            ListLines = XElement.Load(linesPath);
        }

        public static void LoadBuses()
        {
            ListBuses = XElement.Load(busesPath);
        }

        public static void LoadBusOnTrip()
        {
            BusOnTrip = XElement.Load(BusOnTripPath);
        }

        static List<Line> ListPersons = new List<Line>
            {
                new Line
                {
                    FirstStation = 1111,
                    ID = 36,
                    LastStation = 2222,
                    Area = Areas.center
                },

                new Line
                {
                    FirstStation = 1112,
                    ID = 37,
                    LastStation = 2223,
                    Area = Areas.center
                },

                new Line
                {
                   FirstStation = 1113,
                    ID = 38,
                    LastStation = 2224,
                    Area = Areas.center
                }
            };
    }
}
