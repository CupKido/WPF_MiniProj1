using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Remoting.Messaging;
using DS;
using DO;

namespace DAL  
{
    sealed class DLObject : IDAL
    {
        ArrayList Buses = new ArrayList();
        public DLObject()
        {
            
        }

        #region User

        public void AddUser(User user)
        {
            if(DataSource.ListUsers.FirstOrDefault(p=>p.UserName == user.UserName) != null)
            {
                throw "a";
            }
            DataSource.ListUsers.Add(user.Clone());
        }

        public User GetUser(string UserName)
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

        public void UpdateUser(string UserName, User user)
        {
            throw new NotImplementedException();
        }

        public User DeleteUser(string UserName)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Station
        public void AddStation(Station station)
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

        public void UpdateStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(int Code, Action<Station> update)
        {
            throw new NotImplementedException();
        }

        public Station DeleteStation(int Code)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Bus
        public void AddBus(BUS bus)
        {
            if (DataSource.ListBuses.FirstOrDefault(p => p.licenseNum == bus.licenseNum) != null)
            {
                //throw 
            }
            DataSource.ListBuses.Add(bus.Clone());
        }
        public BUS GetBUS(int LicenseNum)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BUS> GetAllBusesBy(Predicate<BUS> perdicate)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(int LicenseNum, BUS Bus)
        {
            throw new NotImplementedException();
        }

        public BUS RemoveBus(int LicenseNum)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Line
        public void AddLine(Line line)
        {
            throw new NotImplementedException();
        }

        public Line GetLine(int ID)
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

        public void UpdateLine(int ID, Line line)
        {
            throw new NotImplementedException();
        }

        public Line DeleteLine(int ID)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Trip
        public void AddTrip(Trip trip)
        {
            throw new NotImplementedException();
        }

        public Trip GetTrip(int ID)
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

        public void UpdateTrip(int ID, Trip trip)
        {
            throw new NotImplementedException();
        }

        public Trip DeleteTrip(int ID)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region BusOnTrip
        public void AddBusOnTrip(BusOnTrip busontrip)
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

        public BusOnTrip DeleteBusOnTrip(int ID)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
