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
using System;
using DLAPI;
using BO;



//BO.Convertors.BTDBus
namespace BL
{
    public class MyBL : IBL
    {

        IDAL myDal = DLFactory.GetDL();
        #region Bus

        public void AddBus(BO.BUS bus)
        {
            //if (user.Admin)
            //{

            if (((bus.LicenseNum.ToString().Length == 7 && bus.FromDate.Year < 2018) || (bus.LicenseNum.ToString().Length == 8 && bus.FromDate.Year >= 2018)) != true)
            {
                throw new BO.BadBusIdException(bus.LicenseNum, "invaild ID");
            }

            if (bus.FromDate > DateTime.Now)
            {
                throw new BO.BadBusIdException(bus.LicenseNum, "starting service date cant be in the future");
            }


            if (bus.lastime > DateTime.Now)
            {
                throw new BO.BadBusIdException(bus.LicenseNum, "Last Repair date cant be in the future");
            }
            if (bus.ckm > bus.TotalTrip)
            {
                throw new BO.BadBusIdException(bus.LicenseNum, "KM since last repair cannot be more than total!");
            }
            if (bus.lastime < bus.FromDate)
            {
                throw new BO.BadBusIdException(bus.LicenseNum, "last treatment cannot be before licensing");
            }

            try
            {
                myDal.AddBus((DO.BUS)bus.CopyPropertiesToNew(typeof(DO.BUS)));
            }
            catch (DO.BadBusIdException ex)
            {

                throw new BO.BadBusIdException(ex.ID, ex.Message, ex);
            }
        }

        public BO.BUS GetBUS(int ID)
        {
            DO.BUS bus;
            try
            {
                bus = myDal.GetBUS(ID);
            }
            catch (DO.BadBusIdException ex)
            {
                throw ex;
            }
            BO.BUS bus1 = (BO.BUS)bus.CopyPropertiesToNew(typeof(BO.BUS));
            if (bus != null)
            {
                return bus1;
            }
            throw new BO.BadBusIdException(bus.LicenseNum, "'");

        }

        public BO.BUS RemoveBus(int LN)
        {
            //if (user.Admin)
            //{

            try
            {
                return myDal.RemoveBus(LN).CopyPropertiesToNew(typeof(BO.BUS)) as BO.BUS;
            }
            catch (DO.BadBusIdException ex)
            {

                throw new BO.BadBusIdException(ex.ID, ex.Message, ex);
            }
            //}
            //else throw "";
        }

        public IEnumerable<BO.BUS> GetAllBuses()
        {
            try
            {
                List<DO.BUS> list = myDal.GetAllBuses().ToList();
                return from item in list
                       let bus = (BO.BUS)item.CopyPropertiesToNew(typeof(BO.BUS))
                       orderby bus.LicenseNum
                       select bus;
            }
            catch (DO.BadBusIdException ex)
            {
                throw new BO.BadBusIdException(ex.ID, ex.Message, ex);
            }
        }

        public IEnumerable<BUS> GetBusesBy(Predicate<BUS> predicate)
        {
            List<DO.BUS> list;
            try
            {
                list = (List<DO.BUS>)myDal.GetAllLines();
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BadLineIdException(0, "no lines exist!", ex);
            }
            return from item in list
                   let bus = (BO.BUS)item.CopyPropertiesToNew(typeof(BO.BUS))
                   orderby bus.LicenseNum
                   where predicate(bus)
                   select bus;
        }

        public void UpdateBus(BUS bus)
        {
            try
            {
                myDal.UpdateBus(bus.LicenseNum, bus.CopyPropertiesToNew(typeof(DO.BUS)) as DO.BUS);
            }
            catch (DO.BadBusIdException ex)
            {

                throw new BadBusIdException(bus.LicenseNum, ex.Message, ex);
            }
        }



        #endregion

        #region bus on trip

        public void AddBusOnTrip(BO.BusOnTrip BOT)
        {
            //if (Auser.Admin)
            //{
            try
            {
                myDal.AddBusOnTrip((DO.BusOnTrip)BOT.CopyPropertiesToNew(typeof(DO.BusOnTrip)));
            }
            catch (DO.BadBOTIdException ex)
            {
                throw new BO.BadBOTIdException(ex.ID, ex.Message, ex);
            }
            //}
            //else throw " ";
        }
        public void RemoveUser(BO.BusOnTrip user)
        {
            //if (Auser.Admin)
            //{
            try
            {
                myDal.DeleteBusOnTrip(user.ID);
            }
            catch (DO.BadBOTIdException ex)
            {

                throw new BO.BadBOTIdException(ex.ID, ex.Message, ex);
            }
            //}
            //else throw "";
        }

        public IEnumerable<BO.BusOnTrip> GetAllBusesOnTrip()
        {
            List<DO.BusOnTrip> list = (List<DO.BusOnTrip>)(from bus in myDal.GetAllBusesBy(p => p.LicenseNum != 0)
                                                           let bus1 = myDal.GetBusOnTrip(bus.LicenseNum)
                                                           where (bus1 != null)
                                                           select bus1);
            return from item in list
                   let BOT = (BO.BusOnTrip)item.CopyPropertiesToNew(typeof(BO.BusOnTrip))
                   orderby BOT.ID
                   select BOT;
        }

        public void UpdateBusOnTrip(BusOnTrip bot)
        {
            try
            {
                myDal.UpdateBusOnTrip(bot.LicenseNum, bot.CopyPropertiesToNew(typeof(DO.BusOnTrip)) as DO.BusOnTrip);
            }
            catch (DO.BadBusIdException ex)
            {
                throw new BadBusIdException(bot.LicenseNum, ex.Message, ex);
            }
        }

        #endregion

        #region Line

        public void AddLine(BO.Line line)
        {
            //if (Auser.Admin)
            //{
            try
            {
                bool flag = true;
                for (int i = 1; 1 < myDal.GetAllLines().Count() + 1 && flag; i++)
                {
                    int Code = (from Line in myDal.GetAllLines()
                                where Line.Code == i
                                select i).FirstOrDefault();
                    if (Code == 0)
                    {
                        line.Code = i;
                        flag = false;
                    }
                }

            }
            catch (DO.BadLineIdException ex)
            {
                //throw new BO.BadLineIdException(ex.ID, ex.Message, ex);
                line.Code = 1;
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (DO.BadLSIdException ex)
            {
                throw new BO.BadLineIdException(ex.ID, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BO.BadLineIdException(line.ID, ex.Message, ex);
            }


            try
            {
                if (myDal.GetAllStations().FirstOrDefault(p => p.Code == line.FirstStation) != null && myDal.GetAllStations().FirstOrDefault(p => p.Code == line.LastStation) != null)
                {
                    DO.LineStation stat1 = new DO.LineStation() { LineID = line.ID, Station = line.FirstStation, LineStationIndex = 1, NextStation = line.LastStation };
                    DO.LineStation stat2 = new DO.LineStation() { LineID = line.ID, Station = line.LastStation, LineStationIndex = 2, PrevStation = line.FirstStation };
                    myDal.AddLine((DO.Line)line.CopyPropertiesToNew(typeof(DO.Line)));
                    myDal.AddLineStation(stat1);
                    myDal.AddLineStation(stat2);
                    AdjacentStations ad = new AdjacentStations()
                    {
                        Station1 = stat1.Station,
                        Station2 = stat2.Station,
                        Distance = distanceBetweenTwo(stat1.Station, stat2.Station),
                    };
                    AddAdjacentStations(ad);
                    ad.Time = TimeBetweenTwo(stat1.Station, stat2.Station, stat1.LineID);
                    UpdateAdjacentStations(ad);
                }
                else
                    throw new BO.BadLineIdException(line.ID, "line first station or last station is not exist");
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(ex.ID, ex.Message, ex);
            }
            catch (DO.BadStationIdException ex)
            {
                throw new BO.BadLineIdException(ex.ID, ex.Message + "\n" + " please insert stations that they in data" + "\n" + " or add the stations into data base first", ex);
            }
            catch
            {

            }
            //}
            //else throw " ";
        }


        public IEnumerable<BO.Line> GetAllLines()
        {
            try
            {
                List<DO.Line> list = myDal.GetAllLines().ToList();
                //List<BO.Line> list1 = list.CopyPropertiesToNew(typeof(List<BO.Line>)) as List<BO.Line>;
                return from item in list
                       let line = item.CopyPropertiesToNew(typeof(BO.Line)) as BO.Line
                       orderby line.ID
                       select line;
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(ex.ID, ex.Message, ex);
            }
        }

        public Line GetLine(int ID, int code)
        {
            DO.Line foundLine;
            try
            {
                foundLine = myDal.GetLine(ID, code);
            }
            catch
            {
                throw new NotImplementedException();
            }
            return (BO.Line)foundLine.CopyPropertiesToNew(typeof(BO.Line));
        }

        public void UpdateLine(Line line)
        {
            try
            {
                myDal.UpdateLine(line.ID, line.CopyPropertiesToNew(typeof(DO.Line)) as DO.Line);
            }
            catch (DO.BadBusIdException ex)
            {

                throw new BadLineIdException(line.ID, ex.Message, ex);
            }
        }

        public IEnumerable<Line> GetLinesBy(Predicate<Line> predicate)
        {
            List<DO.Line> list;
            try
            {
                list = (List<DO.Line>)myDal.GetAllLines();
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BadLineIdException(0, "no lines exist!", ex);
            }
            return from item in list
                   let line = (BO.Line)item.CopyPropertiesToNew(typeof(BO.Line))
                   orderby line.ID
                   where predicate(line)
                   select line;
        }
        //keep on this
        public Line RemoveLine(int ID, int Code)
        {
            try
            {
                foreach (DO.LineStation LS in myDal.GetAllLineStations())
                {
                    if (LS.LineID == ID)
                    {
                        myDal.DeleteLineStation(LS.Station, ID);
                    }
                }
                return myDal.DeleteLine(ID, Code).CopyPropertiesToNew(typeof(BO.Line)) as BO.Line;
            }
            catch (DO.BadLineIdException ex)
            {

                throw new BO.BadLineIdException(ex.ID, ex.Message, ex);
            }
            catch (DO.BadLSIdException ex)
            {
                return myDal.DeleteLine(ID, Code).CopyPropertiesToNew(typeof(BO.Line)) as BO.Line;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region User

        public void AddUser(BO.User user)
        {
            //if (Auser.Admin)
            //{
            try
            {
                myDal.AddUser((DO.User)user.CopyPropertiesToNew(typeof(DO.User)));
            }
            catch (DO.BadUserNameException ex)
            {
                throw new BO.BadUserNameException(user.UserName, ex.Message, ex);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else throw " ";
        }

        public IEnumerable<BO.User> GetAllUsers()
        {
            List<DO.User> list;
            try
            {
                list = (List<DO.User>)myDal.GetAllUsers();
            }
            catch (DO.BadUserNameException ex)
            {
                throw new BO.BadUserNameException(ex.ID, ex.Message, ex);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return from item in list
                   let Tuser = (BO.User)item.CopyPropertiesToNew(typeof(BO.User))
                   orderby Tuser.UserName
                   select Tuser;
        }

        public User GetUser(User ThatUser)
        {
            DO.User FoundUser;
            try
            {
                FoundUser = myDal.GetUser(ThatUser.UserName);
            }
            catch (DO.BadUserNameException ex)
            {
                throw new BO.BadUserNameException(ex.ID, ex.Message, ex);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BO.BadUserNameException(ThatUser.UserName, ex.Message, ex);
            }

            if (ThatUser.Password != FoundUser.Password)
            {
                throw new Exception("Incorrect UserName or Password");
            }
            return (BO.User)FoundUser.CopyPropertiesToNew(typeof(BO.User));
        }

        public User GetUser(string UserName)
        {
            DO.User FoundUser;
            try
            {
                FoundUser = myDal.GetUser(UserName);
            }
            catch (DO.BadUserNameException ex)
            {
                throw new BO.BadUserNameException(ex.ID, ex.Message, ex);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BO.BadUserNameException(UserName, ex.Message, ex);
            }
            return (BO.User)FoundUser.CopyPropertiesToNew(typeof(BO.User));
        }

        public void UpdateUser(User user)
        {
            try
            {
                myDal.UpdateUser(user.UserName, user.CopyPropertiesToNew(typeof(DO.User)) as DO.User);
            }
            catch (DO.BadBusIdException ex)
            {

                throw new BadUserNameException(user.UserName, ex.Message, ex);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BO.BadUserNameException(user.UserName, ex.Message, ex);
            }
        }

        public BO.User RemoveUser(BO.User user)
        {
            //if (Auser.Admin)
            //{
            try
            {
                return myDal.DeleteUser(user.UserName).CopyPropertiesToNew(typeof(BO.User)) as BO.User;
            }
            catch (DO.BadUserNameException ex)
            {
                throw new BO.BadUserNameException(ex.ID, ex.Message, ex);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BO.BadUserNameException(user.UserName, ex.Message, ex);
            }
            //}
            //else throw "";
        }






        #endregion

        #region station

        public void AddStation(Station station)
        {
            try
            {
                myDal.AddStation(station.CopyPropertiesToNew(typeof(DO.Station)) as DO.Station);
            }
            catch (DO.BadStationIdException ex)
            {
                throw new BO.BadStationIdException(ex.ID, ex.Message, ex);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public Station GetStation(int ID)
        {
            DO.Station foundStation = null;
            try
            {
                foundStation = myDal.GetStation(ID);

            }
            catch (DO.BadStationIdException ex)
            {
                throw new BO.BadStationIdException(ID, ex.Message);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BadStationIdException(ID, ex.Message, ex);
            }
            return foundStation.CopyPropertiesToNew(typeof(BO.Station)) as BO.Station;
        }

        public IEnumerable<Station> GetAllStations()
        {
            List<DO.Station> list;
            try
            {
                list = (List<DO.Station>)myDal.GetAllStations().ToList();
            }
            catch (DO.BadStationIdException ex)
            {
                throw new BO.BadStationIdException(ex.ID, ex.Message);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return from item in list
                   let station = item.CopyPropertiesToNew(typeof(BO.Station)) as BO.Station
                   orderby station.Code
                   select station;
        }

        public IEnumerable<Station> GetStationsBy(Predicate<Station> predicate)
        {
            List<DO.Station> list;
            try
            {
                list = (List<DO.Station>)myDal.GetAllStations();
            }
            catch (DO.BadStationIdException ex)
            {
                throw new BO.BadStationIdException(ex.ID, ex.Message);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return from item in list
                   let station = item.CopyPropertiesToNew(typeof(BO.Station)) as BO.Station
                   orderby station.Code
                   where predicate(station)
                   select station;
        }

        public void UpdateStation(Station station)
        {
            try
            {
                myDal.UpdateStation(station.Code, station.CopyPropertiesToNew(typeof(DO.Station)) as DO.Station);
            }
            catch (DO.BadStationIdException ex)
            {
                throw new BO.BadStationIdException(station.Code, ex.Message, ex);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Station RemoveStation(int Code)
        {
            try
            {
                return myDal.DeleteStation(Code).CopyPropertiesToNew(typeof(BO.Station)) as BO.Station;
            }
            catch (DO.BadStationIdException ex)
            {
                throw new BO.BadStationIdException(Code, ex.Message, ex);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region LineStation

        public void AddLineStation(LineStation station)
        {
            try
            {
                GetStation(station.Station);
                List<DO.LineStation> stations = myDal.GetAllLineStationsBy(p => p.Station != station.Station && p.LineID == station.LineID).ToList();
                foreach (var item in stations)
                {
                    if (item.LineStationIndex >= station.LineStationIndex)
                    {
                        item.LineStationIndex++;
                    }
                    if (item.LineStationIndex == (station.LineStationIndex) - 1)
                    {
                        item.NextStation = station.Station;
                    }
                    if (item.LineStationIndex == (station.LineStationIndex) + 1)
                    {
                        item.PrevStation = station.Station;
                    }
                    myDal.UpdateLineStation(item);
                }
                myDal.AddLineStation(station.CopyPropertiesToNew(typeof(DO.LineStation)) as DO.LineStation);
                buildAdjecntStations(station.LineID);
            }
            catch (DO.BadLSIdException ex)
            {
                throw new BO.BadLSIdException(ex.ID, ex.Message, ex);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);

            }
            catch (Exception ex)
            {
                throw new BO.BadLSIdException(station.LineID, ex.Message);
            }
        }

        public IEnumerable<LineStation> GetAllLineStations()
        {
            try
            {
                List<DO.LineStation> list = myDal.GetAllLineStations().ToList();
                return from item in list
                       let station = item.CopyPropertiesToNew(typeof(BO.LineStation)) as BO.LineStation
                       orderby station.Station
                       select station;
            }
            catch (DO.BadLSIdException ex)
            {
                throw new BO.BadLSIdException(ex.ID, ex.Message, ex);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<LineStation> GetAllLineStationsBy(Predicate<LineStation> perdicate)
        {
            List<DO.LineStation> list;
            try
            {
                list = myDal.GetAllLineStations().ToList();
                list = (from LS in list
                        let temp = LS.CopyPropertiesToNew(typeof(BO.LineStation)) as BO.LineStation
                        where perdicate(temp)
                        select LS).ToList();


            }
            catch (DO.BadLSIdException ex)
            {
                throw new BO.BadLSIdException(ex.ID, ex.Message, ex);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (list == null || list.Count == 0)
            {
                throw new BO.BadLSIdException(0, "No Stations For This predicate");
            }
            return from item in list
                   let station = item.CopyPropertiesToNew(typeof(BO.LineStation)) as BO.LineStation
                   orderby station.Station
                   select station;
        }

        public LineStation GetLineStation(int Code, int line)
        {
            DO.LineStation foundStation;
            try
            {
                foundStation = myDal.GetLineStation(Code, line);
            }
            catch (DO.BadLSIdException ex)
            {
                throw new BO.BadLSIdException(ex.ID, ex.Message, ex);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return foundStation.CopyPropertiesToNew(typeof(BO.LineStation)) as BO.LineStation;
        }

        public void UpdateLineStation(LineStation station)
        {
            try
            {
                myDal.UpdateLineStation(station.CopyPropertiesToNew(typeof(DO.LineStation)) as DO.LineStation);
                List<DO.LineStation> stations = myDal.GetAllLineStationsBy(p => p.LineStationIndex >= station.LineStationIndex && p.Station != station.Station && p.LineID == station.LineID).ToList();
                foreach (var item in stations)
                {
                    item.LineStationIndex++;
                    myDal.UpdateLineStation(item);
                }

            }
            catch (DO.BadLSIdException ex)
            {
                throw new BO.BadLSIdException(ex.ID, ex.Message, ex);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LineStation DeleteLineStation(int Code, int line)
        {
            try
            {
                LineStation station = myDal.GetLineStation(Code, line).CopyPropertiesToNew(typeof(BO.LineStation)) as BO.LineStation;
                List<DO.LineStation> stations = myDal.GetAllLineStationsBy(p => p.LineStationIndex > station.LineStationIndex && p.Station != station.Station && p.LineID == station.LineID).ToList();
                foreach (var item in stations)
                {
                    item.LineStationIndex--;
                    myDal.UpdateLineStation(item);
                }
                return myDal.DeleteLineStation(Code, line).CopyPropertiesToNew(typeof(BO.LineStation)) as BO.LineStation;
            }
            catch (DO.BadLSIdException ex)
            {
                throw new BO.BadLSIdException(ex.ID, ex.Message, ex);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Adjacent

        public void AddAdjacentStations(AdjacentStations adjacentstation)
        {
            try
            {
                myDal.AddAdjacentStations(adjacentstation.CopyPropertiesToNew(typeof(DO.AdjacentStations)) as DO.AdjacentStations);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Stations already exists") { }
                else
                {
                    throw new BO.BadAdjacentIdException(adjacentstation.Station1, adjacentstation.Station1, "not added", ex);
                }
            }

        }

        public AdjacentStations GetAdjacentStations(int station1, int station2)
        {
            DO.AdjacentStations foundStation;
            try
            {
                foundStation = myDal.GetAdjacentStations(station1, station2);
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BO.BadAdjacentIdException(station1, station1, "cant find Adjacent Stations", ex);
            }
            return foundStation.CopyPropertiesToNew(typeof(BO.AdjacentStations)) as BO.AdjacentStations;
        }

        public IEnumerable<AdjacentStations> GetAllAdjacentStations()
        {
            List<DO.AdjacentStations> list;
            try
            {
                list = myDal.GetAllAdjacentStations().ToList();
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return from item in list
                   let Adjacent = item.CopyPropertiesToNew(typeof(BO.AdjacentStations)) as BO.AdjacentStations
                   select Adjacent;
        }

        public void UpdateAdjacentStations(AdjacentStations adjacentstations)
        {
            try
            {
                myDal.UpdateAdjacentStations(adjacentstations.CopyPropertiesToNew(typeof(DO.AdjacentStations)) as DO.AdjacentStations);

            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {

                throw new BO.BadAdjacentIdException(adjacentstations.Station1, adjacentstations.Station1, ex.Message, ex);
            }
        }

        public AdjacentStations RemoveAdjacentStations(int station1, int station2)
        {
            try
            {
                return myDal.RemoveAdjacentStations(station1, station2).CopyPropertiesToNew(typeof(BO.AdjacentStations)) as BO.AdjacentStations;
            }
            catch (DO.XMLFileLoadCreateException ex)
            {
                throw new BO.XMLFileLoadCreateException(ex.Path, ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BO.BadAdjacentIdException(station1, station2, ex.Message, ex);
            }
        }

        #endregion

        #region simulation



        public void StartSimulator(TimeSpan time, int second, Action<TimeSpan> updateTime)
        {

            Clock.instance.Rate = second;
            Clock.instance.Start = time;
            Clock.instance.TimeChangeEvent += updateTime;
            Clock.instance.Cancel = false;
            Clock.instance.StartClock();

            //LineDispatcher.Instance.StartDispatch();
        }

        public void StopSimulator()
        {
            Clock.instance.StopClock();
            //LineDispatcher.Instance.StopDispatch();
        }
        public void SetStationPanel(int station, Action<LineTiming> updateBus)
        {
            throw new NotImplementedException();
        }

        public TimeSpan TimeBetweenTwo(int station1, int station2, int lineID)
        {

            TimeSpan totalTime = new TimeSpan(0, 0, 0);
            List<AdjacentStations> adlist = buildAdjecntStations(lineID);
            List<LineStation> lineStations = GetAllLineStationsBy(p => p.LineID == lineID).ToList();
            lineStations = (from item in lineStations
                            orderby item.LineStationIndex
                            select item).ToList();
            LineStation ls1 = lineStations.Find(p => p.Station == station1);
            LineStation ls2 = lineStations.Find(p => p.Station == station2);
            if (ls1.LineStationIndex > ls2.LineStationIndex)
            {
                for (int i = ls2.LineStationIndex; i < ls1.LineStationIndex; i++)
                {
                    LineStation tempLs = lineStations.Find(p => p.LineStationIndex == i);
                    totalTime += adlist.Find(p => (p.Station1 == tempLs.Station && p.Station2 == tempLs.NextStation) || (p.Station2 == tempLs.Station && p.Station1 == tempLs.NextStation)).Time;
                }
            }
            else if (ls1.LineStationIndex < ls2.LineStationIndex)
            {
                for (int i = ls1.LineStationIndex; i < ls2.LineStationIndex; i++)
                {
                    LineStation tempLs = lineStations.Find(p => p.LineStationIndex == i);
                    totalTime += adlist.Find(p => (p.Station1 == tempLs.Station && p.Station2 == tempLs.NextStation) || (p.Station2 == tempLs.Station && p.Station1 == tempLs.NextStation)).Time;
                }
            }
            return totalTime;
        }

        Random r = new Random();
        public List<AdjacentStations> buildAdjecntStations(int lineID)
        {
            List<LineStation> lineStations = GetAllLineStationsBy(p => p.LineID == lineID).ToList();
            List<Station> Stations = (from item in lineStations
                                      select GetAllStations().ToList().Find(p => p.Code == item.Station)).ToList();
            List<AdjacentStations> adStations = (from station in lineStations
                                                 let temp = GetAllAdjacentStations().ToList().Find(p => (p.Station1 == station.Station && p.Station2 == station.NextStation) || (p.Station2 == station.Station && p.Station1 == station.NextStation))
                                                 where temp != null
                                                 select temp).ToList();


            foreach (var item in adStations)
            {
                double distance = distanceBetweenTwo(item.Station1, item.Station2);
                double time = distance * (r.Next(30, 99) + r.NextDouble());
                int hour = Convert.ToInt32(Math.Floor(time));
                int minute = Convert.ToInt32(Math.Floor((time % 1) * 60));
                TimeSpan Time = new TimeSpan(hour, minute, 0);
                AdjacentStations temp = new AdjacentStations()
                {
                    Distance = distance,
                    Station1 = item.Station1,
                    Station2 = item.Station2,
                    Time = Time
                };
                AddAdjacentStations(temp);
            }

            return adStations;
        }

        public Double distanceBetweenTwo(int station1, int station2)
        {
            Station Station1 = GetStation(station1);
            Station Station2 = GetStation(station2);
            Double temp = 175 * (Math.Sqrt((Math.Pow((Station1.Longitude) - (Station2.Longitude), 2) + Math.Pow((Station1.Latitude) - (Station2.Latitude), 2))));
            return temp;
        }

        #endregion
    }
}