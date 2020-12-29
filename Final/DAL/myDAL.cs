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
using BE;

namespace DAL
{
    public class myDAL : IDAL
    {
        ArrayList Buses = new ArrayList();

        public void AddBus(BUS bus)
        {
            Buses.Add(bus);
        }
        public BUS RemoveBus(string ID)
        {
            int loc = indexByID(ID);
            BUS temp = null;
            if (loc != -1)
            {
                temp = (BUS)Buses[loc];
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
            foreach (BUS bus in Buses)
            {
                if (bus.currentID == ID)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }
        
    }
}
