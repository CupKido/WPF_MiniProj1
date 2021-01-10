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
using DALAPI;
using DS;
using DO;

namespace DALObject  
{
    class DALObject : IDAL
    {
        static readonly DALObject instance = new DALObject();
        static DALObject() { }// static ctor to ensure instance init is done just before first usage
        DALObject() { } // default => private
        public static DALObject Instance { get => instance; }




        //ArrayList Buses = new ArrayList();
        //public DALObject()
        //{
        //    Buses = DS.myDS.Buses;
        //}
        

        #region User
        public void AddUser(User user)
        {
            if (DataSource.ListUsers.FirstOrDefault(p => p.UserName == user.UserName) != null)
            {
                throw " ";
            }
            DataSource.ListUsers.Add(user.Clone());
        }

        public User GetUser(string UserName)
        {
            DO.User user = DataSource.ListUsers.Find(p => p.UserName == UserName);
            if(user != null)
            {
                return user.Clone();
            }
            throw " ";
        }

        public IEnumerable<User> GetAllUsers()
        {
            return from user in DataSource.ListUsers
                   select user.Clone();
        }

        public IEnumerable<User> GetAllUsersBy(Predicate<User> perdicate)
        {
           if(perdicate != null)
            {
                return from user in DataSource.ListUsers
                       where perdicate(user)
                       select user.Clone();
            }
            return GetAllUsers();
        }

        public void UpdateUser(string UserName, User user)
        {
            DO.User us = DataSource.ListUsers.FirstOrDefault(pe => pe.UserName == UserName);
            if (us != null)
            {
                us = user;
            }
        }

        public User DeleteUser(string UserName)
        {
            DO.User temp = DataSource.ListUsers.FirstOrDefault(p => p.UserName == UserName);
            if (temp != null)
            {
                DataSource.ListUsers.Remove(temp);
                return temp;
            }
            throw " ";
        }
        #endregion

        #region Station
        public void AddStation(Station station)
        {
            if (DataSource.ListStations.FirstOrDefault(p => p.Code == station.Code) != null)
            {
                throw " ";
            }
            DataSource.ListStations.Add(station.Clone());
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
            DO.Station station = DataSource.ListStations.Find(p => p.Code == Code);
            if (station != null)
            {
                return station.Clone();
            }
            throw " ";
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
            DO.Station temp = DataSource.ListStations.FirstOrDefault(p => p.Code == Code);
            if (temp != null)
            {
                DataSource.ListStations.Remove(temp);
                return temp;
            }
            throw " ";
        }
        #endregion

        #region Bus
        public void AddBus(BUS bus)
        {
            if (DataSource.ListBuses.FirstOrDefault(p => p.LicenseNum == bus.LicenseNum) != null)
            {
                throw "";
            }
            DataSource.ListBuses.Add(bus.Clone());
        }
        public BUS GetBUS(int LicenseNum)
        {
            DO.BUS bus = DataSource.ListBuses.Find(p => p.LicenseNum == LicenseNum);
            if (bus != null)
            {
                return bus.Clone();
            }
            throw " ";
        }

        public IEnumerable<BUS> GetAllBusesBy(Predicate<BUS> perdicate)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(int LicenseNum, BUS Bus)
        {
            DO.BUS bus = DataSource.ListBuses.FirstOrDefault(pe => pe.LicenseNum == LicenseNum);
            if (bus != null)
            {
                bus = Bus;
            }
        }

        public BUS RemoveBus(int LicenseNum)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Line
        public void AddLine(Line line)
        {
            if (DataSource.ListLines.FirstOrDefault(p => p.ID == line.ID) != null)
            {
                throw " ";
            }
            DataSource.ListLines.Add(line.Clone());
        }

        public Line GetLine(int ID)
        {
            DO.Line line = DataSource.ListLines.Find(p => p.ID == ID);
            if (line != null)
            {
                return line;
            }
            throw " ";
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
            DO.Line Lin = DataSource.ListLines.FirstOrDefault(pe => pe.ID == ID);
            if (Lin != null)
            {
                Lin = line;
            }
        }

        public Line DeleteLine(int ID)
        {
            DO.Line temp = DataSource.ListLines.FirstOrDefault(p => p.ID == ID);
            if (temp != null)
            {
                DataSource.ListLines.Remove(temp);
                return temp;
            }
            throw " ";
        }

        #endregion

        #region Trip
        public void AddTrip(Trip trip)
        {
            if (DataSource.ListTrips.FirstOrDefault(p => p.ID == trip.ID) != null)
            {
                throw " ";
            }
            DataSource.ListTrips.Add(trip.Clone());
        }

        public Trip GetTrip(int ID)
        {
            DO.Trip trip = DataSource.ListTrips.Find(p => p.ID == ID);
            if (trip != null)
            {
                return trip;
            }
            throw " ";
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
            DO.Trip Tri = DataSource.ListTrips.FirstOrDefault(pe => pe.ID == ID);
            if (Tri != null)
            {
                Tri = trip;
            }
        }

        public Trip DeleteTrip(int ID)
        {
            DO.Trip temp = DataSource.ListTrips.FirstOrDefault(p => p.ID == ID);
            if (temp != null)
            {
                DataSource.ListTrips.Remove(temp);
                return temp;
            }
            throw " ";
        }
        #endregion

        #region BusOnTrip

        public void AddBusOnTrip(BusOnTrip busontrip)
        {
            if (DataSource.ListBusesOnTrips.FirstOrDefault(p => p.ID == busontrip.ID) != null)
            {
                throw " ";
            }
            DataSource.ListBusesOnTrips.Add(busontrip.Clone());
        }

        public BusOnTrip GetBusOnTrip(int ID)
        {
            DO.BusOnTrip busontrip = DataSource.ListBusesOnTrips.Find(p => p.ID == ID);
            if (busontrip != null)
            {
                return busontrip;
            }
            throw " ";
        }

        public void UpdateBusOnTrip(int ID, BusOnTrip busontrip)
        {
            DO.BusOnTrip bot = DataSource.ListBusesOnTrips.FirstOrDefault(pe => pe.ID == ID);
            if (bot != null)
            {
                bot = busontrip;
            }
        }

        public BusOnTrip DeleteBusOnTrip(int ID)
        {
            DO.BusOnTrip temp = DataSource.ListBusesOnTrips.FirstOrDefault(p => p.ID == ID);
            if(temp != null)
            {
                DataSource.ListBusesOnTrips.Remove(temp);
                return temp;
            }
            throw " ";
        }
        #endregion
    }
}
