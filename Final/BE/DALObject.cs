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
        #endregion



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
            if (temp != null)
            {
                DataSource.ListUsers.Remove(temp);
                return temp.Clone();
            }
            UserEx = new BadUserNameException(UserName, "User cant be found");
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
            DO.Station temp = DataSource.ListStations.FirstOrDefault(p => p.Code == Code);
            if (temp != null)
            {
                DataSource.ListStations.Remove(temp);
                return temp.Clone();
            }
            StationEx = new BadStationIdException(Code, "station isn't exist");
            throw StationEx;
        }
        #endregion

        #region Bus
        public void AddBus(BUS bus)
        {            
            if (DataSource.ListBuses.FirstOrDefault(p => p.LicenseNum == bus.LicenseNum) != null)
            {
                BusEx = new BadBusIdException(bus.LicenseNum, "bus is already exist");
                throw BusEx;
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
            BusEx = new BadBusIdException(LicenseNum, "bus isn't exist");
            throw BusEx;
        }

        public IEnumerable<BUS> GetAllBusesBy(Predicate<BUS> perdicate)
        {
            if(perdicate == null)
            {
                throw new BadBusIdException(0, "bad func");
            }
            if(DataSource.ListBuses.Count == 0)
            {
                throw new BadBusIdException(0, "No Buses in List");
            }
            return from bus in DataSource.ListBuses
                   where perdicate(bus)
                   select bus;
        }

        public void UpdateBus(int LicenseNum, BUS Bus)
        {
            DO.BUS bus = DataSource.ListBuses.FirstOrDefault(pe => pe.LicenseNum == LicenseNum);
            if (bus != null)
            {
                bus.ckm = Bus.ckm;
                bus.FuelRemain = Bus.FuelRemain;
                bus.lastime = Bus.lastime;
            }
            else throw new BadBusIdException(LicenseNum, "Bus can not be found");
        }

        public BUS RemoveBus(int LicenseNum)
        {
            DO.BUS temp = DataSource.ListBuses.FirstOrDefault(p => p.LicenseNum == LicenseNum);
            if (temp != null)
            {
                DataSource.ListBuses.Remove(temp);
                return temp.Clone();
            }
            throw new BadBusIdException(LicenseNum, "Bus isn't exist");
            
        }
        #endregion

        #region Line
        public void AddLine(Line line)
        {             
            if (DataSource.ListLines.FirstOrDefault(p => p.ID == line.ID) != null)
            {
                throw new BadLineIdException(line.ID, "line already exists"); 
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
            lineEx = new BadLineIdException(ID, "line doesnt exist");
            throw lineEx;
        }

        public IEnumerable<Line> GetAllLines()
        {
            if(DataSource.ListLines.Count == 0)
            {
                throw new BadLineIdException(0, "No Lines");
            }
            return from line in DataSource.ListLines
                   select line;
        }

        public IEnumerable<Line> GetAllLinesBy(Predicate<Line> perdicate)
        {
            if (DataSource.ListLines.Count == 0)
            {
                throw new BadLineIdException(0, "No Lines");
            }
            if(perdicate == null)
            {
                return GetAllLines();
            }
            return from line in DataSource.ListLines
                   where perdicate(line)
                   select line;
        }

        public void UpdateLine(int ID, Line line)
        {
            DO.Line Lin = DataSource.ListLines.FirstOrDefault(pe => pe.ID == ID);
            if (Lin != null)
            {
                Lin.Area = line.Area;
                Lin.LastStation = line.LastStation;
                Lin.FirstStation = line.LastStation;
                Lin.ID = line.ID;
            }
            else throw new BadLineIdException(ID, "Line can not be found");
        }

        public Line DeleteLine(int ID)
        {            
            DO.Line temp = DataSource.ListLines.FirstOrDefault(p => p.ID == ID);
            if (temp != null)
            {
                DataSource.ListLines.Remove(temp);
                return temp.Clone();
            }
            throw new BadLineIdException(ID, "line does not exist");
        }

        #endregion

        #region Trip
        public void AddTrip(Trip trip)
        {            
            if (DataSource.ListTrips.FirstOrDefault(p => p.ID == trip.ID) != null)
            {
                throw new BadTripIdException(trip.ID, "Trip allready exists");
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
            throw new BadTripIdException(ID, "Trip doesnt exist");
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            if(DataSource.ListTrips.Count == 0)
            {
                throw new BadTripIdException(0, "No Trips");
            }
            return from trip in DataSource.ListTrips
                   select trip;
        }

        public IEnumerable<Trip> GetAllTripsBy(Predicate<Trip> perdicate)
        {
            if (DataSource.ListTrips.Count == 0)
            {
                throw new BadTripIdException(0, "No Trips");
            }
            if(perdicate == null)
            { return GetAllTrips(); }
            return from trip in DataSource.ListTrips
                   where perdicate(trip)
                   select trip;
        }

        public void UpdateTrip(int ID, Trip trip)
        {
            DO.Trip Tri = DataSource.ListTrips.FirstOrDefault(pe => pe.ID == ID);
            if (Tri != null)
            {
                Tri.InAt = trip.InAt;
                Tri.InStation = trip.InStation;
                Tri.OutAt = trip.OutAt;
                Tri.OutStation = trip.OutStation;
            }
            throw new BadTripIdException(ID, "Trip can not be found");
        }

        public Trip DeleteTrip(int ID)
        {            
            DO.Trip temp = DataSource.ListTrips.FirstOrDefault(p => p.ID == ID);
            if (temp != null)
            {
                DataSource.ListTrips.Remove(temp);
                return temp.Clone();
            }
            throw new BadTripIdException(ID, "Trip isn't axist");
        }
        #endregion

        #region BusOnTrip

        public void AddBusOnTrip(BusOnTrip busontrip)
        {            
            if (DataSource.ListBusesOnTrips.FirstOrDefault(p => p.ID == busontrip.ID) != null)
            {
                throw new BadBOTIdException(busontrip.ID, "Bus On Trip already exists");
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
            throw new BadBOTIdException(ID, "bus on trip doesnt exist");
        }

        public void UpdateBusOnTrip(int ID, BusOnTrip busontrip)
        {
            DO.BusOnTrip bot = DataSource.ListBusesOnTrips.FirstOrDefault(pe => pe.ID == ID);
            if (bot != null)
            {
                bot.LicenseNum = busontrip.LicenseNum;
                bot.LineID = busontrip.LineID;
                bot.NextStationAt = busontrip.NextStationAt;
                bot.PlannedTakeOff = busontrip.PlannedTakeOff;
                bot.PrevStationAt = busontrip.PrevStationAt;
            }
            throw new BadBOTIdException(ID, "Bus On Trip can not be found");
        }

        public BusOnTrip DeleteBusOnTrip(int ID)
        {            
            DO.BusOnTrip temp = DataSource.ListBusesOnTrips.FirstOrDefault(p => p.ID == ID);
            if(temp != null)
            {
                DataSource.ListBusesOnTrips.Remove(temp);
                return temp.Clone();
            }
            throw new BadBOTIdException(ID, "Bus On Trip doesnt exist");
        }
        #endregion

        #region LineTrip

        public void AddLineTrip(LineTrip linetrip)
        {
            if (DataSource.ListLineTrip.FirstOrDefault(p => p.ID == linetrip.ID) != null)
            {
                throw new BadBOTIdException(linetrip.ID, "Line Trip already exists");
            }
            DataSource.ListLineTrip.Add(linetrip.Clone());
        }

        public LineTrip GetLineTrip(int ID)
        {
            DO.LineTrip linetrip = DataSource.ListLineTrip.Find(p => p.ID == ID);
            if (linetrip != null)
            {
                return linetrip.Clone();
            }
            throw new BadBOTIdException(ID, "Line Trip doesnt exist");
        }

        public IEnumerable<LineTrip> GetAllLineTrips()
        {
            if(DataSource.ListLines.Count == 0)
            {
                throw new BadBOTIdException(0, "No LineTrips");
            }
            return from lt in DataSource.ListLineTrip
                   select lt;
        }

        public IEnumerable<LineTrip> GetAllLineTripsBy(Predicate<LineTrip> perdicate)
        {
            if (DataSource.ListLines.Count == 0)
            {
                throw new BadBOTIdException(0, "No LineTrips");
            }
            if (perdicate == null)
            {
                return from lt in DataSource.ListLineTrip
                       select lt;
            }
            return from lt in DataSource.ListLineTrip
                   where perdicate(lt)
                   select lt;
        }

        public void UpdateLineTrip(int ID, LineTrip linetrip)
        {
            DO.LineTrip bot = DataSource.ListLineTrip.FirstOrDefault(pe => pe.ID == ID);
            if (bot != null)
            {
                bot = linetrip.Clone();
            }
            else throw new BadBOTIdException(ID, "Bus On Trip can not be found");
        }

        public LineTrip DeleteLineTrip(int ID)
        {
            DO.LineTrip temp = DataSource.ListLineTrip.FirstOrDefault(p => p.ID == ID);
            if (temp != null)
            {
                DataSource.ListLineTrip.Remove(temp);
                return temp.Clone();
            }
            throw new BadBOTIdException(ID, "Line Trip doesnt exist");
        }

        #endregion

    }
}
