using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS
{
    public class BUSDS : ICloneable
    {
        public string ID { get; set; }
        public DateTime startdate { get; set; } = new DateTime(1, 1, 1);
        public DateTime lastime { get; set; } //last treatment
        public double km { get; set; }
        public double ckm { get; set; } // km from last treatment
        public double Gaz { get; set; } = 1200;
        public bool dan { get; set; } // dangerous 
        public bool inproc { get; set; } = false;
        public TimeSpan totaltillret { get; set; } = new TimeSpan();

        public object Clone()
        {
            var BUS = (BUSDS)MemberwiseClone();
            BUS.ID = ID;
            BUS.startdate = startdate;
            BUS.lastime = lastime;
            BUS.km = km;
            BUS.ckm = ckm;
            BUS.Gaz = Gaz;
            BUS.dan = dan;
            BUS.inproc = inproc;
            BUS.totaltillret = totaltillret;
            return BUS;
        }
    }
}
