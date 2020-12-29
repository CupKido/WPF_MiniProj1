using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;

namespace DAL
{
    public interface IDAL
    {
        void AddBus(BUSDS bus);
        BUSDS RemoveBus(String ID);
        bool IsExist(String ID);

    }
}
