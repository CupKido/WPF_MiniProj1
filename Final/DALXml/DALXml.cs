using DLAPI;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace DALXml
{
    public class DALXML : IDAL
    {

        #region singleton

        static readonly DALXML instance = new DALXML();
        static DALXML() { }
        DALXML() { }

        public static DALXML Instance { get => instance; }

        #endregion

        #region Data Paths

        string BusesPath = @"BusesXml.xml";
        string LinesPath = @"LinesXml.xml";
        string LineTripsPath = @"LineTripsXml.xml";
        string StationsPath = @"StationsXml.xml";
        string UsersPath = @"UsersXml.xml";
        string LineStationsPath = @"LineStationsXml.xml";
        string TripsPath = @"TripsXml.xml";
        string BusOnTripPath = @"BusOnTripXml.xml";
        string AdjacentStationsPath = @"AdjacentStationsXml.xml";


        #endregion

        #region User
        public void AddUser(User user)
        {
            XElement UsersRootElem = XMLTools.LoadListFromXMLElement(UsersPath);

            XElement UserElem = (from userElem in UsersRootElem.Elements()
                                 where userElem.Element("UserName").Value == user.UserName
                                 select userElem).FirstOrDefault();

            if (UserElem != null)
                throw new DO.BadUserNameException(user.UserName, "User already exists in Data");

            XElement newBus =
                new XElement("User",
               new XElement("UserName", user.UserName),
               //could use some security
               new XElement("Password", user.Password),
               new XElement("Admin", user.Admin.ToString())
               );
            UsersRootElem.Add(newBus);
            XMLTools.SaveListToXMLElement(UsersRootElem, UsersPath);
        }

        public User GetUser(string UserName)
        {
            XElement UsersRootElem = XMLTools.LoadListFromXMLElement(UsersPath);

            if (UsersRootElem.Elements().Count() == 0)
            {
                throw new BadUserNameException("", "No Users in Data");
            }

            DO.User newUser = (from UserElem in UsersRootElem.Elements()
                               where UserElem.Element("UserName").Value == UserName
                               select new User()
                               {
                                   UserName = UserElem.Element("UserName").Value,
                                   Password = UserElem.Element("Password").Value,
                                   Admin = bool.Parse(UserElem.Element("Admin").Value)
                               }).FirstOrDefault();
            if (newUser == null)
            {
                throw new BadUserNameException(UserName, "User Doesn't exist in Data");
            }
            return newUser;
        }

        public IEnumerable<User> GetAllUsers()
        {
            XElement UsersRootElem = XMLTools.LoadListFromXMLElement(UsersPath);

            if (UsersRootElem.Elements().Count() == 0)
            {
                throw new BadUserNameException("", "No Users in Data");
            }

            return from UserElem in UsersRootElem.Elements()
                   select new User()
                   {
                       UserName = UserElem.Element("UserName").Value,
                       Password = UserElem.Element("Password").Value,
                       Admin = bool.Parse(UserElem.Element("Admin").Value)
                   };
        }
        public IEnumerable<User> GetAllUsersBy(Predicate<User> perdicate)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(string UserName, User user)
        {
            XElement UsersRootElem = XMLTools.LoadListFromXMLElement(UsersPath);

            if (UsersRootElem.Elements().Count() == 0)
            {
                throw new BadUserNameException("", "No Users in Data");
            }

            XElement UserElem = (from userElem in UsersRootElem.Elements()
                                 where userElem.Element("UserName").Value == UserName
                                 select userElem
                                 ).FirstOrDefault();

            if (UserElem == null)
            {
                throw new BadUserNameException(UserName, "User Doesn't exist in Data");
            }

            UserElem.Element("UserName").Value = user.UserName;
            UserElem.Element("Password").Value = user.Password;
            UserElem.Element("Admin").Value = user.Admin.ToString();

        }

        public User DeleteUser(string UserName)
        {
            XElement UsersRootElem = XMLTools.LoadListFromXMLElement(UsersPath);

            if (UsersRootElem.Elements().Count() == 0)
            {
                throw new BadUserNameException("", "No Users in Data");
            }

            XElement UserElem = (from userElem in UsersRootElem.Elements()
                                 where userElem.Element("UserName").Value == UserName
                                 select userElem
                                 ).FirstOrDefault();

            if (UserElem == null)
            {
                throw new BadUserNameException(UserName, "User Doesn't exist in Data");
            }

            DO.User User = new User()
            {
                UserName = UserElem.Element("UserName").Value,
                Password = UserElem.Element("Password").Value,
                Admin = bool.Parse(UserElem.Element("Admin").Value)
            };

            UserElem.Remove();

            XMLTools.SaveListToXMLElement(UsersRootElem, UsersPath);

            return User;
        }

        #endregion

        #region Station

        public void AddStation(Station station)
        {
            XElement StationsRootElem = XMLTools.LoadListFromXMLElement(StationsPath);

            XElement StationElem = (from StationEl in StationsRootElem.Elements()
                                    where int.Parse(StationEl.Element("Code").Value) == station.Code
                                    select StationEl).FirstOrDefault();

            if (StationElem != null)
                throw new DO.BadStationIdException(station.Code, "Station already exists in Data");

            XElement newStation =
                new XElement("Station",
               new XElement("Code", station.Code),
               new XElement("Name", station.Name),
               new XElement("Longitude", station.Longitude),
               new XElement("Latitude", station.Latitude)
               );
            StationsRootElem.Add(newStation);
            XMLTools.SaveListToXMLElement(StationsRootElem, StationsPath);
        }

        public Station GetStation(int Code)
        {
            XElement StationsRootElem = XMLTools.LoadListFromXMLElement(StationsPath);
            List<Station> list = (from stat in StationsRootElem.Elements()
                                      //where int.Parse(stat.Element("Code").Value) == Code
                                  select new Station()
                                  {
                                      Code = int.Parse(stat.Element("Code").Value),
                                      Name = stat.Element("Name").Value,
                                      Longitude = Convert.ToDouble(stat.Element("Longitude").Value),
                                      Latitude = Convert.ToDouble(stat.Element("Latitude").Value)
                                  }).ToList();

            if (StationsRootElem.Elements().Count() == 0)
            {
                throw new BadStationIdException(0, "No Stations in Data");
            }
            return list.Find(stat => stat.Code == Code);
        }

        public IEnumerable<Station> GetAllStations()
        {
            XElement StationsRootElem = XMLTools.LoadListFromXMLElement(StationsPath);

            if (StationsRootElem.Elements().Count() == 0)
            {
                throw new BadStationIdException(0, "No Stations in Data");
            }

            return from stat in StationsRootElem.Elements()
                   select new DO.Station()
                   {
                       Code = int.Parse(stat.Element("Code").Value),
                       Name = stat.Element("Name").Value,
                       Longitude = Convert.ToDouble(stat.Element("Longitude").Value),
                       Latitude = Convert.ToDouble(stat.Element("Latitude").Value)
                   };
        }
        public IEnumerable<Station> GetAllStationsBy(Predicate<Station> perdicate)
        {
            XElement StationsRootElem = XMLTools.LoadListFromXMLElement(StationsPath);

            if (StationsRootElem.Elements().Count() == 0)
            {
                throw new BadStationIdException(0, "No Stations in Data");
            }

            return from stat in StationsRootElem.Elements()
                   let statemp = new DO.Station()
                   {
                       Code = int.Parse(stat.Element("Code").Value),
                       Name = stat.Element("Name").Value,
                       Longitude = Convert.ToDouble(stat.Element("Longitude").Value),
                       Latitude = Convert.ToDouble(stat.Element("Latitude").Value)
                   }
                   where perdicate(statemp)
                   select statemp;
        }
        public void UpdateStation(int Code, Station station)
        {
            XElement StationsRootElem = XMLTools.LoadListFromXMLElement(StationsPath);

            if (StationsRootElem.Elements().Count() == 0)
            {
                throw new BadStationIdException(0, "No Stations in Data");
            }

            XElement StationElem = (from statElem in StationsRootElem.Elements()
                                    where int.Parse(statElem.Element("Code").Value) == Code
                                    select statElem
                                   ).FirstOrDefault();
            if (StationElem == null)
            {
                throw new BadStationIdException(Code, "Station Doesn't exist in Data");
            }
            StationElem.Element("Code").Value = station.Code.ToString();
            StationElem.Element("Name").Value = station.Name;
            StationElem.Element("Longitude").Value = station.Longitude.ToString();
            StationElem.Element("Latitude").Value = station.Latitude.ToString();

            XMLTools.SaveListToXMLElement(StationsRootElem, StationsPath);

        }
        public void UpdateStation(int Code, Action<Station> update)
        {
            throw new NotImplementedException();
        }

        public Station DeleteStation(int Code)
        {
            XElement StationsRootElem = XMLTools.LoadListFromXMLElement(StationsPath);

            if (StationsRootElem.Elements().Count() == 0)
            {
                throw new BadStationIdException(0, "No Stations in Data");
            }

            XElement StationElem = (from StatElem in StationsRootElem.Elements()
                                    where int.Parse(StatElem.Element("Code").Value) == Code
                                    select StatElem
                                   ).FirstOrDefault();
            if (StationElem == null)
            {
                throw new BadStationIdException(Code, "Station Doesn't exist in Data");
            }

            DO.Station Station = new Station()
            {
                Code = int.Parse(StationElem.Element("Code").Value),
                Name = StationElem.Element("Name").Value,
                Longitude = Convert.ToDouble(StationElem.Element("Longitude").Value),
                Latitude = Convert.ToDouble(StationElem.Element("Latitude").Value)
            };

            StationElem.Remove();

            XMLTools.SaveListToXMLElement(StationsRootElem, StationsPath);

            return Station;

        }

        #endregion

        #region Adjacent

        public void AddAdjacentStations(AdjacentStations adjacentstation)
        {
            XElement AdStRootElem;
            try
            {
                AdStRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw ex;
            }


            XElement AdStElem;
            try
            {
                AdStElem = (from AdjElem in AdStRootElem.Elements()
                            where int.Parse(AdjElem.Element("Station1").Value) == adjacentstation.Station1
                            where int.Parse(AdjElem.Element("Station2").Value) == adjacentstation.Station2
                            select AdjElem).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (AdStElem != null)
                throw new Exception("Stations already exists");

            XElement newAdSt =
                new XElement("AdjacentStations",
               new XElement("Station1", adjacentstation.Station1.ToString()),
               new XElement("Station2", adjacentstation.Station2.ToString()),
               new XElement("Distance", adjacentstation.Distance.ToString()),
               new XElement("Time", adjacentstation.Time.ToString())
               );

            AdStRootElem.Add(newAdSt);
            try
            {
                XMLTools.SaveListToXMLElement(AdStRootElem, AdjacentStationsPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }
        }

        public AdjacentStations GetAdjacentStations(int station1, int station2)
        {
            XElement AdStRootElem;
            try
            {
                AdStRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw ex;
            }

            DO.AdjacentStations AdSt = (from AdjElem in AdStRootElem.Elements()
                                        where int.Parse(AdjElem.Element("Station1").Value) == station1
                                        where int.Parse(AdjElem.Element("Station2").Value) == station2
                                        select new DO.AdjacentStations()
                                        {
                                            Station1 = int.Parse(AdjElem.Element("Station1").Value),
                                            Station2 = int.Parse(AdjElem.Element("Station2").Value),
                                            Distance = Convert.ToDouble(AdjElem.Element("Station1").Value),
                                            Time = TimeSpan.Parse(AdjElem.Element("Station1").Value),

                                        }
                                        ).FirstOrDefault();

            if (AdSt == null)
            {
                //put different ex
                throw new Exception("Adjacent Stations Do not exist in Data");
            }
            return AdSt;
        }

        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            XElement AdStRootElem;
            try
            {
                AdStRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw ex;
            }

            if (AdStRootElem.Elements().Count() == 0)
            {
                throw new Exception("No Adjacent Stations in Data");
            }

            return from AdjElem in AdStRootElem.Elements()
                   select new DO.AdjacentStations()
                   {
                       Station1 = int.Parse(AdjElem.Element("Station1").Value),
                       Station2 = int.Parse(AdjElem.Element("Station2").Value),
                       Distance = Convert.ToDouble(AdjElem.Element("Station1").Value),
                       Time = TimeSpan.Parse(AdjElem.Element("Station1").Value)
                   };


        }

        public void UpdateAdjacentStations(AdjacentStations adjacentstations)
        {
            XElement AdStRootElem;
            try
            {
                AdStRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw ex;
            }

            XElement AdStElem = (from AdjElem in AdStRootElem.Elements()
                                 where int.Parse(AdjElem.Element("Station1").Value) == adjacentstations.Station1
                                 where int.Parse(AdjElem.Element("Station2").Value) == adjacentstations.Station2
                                 select AdjElem
                                        ).FirstOrDefault();

            if (AdStElem == null)
            {
                //put different ex
                throw new Exception("Adjacent Stations Do not exist in Data");
            }

            AdStElem.Element("Station1").Value = adjacentstations.Station1.ToString();
            AdStElem.Element("Station2").Value = adjacentstations.Station2.ToString();
            AdStElem.Element("Distance").Value = adjacentstations.Distance.ToString();
            AdStElem.Element("Time").Value = adjacentstations.Time.ToString();

            try
            {
                XMLTools.SaveListToXMLElement(AdStRootElem, AdjacentStationsPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }
        }

        public AdjacentStations RemoveAdjacentStations(int station1, int station2)
        {
            XElement AdStRootElem;
            try
            {
                AdStRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationsPath);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw ex;
            }

            XElement AdStElem = (from AdjElem in AdStRootElem.Elements()
                                 where int.Parse(AdjElem.Element("Station1").Value) == station1
                                 where int.Parse(AdjElem.Element("Station2").Value) == station2
                                 select AdjElem
                                        ).FirstOrDefault();

            if (AdStElem == null)
            {
                //put different ex
                throw new Exception("Adjacent Stations Do not exist in Data");
            }

            DO.AdjacentStations AdSt = new AdjacentStations()
            {
                Station1 = int.Parse(AdStElem.Element("Station1").Value),
                Station2 = int.Parse(AdStElem.Element("Station2").Value),
                Distance = Convert.ToDouble(AdStElem.Element("Station1").Value),
                Time = TimeSpan.Parse(AdStElem.Element("Station1").Value)
            };

            AdStElem.Remove();

            XMLTools.SaveListToXMLElement(AdStRootElem, AdjacentStationsPath);

            return AdSt;
        }

        #endregion

        #region bus
        public void AddBus(BUS bus)
        {
            XElement BusesRootElem;
            try
            {
                BusesRootElem = XMLTools.LoadListFromXMLElement(BusesPath);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw ex;
            }


            XElement Bus;
            try
            {
                Bus = (from BS in BusesRootElem.Elements()
                       where int.Parse(BS.Element("LicenseNum").Value) == bus.LicenseNum
                       select BS).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (Bus != null)
                throw new DO.BadBusIdException(bus.LicenseNum, "Bus already exists");

            XElement newBus =
                new XElement("Bus",
               new XElement("LicenseNum", bus.LicenseNum),
               new XElement("FromDate", new DateTime(bus.FromDate.Year, bus.FromDate.Month, bus.FromDate.Day)),
               new XElement("lastime", new DateTime(bus.lastime.Year, bus.lastime.Month, bus.lastime.Day)),
               new XElement("TotalTrip", bus.TotalTrip.ToString()),
               new XElement("ckm", bus.ckm),
               new XElement("FuelRemain", bus.FuelRemain),
               new XElement("status", bus.status.ToString())


               );
            BusesRootElem.Add(newBus);
            try
            {
                XMLTools.SaveListToXMLElement(BusesRootElem, BusesPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }

        }

        public BUS GetBUS(int LicenseNum)
        {

            XElement BusesRootElem;

            try
            {
                BusesRootElem = XMLTools.LoadListFromXMLElement(BusesPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }


            if (BusesRootElem.Elements().Count() == 0)
            {
                throw new BadBusIdException(0, "No Buses in List");
            }

            DO.BUS Bus;
            try
            {
                Bus = (from bus in BusesRootElem.Elements()
                       where int.Parse(bus.Element("LicenseNum").Value) == LicenseNum
                       select new BUS()
                       {
                           LicenseNum = Int32.Parse(bus.Element("LicenseNum").Value),
                           FromDate = DateTime.Parse(bus.Element("FromDate").Value),
                           lastime = DateTime.Parse(bus.Element("lastime").Value),
                           TotalTrip = Convert.ToDouble(bus.Element("TotalTrip").Value),
                           ckm = Convert.ToDouble(bus.Element("ckm").Value),
                           FuelRemain = Convert.ToDouble(bus.Element("FuelRemain").Value),
                           status = (BusStatus)Enum.Parse(typeof(BusStatus), bus.Element("status").Value)
                       }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }



            if (Bus == null)
            {
                throw new BadBusIdException(LicenseNum, "Bus Doesn't exist");
            }
            return Bus;

        }

        public IEnumerable<BUS> GetAllBuses()
        {
            XElement BusesRootElem;
            try
            {
                BusesRootElem = XMLTools.LoadListFromXMLElement(BusesPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }


            if (BusesRootElem.Elements().Count() == 0)
            {
                throw new BadBusIdException(0, "No Buses in List");
            }

            try
            {
                return (from bus in BusesRootElem.Elements()
                        select new BUS()
                        {
                            LicenseNum = Int32.Parse(bus.Element("LicenseNum").Value),
                            FromDate = (DateTime)bus.Element("FromDate"),
                            lastime = (DateTime)bus.Element("lastime"),
                            TotalTrip = Convert.ToDouble(bus.Element("TotalTrip").Value),
                            ckm = Convert.ToDouble(bus.Element("ckm").Value),
                            FuelRemain = Convert.ToDouble(bus.Element("FuelRemain").Value),
                            status = (BusStatus)Enum.Parse(typeof(BusStatus), bus.Element("status").Value)
                        }
                       );
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<BUS> GetAllBusesBy(Predicate<BUS> perdicate)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(int LicenseNum, BUS Bus)
        {
            XElement BusesRootElem;
            try
            {
                BusesRootElem = XMLTools.LoadListFromXMLElement(BusesPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }

            if (BusesRootElem.Elements().Count() == 0)
            {
                throw new BadBusIdException(0, "No Buses in List");
            }

            XElement BusElem;

            try
            {
                BusElem = (from bus in BusesRootElem.Elements()
                           where int.Parse(bus.Element("LicenseNum").Value) == LicenseNum
                           select bus).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (Bus == null)
            {
                throw new BadBusIdException(LicenseNum, "Bus Doesn't exist in Data");
            }

            BusElem.Element("ckm").Value = Bus.ckm.ToString();
            BusElem.Element("TotalTrip").Value = Bus.TotalTrip.ToString();
            BusElem.Element("FuelRemain").Value = Bus.FuelRemain.ToString();
            BusElem.Element("FromDate").Value = Bus.FromDate.ToString();
            BusElem.Element("lastime").Value = Bus.lastime.ToString();

            try
            {
                XMLTools.SaveListToXMLElement(BusesRootElem, BusesPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }

        }

        public BUS RemoveBus(int LicenseNum)
        {
            XElement BusesRootElem;
            try
            {
                BusesRootElem = XMLTools.LoadListFromXMLElement(BusesPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }


            if (BusesRootElem.Elements().Count() == 0)
            {
                throw new BadBusIdException(0, "No Buses in List");
            }

            XElement BusElem;
            try
            {
                BusElem = (from bus in BusesRootElem.Elements()
                           where int.Parse(bus.Element("LicenseNum").Value) == LicenseNum
                           select bus
                    ).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (BusElem == null)
            {
                throw new BadBusIdException(LicenseNum, "Bus Doesn't exist");
            }
            DO.BUS Bus;
            try
            {
                Bus = new BUS()
                {
                    LicenseNum = Int32.Parse(BusElem.Element("LicenseNum").Value),
                    FromDate = DateTime.Parse(BusElem.Element("FromDate").Value),
                    lastime = DateTime.Parse(BusElem.Element("lastime").Value),
                    TotalTrip = Convert.ToDouble(BusElem.Element("TotalTrip").Value),
                    ckm = Convert.ToDouble(BusElem.Element("ckm").Value),
                    FuelRemain = Convert.ToDouble(BusElem.Element("FuelRemain").Value),
                    status = (BusStatus)Enum.Parse(typeof(BusStatus), BusElem.Element("status").Value)
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }


            BusElem.Remove();

            try
            {
                XMLTools.SaveListToXMLElement(BusesRootElem, BusesPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }

            return Bus;
        }
        #endregion

        #region Line
        public void AddLine(Line line)
        {
            XElement LinesRootElem;
            try
            {
                LinesRootElem = XMLTools.LoadListFromXMLElement(LinesPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }

            XElement LineElem;
            try
            {
                LineElem = (from LineEl in LinesRootElem.Elements()
                            where int.Parse(LineEl.Element("ID").Value) == line.ID
                            select LineEl).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            if (LineElem != null)
                throw new DO.BadLineIdException(line.ID, "Line already exists in Data");

            XElement newLine =
                new XElement("Line",
               new XElement("ID", line.ID),
               new XElement("Code", line.Code),
               new XElement("Area", line.Area.ToString()),
               new XElement("FirstStation", line.FirstStation),
               new XElement("LastStation", line.LastStation)
               );
            LinesRootElem.Add(newLine);

            try
            {
                XMLTools.SaveListToXMLElement(LinesRootElem, LinesPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }

        }

        public Line GetLine(int ID, int code)
        {
            XElement LinesRootElem;
            try
            {
                LinesRootElem = XMLTools.LoadListFromXMLElement(LinesPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }

            if (LinesRootElem.Elements().Count() == 0)
            {
                throw new BadLineIdException(0, "No Lines in Data");
            }
            DO.Line Line;
            try
            {
                Line = (from line in LinesRootElem.Elements()
                        where int.Parse(line.Element("ID").Value) == ID
                        where int.Parse(line.Element("Code").Value) == code
                        select new Line()
                        {
                            ID = Int32.Parse(line.Element("ID").Value),
                            Code = Int32.Parse(line.Element("Code").Value),
                            Area = (Areas)Enum.Parse(typeof(Areas), line.Element("Area").Value),
                            FirstStation = Int32.Parse(line.Element("FirstStation").Value),
                            LastStation = Int32.Parse(line.Element("LastStation").Value)
                        }
                         ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            if (Line == null)
            {
                throw new BadLineIdException(ID, "Line Doesn't exist in Data");
            }
            return Line;
        }

        public IEnumerable<Line> GetAllLines()
        {
            XElement LinesRootElem = XMLTools.LoadListFromXMLElement(LinesPath);

            if (LinesRootElem.Elements().Count() == 0)
            {
                throw new BadLineIdException(0, "No Lines in Data");
            }

            return (from line in LinesRootElem.Elements()
                    select new Line()
                    {
                        ID = Int32.Parse(line.Element("ID").Value),
                        Code = Int32.Parse(line.Element("Code").Value),
                        Area = (Areas)Enum.Parse(typeof(Areas), line.Element("Area").Value),
                        FirstStation = Int32.Parse(line.Element("FirstStation").Value),
                        LastStation = Int32.Parse(line.Element("LastStation").Value)
                    }
                   );
        }
        public IEnumerable<Line> GetAllLinesBy(Predicate<Line> perdicate)
        {
            throw new NotImplementedException();
        }

        public void UpdateLine(int ID, Line line)
        {
            XElement LinesRootElem = XMLTools.LoadListFromXMLElement(LinesPath);

            if (LinesRootElem.Elements().Count() == 0)
            {
                throw new BadBusIdException(0, "No Buses in List");
            }

            XElement LineElem = (from Line in LinesRootElem.Elements()
                                 where int.Parse(Line.Element("ID").Value) == ID
                                 where int.Parse(Line.Element("Code").Value) == line.Code
                                 select Line).FirstOrDefault();
            if (LineElem == null)
            {
                throw new BadLineIdException(ID, "Line Doesn't exist in Data");
            }

            LineElem.Element("ID").Value = line.ID.ToString();
            LineElem.Element("Code").Value = line.Code.ToString();
            LineElem.Element("Area").Value = line.Area.ToString();
            LineElem.Element("FirstStation").Value = line.FirstStation.ToString();
            LineElem.Element("LastStation").Value = line.LastStation.ToString();

            XMLTools.SaveListToXMLElement(LinesRootElem, LinesPath);
        }


        //need to Add line code option
        public Line DeleteLine(int ID)
        {
            XElement LinesRootElem = XMLTools.LoadListFromXMLElement(LinesPath);

            if (LinesRootElem.Elements().Count() == 0)
            {
                throw new BadLineIdException(0, "No Lines in Data");
            }
            XElement LineElem = (from line in LinesRootElem.Elements()
                                 where int.Parse(line.Element("ID").Value) == ID
                                 select line
                         ).FirstOrDefault();



            if (LineElem == null)
            {
                throw new BadLineIdException(ID, "Line Doesn't exist in Data");
            }

            DO.Line Line = new Line()
            {
                ID = Int32.Parse(LineElem.Element("ID").Value),
                Code = Int32.Parse(LineElem.Element("Code").Value),
                Area = (Areas)Enum.Parse(typeof(Areas), LineElem.Element("Area").Value),
                FirstStation = Int32.Parse(LineElem.Element("FirstStation").Value),
                LastStation = Int32.Parse(LineElem.Element("LastStation").Value)
            };

            LineElem.Remove();

            XMLTools.SaveListToXMLElement(LinesRootElem, LinesPath);

            return Line;
        }

        #endregion

        #region Trip
        public void AddTrip(Trip trip)
        {
            XElement TripsRootElem;
            try
            {
                TripsRootElem = XMLTools.LoadListFromXMLElement(TripsPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }

            XElement TripElem;
            try
            {
                TripElem = (from TripEl in TripsRootElem.Elements()
                            where int.Parse(TripEl.Element("ID").Value) == trip.ID
                            select TripEl).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            if (TripElem != null)
                throw new DO.BadTripIdException(trip.ID, "Trip already exists in Data");

            XElement newLine =
                new XElement("Trip",
               new XElement("ID", trip.ID),
               new XElement("UserName", trip.UserName),
               new XElement("LineID", trip.LineID),
               new XElement("InStation", trip.InStation),
               new XElement("InAt", trip.InAt.ToString()),
               new XElement("OutStation", trip.OutStation),
               new XElement("OutAt", trip.OutAt.ToString())
               );
            TripsRootElem.Add(newLine);

            try
            {
                XMLTools.SaveListToXMLElement(TripsRootElem, TripsPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }

        }
        public Trip DeleteTrip(int ID)
        {
            XElement TripsRootElem = XMLTools.LoadListFromXMLElement(TripsPath);

            if (TripsRootElem.Elements().Count() == 0)
            {
                throw new BadTripIdException(0, "No Trips in Data");
            }
            XElement TripElem = (from trip in TripsRootElem.Elements()
                                 where int.Parse(trip.Element("ID").Value) == ID
                                 select trip
                         ).FirstOrDefault();



            if (TripElem == null)
            {
                throw new BadTripIdException(ID, "Trip Doesn't exist in Data");
            }

            DO.Trip Trip = new Trip()
            {
                ID = Int32.Parse(TripElem.Element("ID").Value),
                UserName = TripElem.Element("UserName").Value.ToString(),
                LineID = Int32.Parse(TripElem.Element("LineID").Value),
                InStation = Int32.Parse(TripElem.Element("InStation").Value),
                InAt = TimeSpan.Parse(TripElem.Element("InAt").Value),
                OutStation = Int32.Parse(TripElem.Element("OutStation").Value),
                OutAt = TimeSpan.Parse(TripElem.Element("OutAt").Value)

            };

            TripElem.Remove();

            XMLTools.SaveListToXMLElement(TripsRootElem, TripsPath);

            return Trip;
        }
        public IEnumerable<Trip> GetAllTrips()
        {
            XElement TripsRootElem = XMLTools.LoadListFromXMLElement(TripsPath);

            if (TripsRootElem.Elements().Count() == 0)
            {
                throw new BadTripIdException(0, "No Trips in Data");
            }

            return (from trip in TripsRootElem.Elements()
                    select new Trip()
                    {
                        ID = Int32.Parse(trip.Element("ID").Value),
                        UserName = trip.Element("UserName").Value.ToString(),
                        LineID = Int32.Parse(trip.Element("LineID").Value),
                        InStation = Int32.Parse(trip.Element("InStation").Value),
                        InAt = TimeSpan.Parse(trip.Element("InAt").Value),
                        OutStation = Int32.Parse(trip.Element("OutStation").Value),
                        OutAt = TimeSpan.Parse(trip.Element("OutAt").Value)
                    }
                   );
        }
        public IEnumerable<Trip> GetAllTripsBy(Predicate<Trip> perdicate)
        {
            throw new NotImplementedException();
        }
        public Trip GetTrip(int ID)
        {
            XElement TripsRootElem;
            try
            {
                TripsRootElem = XMLTools.LoadListFromXMLElement(TripsPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }

            if (TripsRootElem.Elements().Count() == 0)
            {
                throw new BadTripIdException(0, "No Trips in Data");
            }
            DO.Trip Trip;
            try
            {
                Trip = (from TripElem in TripsRootElem.Elements()
                        where int.Parse(TripElem.Element("ID").Value) == ID
                        select new Trip()
                        {
                            ID = Int32.Parse(TripElem.Element("ID").Value),
                            UserName = TripElem.Element("UserName").Value.ToString(),
                            LineID = Int32.Parse(TripElem.Element("LineID").Value),
                            InStation = Int32.Parse(TripElem.Element("InStation").Value),
                            InAt = TimeSpan.Parse(TripElem.Element("InAt").Value),
                            OutStation = Int32.Parse(TripElem.Element("OutStation").Value),
                            OutAt = TimeSpan.Parse(TripElem.Element("OutAt").Value)
                        }
                         ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            if (Trip == null)
            {
                throw new BadTripIdException(ID, "Trip Doesn't exist in Data");
            }
            return Trip;
        }
        public void UpdateTrip(int ID, Trip trip)
        {
            XElement TripsRootElem = XMLTools.LoadListFromXMLElement(TripsPath);

            if (TripsRootElem.Elements().Count() == 0)
            {
                throw new BadTripIdException(0, "No Trips in List");
            }

            XElement TripElem = (from Trip in TripsRootElem.Elements()
                                 where int.Parse(Trip.Element("ID").Value) == ID
                                 select Trip).FirstOrDefault();
            if (TripElem == null)
            {
                throw new BadTripIdException(ID, "Trip Doesn't exist in Data");
            }

            TripElem.Element("ID").Value = trip.ID.ToString();
            TripElem.Element("UserName").Value = trip.UserName.ToString();
            TripElem.Element("LineID").Value = trip.LineID.ToString();
            TripElem.Element("InStation").Value = trip.InStation.ToString();
            TripElem.Element("InAt").Value = trip.InAt.ToString();
            TripElem.Element("OutStation").Value = trip.OutStation.ToString();
            TripElem.Element("OutAt").Value = trip.OutAt.ToString();


            XMLTools.SaveListToXMLElement(TripsRootElem, TripsPath);
        }

        #endregion

        #region BusOnTrip
        public void AddBusOnTrip(BusOnTrip busontrip)
        {
            XElement BusOnTripsRootElem;
            try
            {
                BusOnTripsRootElem = XMLTools.LoadListFromXMLElement(BusOnTripPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }

            XElement BusOnTripElem;
            try
            {
                BusOnTripElem = (from BusOnTripEl in BusOnTripsRootElem.Elements()
                                 where int.Parse(BusOnTripEl.Element("ID").Value) == busontrip.ID
                                 select BusOnTripEl).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            if (BusOnTripElem != null)
                throw new DO.BadTripIdException(busontrip.ID, "The Bus Trip already exists in Data");

            XElement newBusOnTrip =
                new XElement("Trip",
               new XElement("ID", busontrip.ID),
               new XElement("LicenseNum", busontrip.LicenseNum),
               new XElement("LineID", busontrip.LineID),
               new XElement("PlannedTakeOff", busontrip.PlannedTakeOff.ToString()),
               new XElement("ActualTakeOff", busontrip.ActualTakeOff.ToString()),
               new XElement("PrevStation", busontrip.PrevStation),
               new XElement("PrevStationAt", busontrip.PrevStationAt.ToString()),
                new XElement("NextStationAt", busontrip.NextStationAt.ToString())
               );
            BusOnTripsRootElem.Add(newBusOnTrip);

            try
            {
                XMLTools.SaveListToXMLElement(BusOnTripsRootElem, BusOnTripPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }
        }
        public BusOnTrip DeleteBusOnTrip(int ID)
        {
            XElement BusOnTripsRootElem = XMLTools.LoadListFromXMLElement(BusOnTripPath);

            if (BusOnTripsRootElem.Elements().Count() == 0)
            {
                throw new BadBOTIdException(0, "No Buses Trips in Data");
            }
            XElement BusOnTripElem = (from bot in BusOnTripsRootElem.Elements()
                                      where int.Parse(bot.Element("ID").Value) == ID
                                      select bot
                         ).FirstOrDefault();



            if (BusOnTripElem == null)
            {
                throw new BadBOTIdException(ID, "buse's Trip Doesn't exist in Data");
            }

            DO.BusOnTrip busontrip = new BusOnTrip()
            {
                ID = Int32.Parse(BusOnTripElem.Element("ID").Value),
                LicenseNum = Int32.Parse(BusOnTripElem.Element("LicenseNum").Value),
                LineID = Int32.Parse(BusOnTripElem.Element("LineID").Value),
                PlannedTakeOff = TimeSpan.Parse(BusOnTripElem.Element("PlannedTakeOff").Value),
                ActualTakeOff = TimeSpan.Parse(BusOnTripElem.Element("ActualTakeOff").Value),
                PrevStation = Int32.Parse(BusOnTripElem.Element("PrevStation").Value),
                PrevStationAt = TimeSpan.Parse(BusOnTripElem.Element("PrevStationAt").Value),
                NextStationAt = TimeSpan.Parse(BusOnTripElem.Element("NextStationAt").Value)

            };

            BusOnTripElem.Remove();

            XMLTools.SaveListToXMLElement(BusOnTripsRootElem, BusOnTripPath);

            return busontrip;
        }
        public IEnumerable<BusOnTrip> GetAllBusOnTrip()
        {
            XElement BusOnTripsRootElem = XMLTools.LoadListFromXMLElement(BusOnTripPath);

            if (BusOnTripsRootElem.Elements().Count() == 0)
            {
                throw new BadBOTIdException(0, "No LIne Trips in Data");
            }

            return (from bot in BusOnTripsRootElem.Elements()
                    select new BusOnTrip()
                    {
                        ID = Int32.Parse(bot.Element("ID").Value),
                        LicenseNum = Int32.Parse(bot.Element("LicenseNum").Value),
                        LineID = Int32.Parse(bot.Element("LineID").Value),
                        PlannedTakeOff = TimeSpan.Parse(bot.Element("PlannedTakeOff").Value),
                        ActualTakeOff = TimeSpan.Parse(bot.Element("ActualTakeOff").Value),
                        PrevStation = Int32.Parse(bot.Element("PrevStation").Value),
                        PrevStationAt = TimeSpan.Parse(bot.Element("PrevStationAt").Value),
                        NextStationAt = TimeSpan.Parse(bot.Element("NextStationAt").Value)
                    }
                   );
        }
        public IEnumerable<BusOnTrip> GetAllBusOnTripBy(Predicate<Line> perdicate)
        {
            throw new NotImplementedException();
        }
        public BusOnTrip GetBusOnTrip(int ID)
        {
            XElement BusOnTripRootElem;
            try
            {
                BusOnTripRootElem = XMLTools.LoadListFromXMLElement(BusOnTripPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }

            if (BusOnTripRootElem.Elements().Count() == 0)
            {
                throw new BadBOTIdException(0, "No buses trips in Data");
            }
            DO.BusOnTrip busOnTrip;
            try
            {
                busOnTrip = (from bot in BusOnTripRootElem.Elements()
                             where int.Parse(bot.Element("ID").Value) == ID
                             select new BusOnTrip()
                             {
                                 ID = Int32.Parse(bot.Element("ID").Value),
                                 LicenseNum = Int32.Parse(bot.Element("LicenseNum").Value),
                                 LineID = Int32.Parse(bot.Element("LineID").Value),
                                 PlannedTakeOff = TimeSpan.Parse(bot.Element("PlannedTakeOff").Value),
                                 ActualTakeOff = TimeSpan.Parse(bot.Element("ActualTakeOff").Value),
                                 PrevStation = Int32.Parse(bot.Element("PrevStation").Value),
                                 PrevStationAt = TimeSpan.Parse(bot.Element("PrevStationAt").Value),
                                 NextStationAt = TimeSpan.Parse(bot.Element("NextStationAt").Value)
                             }
                         ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            if (busOnTrip == null)
            {
                throw new BadBOTIdException(ID, "buse's trip Doesn't exist in Data");
            }
            return busOnTrip;
        }
        public void UpdateBusOnTrip(int ID, BusOnTrip busontrip)
        {
            XElement BusOnTripsRootElem = XMLTools.LoadListFromXMLElement(BusOnTripPath);

            if (BusOnTripsRootElem.Elements().Count() == 0)
            {
                throw new BadBOTIdException(0, "No Buses Trips in List");
            }

            XElement BusOnTripElem = (from bot in BusOnTripsRootElem.Elements()
                                      where int.Parse(bot.Element("ID").Value) == ID
                                      select bot).FirstOrDefault();
            if (BusOnTripElem == null)
            {
                throw new BadBOTIdException(ID, "buse's Trip Doesn't exist in Data");
            }

            BusOnTripElem.Element("ID").Value = busontrip.ID.ToString();
            BusOnTripElem.Element("LicenseNum").Value = busontrip.LicenseNum.ToString();
            BusOnTripElem.Element("LineID").Value = busontrip.LineID.ToString();
            BusOnTripElem.Element("PlannedTakeOff").Value = busontrip.PlannedTakeOff.ToString();
            BusOnTripElem.Element("ActualTakeOff").Value = busontrip.ActualTakeOff.ToString();
            BusOnTripElem.Element("PrevStation").Value = busontrip.PrevStation.ToString();
            BusOnTripElem.Element("PrevStationAt").Value = busontrip.PrevStationAt.ToString();
            BusOnTripElem.Element("NextStationAt").Value = busontrip.NextStationAt.ToString();


            XMLTools.SaveListToXMLElement(BusOnTripsRootElem, BusOnTripPath);
        }

        #endregion

        #region LineTrip

        public void AddLineTrip(LineTrip linetrip)
        {
            XElement LineTripsRootElem;
            try
            {
                LineTripsRootElem = XMLTools.LoadListFromXMLElement(LineTripsPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }

            XElement TripElem;
            try
            {
                TripElem = (from LineTripEl in LineTripsRootElem.Elements()
                            where int.Parse(LineTripEl.Element("ID").Value) == linetrip.ID
                            select LineTripEl).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            if (TripElem != null)
                throw new DO.BadTripIdException(linetrip.ID, "The Trip of this line is already exists in Data");

            XElement newlinetrip =
                new XElement("Trip",
               new XElement("ID", linetrip.ID),
               new XElement("LineID", linetrip.LineID),
               new XElement("StartAt", linetrip.StartAt.ToString()),
               new XElement("Frequency", linetrip.Frequency.ToString()),
               new XElement("FinishAt", linetrip.FinishAt.ToString())
               );
            LineTripsRootElem.Add(newlinetrip);

            try
            {
                XMLTools.SaveListToXMLElement(LineTripsRootElem, LineTripsPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }
        }

        public LineTrip GetLineTrip(int ID)
        {
            XElement LineTripsRootElem;
            try
            {
                LineTripsRootElem = XMLTools.LoadListFromXMLElement(LineTripsPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }

            if (LineTripsRootElem.Elements().Count() == 0)
            {
                throw new BadTripIdException(0, "No lines trips in Data");
            }
            DO.LineTrip linetrip;
            try
            {
                linetrip = (from lt in LineTripsRootElem.Elements()
                            where int.Parse(lt.Element("ID").Value) == ID
                            select new LineTrip()
                            {
                                ID = Int32.Parse(lt.Element("ID").Value),
                                LineID = Int32.Parse(lt.Element("LineID").Value),
                                StartAt = TimeSpan.Parse(lt.Element("StartAt").Value),
                                Frequency = TimeSpan.Parse(lt.Element("Frequency").Value),
                                FinishAt = TimeSpan.Parse(lt.Element("FinishAt").Value)
                            }
                         ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            if (linetrip == null)
            {
                throw new BadTripIdException(ID, "lines trip Doesn't exist in Data");
            }
            return linetrip;
        }

        public IEnumerable<LineTrip> GetAllLineTrips()
        {
            XElement LineTripsRootElem = XMLTools.LoadListFromXMLElement(LineTripsPath);

            if (LineTripsRootElem.Elements().Count() == 0)
            {
                throw new BadTripIdException(0, "No LIne Trips in Data");
            }

            return (from lt in LineTripsRootElem.Elements()
                    select new LineTrip()
                    {
                        ID = Int32.Parse(lt.Element("ID").Value),
                        LineID = Int32.Parse(lt.Element("LineID").Value),
                        StartAt = TimeSpan.Parse(lt.Element("StartAt").Value),
                        Frequency = TimeSpan.Parse(lt.Element("Frequency").Value),
                        FinishAt = TimeSpan.Parse(lt.Element("FinishAt").Value)
                    }
                   );
        }

        public IEnumerable<LineTrip> GetAllLineTripsBy(Predicate<LineTrip> perdicate)
        {
            throw new NotImplementedException();
        }

        public void UpdateLineTrip(int ID, LineTrip linetrip)
        {
            XElement LineTripsRootElem = XMLTools.LoadListFromXMLElement(LineTripsPath);

            if (LineTripsRootElem.Elements().Count() == 0)
            {
                throw new BadTripIdException(0, "No lines Trips in List");
            }

            XElement LineTripElem = (from lt in LineTripsRootElem.Elements()
                                     where int.Parse(lt.Element("ID").Value) == ID
                                     select lt).FirstOrDefault();
            if (LineTripElem == null)
            {
                throw new BadTripIdException(ID, "Line's Trip Doesn't exist in Data");
            }

            LineTripElem.Element("ID").Value = linetrip.ID.ToString();
            LineTripElem.Element("StartAt").Value = linetrip.StartAt.ToString();
            LineTripElem.Element("LineID").Value = linetrip.LineID.ToString();
            LineTripElem.Element("Frequency").Value = linetrip.Frequency.ToString();
            LineTripElem.Element("FinishAt").Value = linetrip.FinishAt.ToString();


            XMLTools.SaveListToXMLElement(LineTripsRootElem, LineTripsPath);
        }

        public LineTrip DeleteLineTrip(int ID)
        {
            XElement LineTripsRootElem = XMLTools.LoadListFromXMLElement(LineTripsPath);

            if (LineTripsRootElem.Elements().Count() == 0)
            {
                throw new BadTripIdException(0, "No Lines Trips in Data");
            }
            XElement LineTripElem = (from lt in LineTripsRootElem.Elements()
                                     where int.Parse(lt.Element("ID").Value) == ID
                                     select lt
                         ).FirstOrDefault();



            if (LineTripElem == null)
            {
                throw new BadTripIdException(ID, "line's Trip Doesn't exist in Data");
            }

            DO.LineTrip lineTrip = new LineTrip()
            {
                ID = Int32.Parse(LineTripElem.Element("ID").Value),
                LineID = Int32.Parse(LineTripElem.Element("LineID").Value),
                StartAt = TimeSpan.Parse(LineTripElem.Element("StartAt").Value),
                Frequency = TimeSpan.Parse(LineTripElem.Element("Frequency").Value),
                FinishAt = TimeSpan.Parse(LineTripElem.Element("FinishAt").Value)

            };

            LineTripElem.Remove();

            XMLTools.SaveListToXMLElement(LineTripsRootElem, LineTripsPath);

            return lineTrip;
        }

        #endregion

        #region LineStation
        public void AddLineStation(LineStation station)
        {
            XElement LineStationsRootElem = XMLTools.LoadListFromXMLElement(LineStationsPath);

            XElement LSElem = (from lsElem in LineStationsRootElem.Elements()
                               where int.Parse(lsElem.Element("LineID").Value) == station.LineID
                               where int.Parse(lsElem.Element("Station").Value) == station.Station
                               select lsElem).FirstOrDefault();
            if (LSElem != null)
            {
                throw new BadLSIdException(station.LineID, "Station-for-Line Allready exists in Data");
            }

            XElement newLS =
                new XElement("LineStation",
                new XElement("LineID", station.LineID),
                new XElement("Station", station.Station),
                new XElement("LineStationIndex", station.LineStationIndex),
                new XElement("PrevStation", station.PrevStation),
                new XElement("NextStation", station.NextStation)
                );
            LineStationsRootElem.Add(newLS);
            XMLTools.SaveListToXMLElement(LineStationsRootElem, LineStationsPath);


        }
        public LineStation GetLineStation(int Code, int line)
        {
            XElement LineStationsRootElem;
            try
            {
                LineStationsRootElem = XMLTools.LoadListFromXMLElement(LineStationsPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }


            if (LineStationsRootElem.Elements().Count() == 0)
            {
                throw new BadLSIdException(0, "No Stations-for-Lines in Data");
            }

            DO.LineStation LS = (from lsElem in LineStationsRootElem.Elements()
                                 where int.Parse(lsElem.Element("LineID").Value) == line
                                 where int.Parse(lsElem.Element("Station").Value) == Code
                                 select new LineStation()
                                 {
                                     LineID = int.Parse(lsElem.Element("LineID").Value),
                                     Station = int.Parse(lsElem.Element("Station").Value),
                                     LineStationIndex = int.Parse(lsElem.Element("LineStationIndex").Value),
                                     PrevStation = int.Parse(lsElem.Element("PrevStation").Value),
                                     NextStation = int.Parse(lsElem.Element("NextStation").Value)
                                 }
                                 ).FirstOrDefault();
            if (LS == null)
            {
                throw new BadLSIdException(line, "Station-for-Line Doesn't Exist in Data");
            }

            return LS;

        }

        public IEnumerable<LineStation> GetAllLineStations()
        {
            XElement LineStationsRootElem;
            try
            {
                LineStationsRootElem = XMLTools.LoadListFromXMLElement(LineStationsPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }

            if (LineStationsRootElem.Elements().Count() == 0)
            {
                throw new BadLSIdException(0, "No Stations-for-Lines in Data");
            }

            return from lsElem in LineStationsRootElem.Elements()
                   select new LineStation()
                   {
                       LineID = int.Parse(lsElem.Element("LineID").Value),
                       Station = int.Parse(lsElem.Element("Station").Value),
                       LineStationIndex = int.Parse(lsElem.Element("LineStationIndex").Value),
                       PrevStation = int.Parse(lsElem.Element("PrevStation").Value),
                       NextStation = int.Parse(lsElem.Element("NextStation").Value)
                   };
        }
        public IEnumerable<LineStation> GetAllLineStationsBy(Predicate<LineStation> perdicate)
        {

            XElement LineStationsRootElem;
            try
            {
                LineStationsRootElem = XMLTools.LoadListFromXMLElement(LineStationsPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }

            if (LineStationsRootElem.Elements().Count() == 0)
            {
                throw new BadLSIdException(0, "No Stations-for-Lines in Data");
            }

            return from lsElem in LineStationsRootElem.Elements()
                   let IsElemTemp = new LineStation()
                   {
                       LineID = int.Parse(lsElem.Element("LineID").Value),
                       Station = int.Parse(lsElem.Element("Station").Value),
                       LineStationIndex = int.Parse(lsElem.Element("LineStationIndex").Value),
                       PrevStation = int.Parse(lsElem.Element("PrevStation").Value),
                       NextStation = int.Parse(lsElem.Element("NextStation").Value)
                   }
                   where perdicate(IsElemTemp)
                   select IsElemTemp;
        }

        public void UpdateLineStation(LineStation station)
        {
            XElement LineStationsRootElem;
            try
            {
                LineStationsRootElem = XMLTools.LoadListFromXMLElement(LineStationsPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }

            if (LineStationsRootElem.Elements().Count() == 0)
            {
                throw new BadLSIdException(0, "No Stations-for-Lines in Data");
            }

            XElement LSElem = (from lsElem in LineStationsRootElem.Elements()
                               where int.Parse(lsElem.Element("LineID").Value) == station.LineID
                               where int.Parse(lsElem.Element("Station").Value) == station.Station
                               select lsElem
                               ).FirstOrDefault();
            if (LSElem == null)
            {
                throw new BadLSIdException(station.LineID, "Station-for-Line Doesn't Exist in Data");
            }

            LSElem.Element("LineID").Value = station.LineID.ToString();
            LSElem.Element("Station").Value = station.Station.ToString();
            LSElem.Element("LineStationIndex").Value = station.LineStationIndex.ToString();
            LSElem.Element("PrevStation").Value = station.PrevStation.ToString();
            LSElem.Element("NextStation").Value = station.NextStation.ToString();

            try
            {
                XMLTools.SaveListToXMLElement(LineStationsRootElem, LineStationsPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }

        }

        public LineStation DeleteLineStation(int Code, int line)
        {
            XElement LineStationsRootElem;
            try
            {
                LineStationsRootElem = XMLTools.LoadListFromXMLElement(LineStationsPath);
            }
            catch (XMLFileLoadCreateException ex)
            {
                throw ex;
            }

            if (LineStationsRootElem.Elements().Count() == 0)
            {
                throw new BadLSIdException(0, "No Stations-for-Lines in Data");
            }

            XElement LSElem = (from lsElem in LineStationsRootElem.Elements()
                               where int.Parse(lsElem.Element("LineID").Value) == line
                               where int.Parse(lsElem.Element("Station").Value) == Code
                               select lsElem
                               ).FirstOrDefault();
            if (LSElem == null)
            {
                throw new BadLSIdException(line, "Station-for-Line Doesn't Exist in Data");
            }

            DO.LineStation LineStation = new LineStation()
            {
                LineID = int.Parse(LSElem.Element("LineID").Value),
                Station = int.Parse(LSElem.Element("Station").Value),
                LineStationIndex = int.Parse(LSElem.Element("LineStationIndex").Value),
                PrevStation = int.Parse(LSElem.Element("PrevStation").Value),
                NextStation = int.Parse(LSElem.Element("NextStation").Value)
            };

            LSElem.Remove();

            XMLTools.SaveListToXMLElement(LineStationsRootElem, LineStationsPath);

            return LineStation;
        }

        #endregion



    }
}
