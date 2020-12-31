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
using DAL;
using BO;

namespace BL
{
    public class MyBL
    {
        IDAL myDal = DALFactory.GetDAL();
        public void AddBus(DAL.BUS bus)
        {
            if (!myDal.IsExist(bus.ID))
            {
                myDal.AddBus(bus);
            }
        }
        public void RemoveBus(BO.BUS bus)
        {
            if (myDal.IsExist(bus.ID))
            {
                myDal.RemoveBus(bus.ID);
            }
        }
    }
}
