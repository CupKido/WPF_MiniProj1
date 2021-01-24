using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IBL
    {
        void AddBus(BO.BUS bus);
        BO.BUS GetBUS(int id);
        IEnumerable<BO.BUS> GetAllBuses();
        IEnumerable<BO.BUS> GetBusesBy(Predicate<BO.BUS> predicate);

    }
}
