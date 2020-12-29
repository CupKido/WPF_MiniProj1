using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Remoting.Messaging;
using DAL;

namespace BL
{
    public class MyBL
    {
        IDAL myDal = DALFactory.GetDAL();
        public void AddBus(BUS bus)
        {
            if (!myDal.IsExist(bus.currentID))
            {
                myDal.AddBus(bus);
            }
        }
        public void RemoveBus(BUS bus)
        {
            if (myDal.IsExist(bus.currentID))
            {
                myDal.RemoveBus(bus.currentID);
            }
        }
    }
}
