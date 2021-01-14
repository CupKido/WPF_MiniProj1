using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    class Convertors
    {
        public static DO.BUS BTDBus (BO.BUS bus)
        {
            DO.BUS bus1 = new DO.BUS();
            bus1.LicenseNum = bus.LicenseNum;
            bus1.FromDate = bus.FromDate;
            bus1.ckm = bus.ckm;
            bus1.FuelRemain = bus.FuelRemain;
            bus1.lastime = bus.lastime;
            bus1.TotalTrip = bus.TotalTrip;
            return bus1;
        }

        public static DO.User BTDUser (BO.User user)
        {
            DO.User user1 = new DO.User();
            user1.Admin = user.Admin;
            user1.Password = user.Password;
            user1.UserName = user.UserName;
            return user1;
        }

    }
}
