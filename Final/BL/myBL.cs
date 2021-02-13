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
using DLAPI;
using BO;

//BO.Convertors.BTDBus
namespace BL
{
    public class MyBL : IBL
    {

        IDAL myDal = DLFactory.GetDL();
        #region Bus

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
            BO.BUS bus1 =(BO.BUS)bus.CopyPropertiesToNew(typeof(BO.BUS));
            if (bus != null)
            {
                return bus1;
            }
            throw  new BO.BadBusIdException(bus.LicenseNum,"'");

        }
        public void AddBus(BO.BUS bus)
        {
            //if (user.Admin)
            //{
            if ((bus.LicenseNum.ToString().Length == 7 && bus.FromDate.Year < 2018) || (bus.LicenseNum.ToString().Length == 8 && bus.FromDate.Year >= 2018))
            {
                if (bus.FromDate <= DateTime.Now)
                {
                    try
                    {
                        myDal.AddBus((DO.BUS)bus.CopyPropertiesToNew(typeof(DO.BUS)));
                    }
                    catch (DO.BadBusIdException ex)
                    {

                        throw new BO.BadBusIdException(ex.ID,ex.Message,ex);
                    }
                }
                else throw new BO.BadBusIdException(bus.LicenseNum, "starting service date cant be in the future");
            }
            else throw new BO.BadBusIdException(bus.LicenseNum, "invaild ID");
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
            List<DO.BUS> list = myDal.GetAllBuses().ToList(); 
            return from item in list
                   let bus = (BO.BUS)item.CopyPropertiesToNew(typeof(BO.BUS))
                   orderby bus.LicenseNum
                   select bus;
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
                myDal.AddLine((DO.Line)line.CopyPropertiesToNew(typeof(DO.Line)));
                myDal.AddLineStation(new DO.LineStation() { LineID = line.ID, Station = line.FirstStation, LineStationIndex = 1 ,NextStation =line.LastStation });
                myDal.AddLineStation(new DO.LineStation() { LineID = line.ID, Station = line.LastStation, LineStationIndex = 0, PrevStation = line.FirstStation });
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(ex.ID, ex.Message, ex);
            }
            //}
            //else throw " ";
        }
       

        public IEnumerable<BO.Line> GetAllLines()
        {
            List<DO.Line> list = myDal.GetAllLines().ToList();
            //List<BO.Line> list1 = list.CopyPropertiesToNew(typeof(List<BO.Line>)) as List<BO.Line>;
            return from item in list
                   let line = item.CopyPropertiesToNew(typeof(BO.Line)) as BO.Line
                   orderby line.ID
                   select line;
        }

        public Line GetLine(int ID)
        {
            DO.Line foundLine;
            try
            {
                foundLine = myDal.GetLine(ID);
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
            catch(DO.BadLineIdException ex)
            {
                throw new BadLineIdException(0,"no lines exist!",ex);
            }
            return from item in list
                   let line = (BO.Line)item.CopyPropertiesToNew(typeof(BO.Line))
                   orderby line.ID
                   where predicate(line)
                   select line;
        }

        public Line RemoveLine(int ID)
        {
            try
            {
                return myDal.DeleteLine(ID).CopyPropertiesToNew(typeof(BO.Line)) as BO.Line;
            }
            catch (DO.BadLineIdException ex)
            {

                throw new BO.BadLineIdException(ex.ID, ex.Message, ex);
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
                    throw new BO.BadUserNameException(ex.ID, ex.Message, ex);
                }
            //}
            //else throw " ";
        }
        

        public IEnumerable<BO.User> GetAllUsers()
        {
            List<DO.User> list = (List<DO.User>)myDal.GetAllUsers();
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
            catch( Exception ex)
            {
                throw new Exception(ex.Message + "\n" + ThatUser.UserName);
            }
            if(ThatUser.Password != FoundUser.Password)
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
            catch
            {
                throw new Exception("User not found in System");
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
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadStationIdException(ex.ID, ex.Message, ex);
            }
        }

        public Station GetStation(int ID)
        {
            DO.Station foundStation;
            try
            {
                foundStation = myDal.GetStation(ID);
            }
            catch
            {
                throw new NotImplementedException();
            }
            return foundStation.CopyPropertiesToNew(typeof(BO.Station)) as BO.Station;
        }

        public IEnumerable<Station> GetAllStations()
        {
            List<DO.Station> list = myDal.GetAllStations().ToList();
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
            catch
            {
                throw new NotImplementedException();
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
            catch (DO.BadBusIdException ex)
            {

                throw new BadBusIdException(station.Code, ex.Message, ex);
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

                throw new BO.BadStationIdException(ex.ID, ex.Message, ex);
            }
        }

        #endregion

        #region LineStation

        public void AddLineStation(LineStation station)
        {
            try
            {
                myDal.AddLineStation(station.CopyPropertiesToNew(typeof(DO.LineStation)) as DO.LineStation);
            }
            catch (DO.BadStationIdException ex)
            {
                throw new BO.BadStationIdException(ex.ID, ex.Message, ex);
            }
        }

        public IEnumerable<LineStation> GetAllLineStations()
        {
            List<DO.LineStation> list = myDal.GetAllLineStations().ToList();
            return from item in list
                   let station = item.CopyPropertiesToNew(typeof(BO.LineStation)) as BO.LineStation
                   orderby station.Station
                   select station;
        }

        public IEnumerable<LineStation> GetAllLineStationsBy(Predicate<LineStation> perdicate)
        {
            List<DO.LineStation> list;
            try
            {
                list = (List<DO.LineStation>)myDal.GetAllLineStations();
            }
            catch
            {
                throw new NotImplementedException();
            }
            return from item in list
                   let station = item.CopyPropertiesToNew(typeof(BO.LineStation)) as BO.LineStation
                   orderby station.Station
                   where perdicate(station)
                   select station;
        }

        public LineStation GetLineStation(int Code, int line)
        {
            DO.LineStation foundStation;
            try
            {
                foundStation = myDal.GetLineStation(Code, line);
            }
            catch
            {
                throw new NotImplementedException();
            }
            return foundStation.CopyPropertiesToNew(typeof(BO.LineStation)) as BO.LineStation;
        }

        public void UpdateLineStation(LineStation station)
        {
            try
            {
                myDal.UpdateLineStation(station.CopyPropertiesToNew(typeof(DO.LineStation)) as DO.LineStation);
            }
            catch (DO.BadBusIdException ex)
            {

                throw new BadBusIdException(station.Station, ex.Message, ex);
            }
        }

        public LineStation DeleteLineStation(int Code, int line)
        {
            try
            {
                return myDal.DeleteLineStation(Code ,line).CopyPropertiesToNew(typeof(BO.LineStation)) as BO.LineStation;
            }
            catch (DO.BadStationIdException ex)
            {

                throw new BO.BadStationIdException(ex.ID, ex.Message, ex);
            }
        }

        #endregion

       
        

        

    }
}

