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
        BadUserNameException UserEx;
        BadStationIdException StationEx;
        BadBusIdException BusEx;
        BadLineIdException lineEx;
        BadTripIdException TripEx;
        BadBOTIdException BOTEx;

        #region single tone
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
                UserEx = new BadUserNameException(user.UserName, "UserName already exists");
                throw UserEx;
            }
            DataSource.ListUsers.Add(user.Clone());
        }

        public User GetUser(string UserName)
        {
            User user = DataSource.ListUsers.Find(p => p.UserName == UserName);

            if(user != null)
            {
                return user.Clone();
            }
            UserEx = new BadUserNameException(UserName, "user cant found");
            throw UserEx;
        }

        public IEnumerable<User> GetAllUsers()
        {
            if(DataSource.ListUsers.Count == 0)
            {
                throw new BadUserNameException("No Users in List");
            }
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
                // that way is better asuming we dont want to copy everything:
                us.Password = user.Password;
                us.Admin = user.Admin;
            }
            UserEx = new BadUserNameException(UserName, "User cant be found");
            throw UserEx;
        }

        public User DeleteUser(string UserName)
        {
            User temp = DataSource.ListUsers.FirstOrDefault(p => p.UserName == UserName);
            UserEx = new BadUserNameException(UserName, "User cant be found");
            if (temp != null)
            {
                DataSource.ListUsers.Remove(temp);
                return temp;
            }
            throw UserEx;
        }
        #endregion

        #region Station
        public void AddStation(Station station)
        {
            
            if (DataSource.ListStations.FirstOrDefault(p => p.Code == station.Code) != null)
            {
                StationEx = new BadStationIdException(station.Code, "station is already exist");
                throw StationEx;
            }
            DataSource.ListStations.Add(station.Clone());
        }

        public IEnumerable<Station> GetAllStations()
        {
            if (DataSource.ListStations.Count == 0)
            {
                throw new BadStationIdException(0, "No Stations Exists");
            }
            return from station in DataSource.ListStations
                   select station.Clone();
        }

        public IEnumerable<Station> GetAllStationsBy(Predicate<Station> perdicate)
        {
            if (perdicate != null)
            {
                return from station in DataSource.ListStations
                       where perdicate(station)
                       select station.Clone();
            }
            return GetAllStations();
        }

        public Station GetStation(int Code)
        {            
            Station station = DataSource.ListStations.Find(p => p.Code == Code);
            if (station != null)
            {
                return station.Clone();
            }
            StationEx = new BadStationIdException(Code, "station does not exist");
            throw StationEx;
        }

        public void UpdateStation(int Code, Station station)
        {
            DO.Station stat = DataSource.ListStations.FirstOrDefault(pe => pe.Code == Code);
            if (stat != null)
            {
                // that way is better asuming we dont want to copy everything:
                stat.Name = station.Name;
                //optional: (i think we shouldnt allow it...) 
                stat.latitude = station.latitude;
                stat.longitude = station.longitude;
                
            }
            else {
                throw new BadStationIdException(Code, "Station cant be found");
            }
            
        }

        public void UpdateStation(int Code, Action<Station> update)
        {
            throw new NotImplementedException();
        }

        public Station DeleteStation(int Code)
        {
            StationEx = new BadStationIdException(Code, "station isn't exist");
            DO.Station temp = DataSource.ListStations.FirstOrDefault(p => p.Code == Code);
            if (temp != null)
            {
                DataSource.ListStations.Remove(temp);
                return temp;
            }
            throw StationEx;
        }
        #endregion

        #region Bus
        public void AddBus(BUS bus)
        {
            BusEx = new BadBusIdException(bus.LicenseNum, "bus is already exist");
            if (DataSource.ListBuses.FirstOrDefault(p => p.LicenseNum == bus.LicenseNum) != null)
            {
                throw BusEx;
            }
            DataSource.ListBuses.Add(bus.Clone());
        }
        public BUS GetBUS(int LicenseNum)
        {
            BusEx = new BadBusIdException(LicenseNum, "bus isn't exist");
            DO.BUS bus = DataSource.ListBuses.Find(p => p.LicenseNum == LicenseNum);
            if (bus != null)
            {
                return bus.Clone();
            }
            throw BusEx;
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
            lineEx = new BadLineIdException(line.ID,"line is already exist");
            if (DataSource.ListLines.FirstOrDefault(p => p.ID == line.ID) != null)
            {
                throw lineEx;
            }
            DataSource.ListLines.Add(line.Clone());
        }

        public Line GetLine(int ID)
        {
            lineEx = new BadLineIdException(ID, "line isn't exist");
            DO.Line line = DataSource.ListLines.Find(p => p.ID == ID);
            if (line != null)
            {
                return line;
            }
            throw lineEx;
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
            lineEx = new BadLineIdException(ID, "line isn't exist");
            DO.Line temp = DataSource.ListLines.FirstOrDefault(p => p.ID == ID);
            if (temp != null)
            {
                DataSource.ListLines.Remove(temp);
                return temp;
            }
            throw lineEx;
        }

        #endregion

        #region Trip
        public void AddTrip(Trip trip)
        {
            TripEx = new BadTripIdException(trip.ID, "Trip is already axist");
            if (DataSource.ListTrips.FirstOrDefault(p => p.ID == trip.ID) != null)
            {
                throw TripEx;
            }
            DataSource.ListTrips.Add(trip.Clone());
        }

        public Trip GetTrip(int ID)
        {
            TripEx = new BadTripIdException(ID, "Trip isn't axist");
            DO.Trip trip = DataSource.ListTrips.Find(p => p.ID == ID);
            if (trip != null)
            {
                return trip;
            }
            throw TripEx;
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
            TripEx = new BadTripIdException(ID, "Trip isn't axist");
            DO.Trip temp = DataSource.ListTrips.FirstOrDefault(p => p.ID == ID);
            if (temp != null)
            {
                DataSource.ListTrips.Remove(temp);
                return temp;
            }
            throw TripEx;
        }
        #endregion

        #region BusOnTrip

        public void AddBusOnTrip(BusOnTrip busontrip)
        {
            BOTEx = new BadBOTIdException(busontrip.ID, "bus on trip is already exist");
            if (DataSource.ListBusesOnTrips.FirstOrDefault(p => p.ID == busontrip.ID) != null)
            {
                throw BOTEx;
            }
            DataSource.ListBusesOnTrips.Add(busontrip.Clone());
        }

        public BusOnTrip GetBusOnTrip(int ID)
        {
            BOTEx = new BadBOTIdException(ID, "bus on trip isn't exist");
            DO.BusOnTrip busontrip = DataSource.ListBusesOnTrips.Find(p => p.ID == ID);
            if (busontrip != null)
            {
                return busontrip;
            }
            throw BOTEx;
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
            BOTEx = new BadBOTIdException(ID, "bus on trip isn't exist");
            DO.BusOnTrip temp = DataSource.ListBusesOnTrips.FirstOrDefault(p => p.ID == ID);
            if(temp != null)
            {
                DataSource.ListBusesOnTrips.Remove(temp);
                return temp;
            }
            throw BOTEx;
        }
        #endregion
    }
}
#endregion