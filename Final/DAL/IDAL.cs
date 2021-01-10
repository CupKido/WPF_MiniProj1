using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DAL
{
    public interface IDAL
    {
        #region Station
        void AddStation ()
        void AddBus(BUS bus);
        BUS RemoveBus(String ID);
        bool IsExist(String ID);

    }
}
