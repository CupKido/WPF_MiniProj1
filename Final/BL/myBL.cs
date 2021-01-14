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
using BO;

//BO.Convertors.BTDBus
namespace BL
{
    public class MyBL
    {
        IDAL myDal = DALFactory.GetDAL();
        #region Bus

        public BO.BUS GetBUS(int ID)
        {
            DO.BUS bus = new DO.BUS();
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
            throw  new DO.BadBusIdException(bus.LicenseNum,"'");

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

                        throw ex;
                    }
                }
                else throw new DO.BadBusIdException(bus.LicenseNum, "starting service date cant be in the future");
            }
            else throw new DO.BadBusIdException(bus.LicenseNum, "invaild ID");
        }

        public void RemoveBus(BO.BUS bus )
        {
            //if (user.Admin)
            //{

                try
                {
                    myDal.RemoveBus(bus.LicenseNum);
                }
                catch (DO.BadBusIdException ex)
                {

                    throw ex;
                }
            //}
            //else throw "";
        }

        public IEnumerable<BO.BUS> GetAllBuses()
        {
            List<DO.BUS> list = (List<DO.BUS>)myDal.GetAllBusesBy(x => x.LicenseNum != 0);
            return from item in list
                   let bus = (BO.BUS)item.CopyPropertiesToNew(typeof(BO.BUS))
                   orderby bus.LicenseNum
                   select bus;
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
                    throw ex;
                }
            //}
            //else throw " ";
        }
        public void RemoveUser(BO.User user)
        {
            //if (Auser.Admin)
            //{
                try
                {
                    myDal.DeleteUser(user.UserName);
                }
                catch (DO.BadUserNameException ex)
                {

                    throw ex;
                }
            //}
            //else throw "";
        }

        public IEnumerable<BO.User> GetAllUseres()
        {
            List<DO.User> list = (List<DO.User>)myDal.GetAllUsers();
            return from item in list
                   let Tuser = (BO.User)item.CopyPropertiesToNew(typeof(BO.User))
                   orderby Tuser.UserName
                   select Tuser;
        }
       
        #endregion
    }
}

