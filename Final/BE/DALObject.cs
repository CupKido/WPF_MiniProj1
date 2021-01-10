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
using DO;

namespace DAL  
{
    class DALObject : IDAL
    {
        static readonly DLObject instance = new DLObject();
        static DLObject() { }// static ctor to ensure instance init is done just before first usage
        DALObject() { } // default => private
        public static DLObject Instance { get => instance; }
        ArrayList Buses = new ArrayList();
        public DALObject()
        {
            Buses = DS.myDS.Buses;
        }
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
