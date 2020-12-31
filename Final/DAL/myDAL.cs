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
using DS;

namespace DAL : 
{
    class myDAL : IDAL
    {
        ArrayList Buses = new ArrayList();
        public myDAL()
        {
            Buses = DS.myDS.Buses;
        }
        public void AddBus(BUSDAL bus)
        {
            Buses.Add(bus.);
        }
        public BUSDAL RemoveBus(string ID)
        {
            int loc = indexByID(ID);
            BUSDAL temp = null;
            if (loc != -1)
            {
                temp = (BUSDAL)Buses[loc];
                Buses.RemoveAt(loc);
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
            foreach (BUSDS bus in Buses)
            {
                if (bus.ID == ID)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }
        
    }
}
