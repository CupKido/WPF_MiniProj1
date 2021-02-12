using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using DO;
using System.Xml.Linq;
using System.IO;

namespace DS
{
    static public class DataSource
    {
        #region xml
        //public static XElement ListBuses;
        //public static string busesPath = @"BusesXml.xml";
        //public static XElement ListLines;
        //public static string linesPath = @"LinesXml.xml";
        //public static XElement ListTrips;
        //public static string TripsPath = @"TripsXml.xml";
        //public static XElement BusOnTrip;
        //public static string BusOnTripPath = @"BusOnTripXml.xml";
        //public static XElement ListStations;
        //public static string StationsPath = @"StationsXml.xml";
        //public static XElement ListUsers;
        //public static string UsersPath = @"UsersXml.xml";
        //public static XElement ListLineStations;
        //public static string LineStationPath = @"LineStationsXml.xml";
        //public static XElement ListLineTrip;
        //public static string LineTripPath = @"LineTripsXml.xml";
        //public static XElement ListAdjacent;
        //public static string AdjacentPath = @"AdjacentXml.xml";

        //static DataSource()
        //{
        //    if (!File.Exists(busesPath))
        //        CreateFileBusOnTrip();
        //    if (!File.Exists(linesPath))
        //        CreateFileBuses();
        //    if (!File.Exists(TripsPath))
        //        CreateFileLines();
        //    if (!File.Exists(BusOnTripPath))
        //        CreateFileTrips();
        //    if (!File.Exists(StationsPath))
        //        CreateFileStations();
        //    if (!File.Exists(UsersPath))
        //        CreateFileUsers();
        //    if (!File.Exists(LineStationPath))
        //        CreateFileLineStations();
        //    if (!File.Exists(LineTripPath))
        //        CreateFileLineTrips();
        //    if (!File.Exists(AdjacentPath))
        //        CreateFileAdjacent();
        //    loadAll();
        //    InitAllElements();

        //}

        //#region Create  Files
        //private static void CreateFileBusOnTrip()
        //{
        //    BusOnTrip = new XElement("BusOnTrip");
        //    BusOnTrip.Save(busesPath);
        //}

        //private static void CreateFileBuses()
        //{
        //    BusOnTrip = new XElement("Buses");
        //    BusOnTrip.Save(linesPath);
        //}

        //private static void CreateFileLines()
        //{
        //    BusOnTrip = new XElement("Lines");
        //    BusOnTrip.Save(TripsPath);
        //}

        //static private void CreateFileTrips()
        //{
        //    BusOnTrip = new XElement("Trips");
        //    BusOnTrip.Save(BusOnTripPath);
        //}

        //private static void CreateFileStations()
        //{
        //    BusOnTrip = new XElement("Stations");
        //    BusOnTrip.Save(StationsPath);
        //}

        //private static void CreateFileUsers()
        //{
        //    BusOnTrip = new XElement("users");
        //    BusOnTrip.Save(UsersPath);
        //}

        //private static void CreateFileLineStations()
        //{
        //    BusOnTrip = new XElement("LineStations");
        //    BusOnTrip.Save(UsersPath);
        //}
        //private static void CreateFileLineTrips()
        //{
        //    BusOnTrip = new XElement("LineTrips");
        //    BusOnTrip.Save(UsersPath);
        //}
        //private static void CreateFileAdjacent()
        //{
        //    BusOnTrip = new XElement("Adjacent");
        //    BusOnTrip.Save(UsersPath);
        //}
        //#endregion

        //private static void InitAllElements()
        //{


        //}


        //public static void SaveCourse() { ListBuses.Save(busesPath); }
        //static public void loadAll()
        //{
        //    LoadBuses();
        //    LoadLines();
        //    LoadTrips();
        //    LoadBusOnTrip();
        //    LoadStations();
        //    LoadUsers();
        //}

        //public static void LoadUsers()
        //{
        //    ListUsers = XElement.Load(UsersPath);
        //}

        //public static void LoadStations()
        //{
        //    ListStations = XElement.Load(StationsPath);
        //}

        //public static void LoadTrips()
        //{
        //    ListTrips = XElement.Load(TripsPath);
        //}

        //public static void LoadLines()
        //{
        //    ListLines = XElement.Load(linesPath);
        //}

        //public static void LoadBuses()
        //{
        //    ListBuses = XElement.Load(busesPath);
        //}

        //public static void LoadBusOnTrip()
        //{
        //    BusOnTrip = XElement.Load(BusOnTripPath);
        //}

        //static List<Line> ListPersons = new List<Line>
        //    {
        //        new Line
        //        {
        //            FirstStation = 1111,
        //            ID = 36,
        //            LastStation = 2222,
        //            Area = Areas.center
        //        },

        //        new Line
        //        {
        //            FirstStation = 1112,
        //            ID = 37,
        //            LastStation = 2223,
        //            Area = Areas.center
        //        },

        //        new Line
        //        {
        //           FirstStation = 1113,
        //            ID = 38,
        //            LastStation = 2224,
        //            Area = Areas.center
        //        }
        //    };

        #endregion


        public static List<BUS> ListBuses;
        public static List<Line> ListLines;
        public static List<Trip> ListTrips;
        public static List<BusOnTrip> ListBusesOnTrips;
        public static List<Station> ListStations;
        public static List<User> ListUsers;
        public static List<LineStation> ListLineStations;
        public static List<LineTrip> ListLineTrip;
        public static List<AdjacentStations> ListAdjacent;
        public static int CurrentLineCode;

        static DataSource()
        {
            //insert random generator here
            clear();

        }
        static void clear()
        {
            CurrentLineCode = 0;
            ListBuses = new List<BUS>() {
                new BUS()
                {
                    FromDate = new DateTime(2003,6,12),
                    LicenseNum = 3642531,
                    TotalTrip = 2222,
                    FuelRemain = 1200,
                    lastime = new DateTime(2018,7,05),
                    ckm = 20
                },

                    new BUS()
                    {
                        FromDate = new DateTime(2007,1,20),
                    LicenseNum = 3642532,
                    TotalTrip = 2221,
                    FuelRemain = 800,
                    lastime = new DateTime(2017,8,24),
                    ckm = 50
                    },

                    new BUS()
                    {
                        FromDate = new DateTime(2019,10,29),
                    LicenseNum = 36123419,
                    TotalTrip = 10293,
                    FuelRemain = 910,
                    lastime = new DateTime(2019,10,29),
                    ckm = 10293
                    }
                };
            ListLines = new List<Line>()
            {
                new Line()
                {
                    FirstStation = 1111,
                    ID = 36,
                    LastStation = 2222,
                    Area = Areas.center,
                    Code = ++CurrentLineCode
                },

                new Line()
                {
                    FirstStation = 1112,
                    ID = 37,
                    LastStation = 2223,
                    Area = Areas.center,
                    Code = ++CurrentLineCode
                },
                
                new Line()
                {
                    FirstStation = 6203,
                    ID = 14,
                    LastStation = 1305,
                    Area = Areas.center,
                    Code = ++CurrentLineCode
                }
                };
            ListTrips = new List<Trip>();
            ListBusesOnTrips = new List<BusOnTrip>() { 
            };
            ListStations = new List<Station>() {
            new Station()
            {
                Code = 6203,
                Latitude = 0,
                Longitude = 2,
                Name = "תחנת רכבת בית שמש"
            },
             new Station()
            {
                Code = 5393,
                Latitude = 0,
                Longitude = 2,
                Name = "העלייה/יגאל אלון"
            },
              new Station()
            {
                Code = 5388,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 5395,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 5370,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 5378,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 5377,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 5368,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 5367,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 5339,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 5335,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 5331,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 5328,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 5291,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 1133,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 563,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 5286,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 5309,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 5306,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 5305,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 5279,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 5276,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 5282,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 5310,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 5316,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 5318,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 572,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 5341,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
               new Station()
            {
                Code = 573,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 574,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 575,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 576,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 577,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 578,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 579,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 580,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 581,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 582,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 583,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 584,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 585,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 5221,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 5205,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 598,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 5223,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 599,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 600,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 5220,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 4229,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 5199,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 635,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 1322,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 1305,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
               new Station()
            {
                Code = 1111,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 1112,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
             new Station()
            {
                Code = 2222,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            },
              new Station()
            {
                Code = 3333,
                Latitude = 0,
                Longitude = 2,
                Name = "a"
            }
            };
            ListUsers = new List<User>();
            ListLineStations = new List<LineStation>() {
            new LineStation()
            {
                LineID = 14,
                LineStationIndex = 1,
                Station = 6023,
                NextStation = 5393
            },
            new LineStation()
            {
                LineID = 14,
                LineStationIndex = 1,
                PrevStation = 6023,
                Station = 5393,
                NextStation = 5388
            },
            new LineStation()
            {
                LineID = 14,
                LineStationIndex = 2,
                NextStation = 5395,
                PrevStation = 5393,
                Station = 5388
            },
            new LineStation()
            {
                LineID = 14,
                LineStationIndex = 3,
                Station = 5395,
                PrevStation = 5393,
                NextStation = 1305
            },
            new LineStation()
            {
                LineID = 14,
                LineStationIndex = 0,
                Station = 1305,
                PrevStation = 5395
            }
            };
            ListLineTrip = new List<LineTrip>();
            ListAdjacent = new List<AdjacentStations>();
        }


    }
}
