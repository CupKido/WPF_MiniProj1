using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DO
{
    public class BUS
    {
        public string licenseNum { get; set; }
        public DateTime FromDate { get; set; } = new DateTime(1, 1, 1);
        public DateTime lastime { get; set; } //last treatment
        public double TotalTrip { get; set; }
        public double ckm { get; set; } // km from last treatment
        public double FuelRemain { get; set; } = 1200;
        public bool Status { get; set; } // dangerous 
        public bool inproccess { get; set; } = false;
        

        //public object Clone()
        //{
        //    var BUS = (BUS)MemberwiseClone();
        //    BUS.ID = ID;
        //    BUS.startdate = startdate;
        //    BUS.lastime = lastime;
        //    BUS.km = km;
        //    BUS.ckm = ckm;
        //    BUS.Gaz = Gaz;
        //    BUS.dan = dan;
        //    BUS.inproc = inproc;
        //    BUS.totaltillret = totaltillret;
        //    return BUS;
        //}
    }
}
