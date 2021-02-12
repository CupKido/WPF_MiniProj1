using DLAPI;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DSXml;

namespace DALXml
{
    public class DALXml : IDAL
    {
        #region bus
        public void AddBus(BUS bus)
        {
            XElement element = (from per in DataSourceXml.ListBuses.Elements()
                                where int.Parse(per.Element("id").Value) == bus.LicenseNum
                                select per).FirstOrDefault();
            if (element != null)
                throw new DO.BadBusIdException(bus.LicenseNum, "bus is already exist");
            XElement p = new XElement("bus",
               new XElement("LicenseNum", bus.LicenseNum),
               new XElement("FromDate", bus.FromDate.ToUniversalTime().Ticks.ToString()),
               new XElement("lastime", bus.lastime.ToUniversalTime().Ticks.ToString()),
               new XElement("TotalTrip", bus.TotalTrip.ToString()),
               new XElement("ckm", bus.ckm),
               new XElement("FuelRemain", bus.FuelRemain),
               new XElement("status", bus.status.ToString())


               );
            DataSourceXml.ListBuses.Add(p);
            DataSourceXml.ListBuses.Save(DataSourceXml.busesPath);
        }
        public IEnumerable<BUS> GetAllBuses()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<BUS> GetAllBusesBy(Predicate<BUS> perdicate)
        {
            throw new NotImplementedException();
        }
        public BUS GetBUS(int LicenseNum)
        {
            throw new NotImplementedException();
        }
        public BUS RemoveBus(int LicenseNum)
        {
            throw new NotImplementedException();
        }
        public void UpdateBus(int LicenseNum, BUS Bus)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Line
        public void AddLine(Line line)
        {
            throw new NotImplementedException();
        }
        public Line DeleteLine(int ID)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Line> GetAllLines()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Line> GetAllLinesBy(Predicate<Line> perdicate)
        {
            throw new NotImplementedException();
        }
        public Line GetLine(int ID)
        {
            throw new NotImplementedException();
        }
        public void UpdateLine(int ID, Line line)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Trip
        public void AddTrip(Trip trip)
        {
            throw new NotImplementedException();
        }
        public Trip DeleteTrip(int ID)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Trip> GetAllTrips()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Trip> GetAllTripsBy(Predicate<Trip> perdicate)
        {
            throw new NotImplementedException();
        }
        public Trip GetTrip(int ID)
        {
            throw new NotImplementedException();
        }
        public void UpdateTrip(int ID, Trip trip)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region station
        public void AddStation(Station station)
        {
            throw new NotImplementedException();
        }
        public Station DeleteStation(int Code)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Station> GetAllStations()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Station> GetAllStationsBy(Predicate<Station> perdicate)
        {
            throw new NotImplementedException();
        }
        public Station GetStation(int Code)
        {
            throw new NotImplementedException();
        }
        public void UpdateStation(int Code, Station station)
        {
            throw new NotImplementedException();
        }
        public void UpdateStation(int Code, Action<Station> update)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region user
        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }
        public User DeleteUser(string UserName)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<User> GetAllUsersBy(Predicate<User> perdicate)
        {
            throw new NotImplementedException();
        }
        public User GetUser(string UserName)
        {
            throw new NotImplementedException();
        }
        public void UpdateUser(string UserName, User user)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region BusOnTrip
        public void AddBusOnTrip(BusOnTrip busontrip)
        {
            throw new NotImplementedException();
        }
        public BusOnTrip DeleteBusOnTrip(int ID)
        {
            throw new NotImplementedException();
        }
        public BusOnTrip GetBusOnTrip(int ID)
        {
            throw new NotImplementedException();
        }
        public void UpdateBusOnTrip(int ID, BusOnTrip busontrip)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region LineStation
        public void AddLineStation(LineStation station)
        {
            throw new NotImplementedException();
        }
        public LineStation DeleteLineStation(int Code, int line)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<LineStation> GetAllLineStations()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<LineStation> GetAllLineStationsBy(Predicate<LineStation> perdicate)
        {
            throw new NotImplementedException();
        }
        public LineStation GetLineStation(int Code, int line)
        {
            throw new NotImplementedException();
        }
        public void UpdateLineStation(LineStation station)
        {
            throw new NotImplementedException();
        }

        #endregion



    }
}
