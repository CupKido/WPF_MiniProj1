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

namespace BL
{
    public class MyBL
    {
        IDAL myDal = DALFactory.GetDAL();

        #region Bus
        public void AddBus(BO.BUS bus)
        {
            try 
            {
                myDal.AddBus(BO.Convertors.BTDBus(bus));
            }
            catch(DO.BadBusIdException ex)
            {
                throw ex;
            }
        }
        public void RemoveBus(BO.BUS bus)
        {
            try
            {
                myDal.RemoveBus(bus.LicenseNum);
            }
            catch (DO.BadBusIdException ex)
            {

                throw ex;
            }
        }
        #region User
        public void AddUser(BO.User user)
        {
            try
            {
                myDal.AddUser(BO.Convertors.BTDUser(user));
            }
            catch (DO.BadUserNameException ex)
            {
                throw ex;
            }
        }
        public void RemoveUser(BO.User user)
        {
            try
            {
                myDal.DeleteUser(user.UserName);
            }
            catch (DO.BadUserNameException ex)
            {

                throw ex;
            }
        }
        #endregion
    }
}
#endregion
