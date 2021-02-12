using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DO
{
    public class BUS
    {
        public int LicenseNum { get; set; }
        public DateTime FromDate { get; set; } = new DateTime(1, 1, 1);
        public DateTime lastime { get; set; } //last treatment
        public double TotalTrip { get; set; }
        public double ckm { get; set; } // km from last treatment
        public double FuelRemain { get; set; } = 1200;
        public BusStatus status { set; get; }
        public BUS()
        {

        }
        public BUS(int LN, DateTime FD, DateTime LT, double Tkm)
        {
            LicenseNum = LN;
            FromDate = FD;
            lastime = LT;
            TotalTrip = Tkm;
        }
    }
}
