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
        public void AddBus(BUS bus)
        {
            myDS.Buses.Add(bus);
        }
        public BUS RemoveBus(string ID)
        {
            int loc = indexByID(ID);
            BUS temp = null;
            if (loc != -1)
            {
                temp = (BUS)myDS.Buses[loc];
                myDS.Buses.RemoveAt(loc);
            }
            return temp;
        }
        public bool IsExist(string ID)
        {
            if (indexByID(ID) == -1)
            {
                return false;
            }
            return true;
        }
        public int indexByID(string ID)
        {
            int i = 0;
            foreach (BUS bus in DS.Buses)
            {
                if (bus.licenseNum == ID)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
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
    }
}
