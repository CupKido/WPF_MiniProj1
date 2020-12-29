using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;
namespace DAL
{
    public class BUSDAL
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
        public BUSDAL(BUSDS bus)
        {
            ID = bus.ID;
            startdate = bus.startdate;
            km = bus.km;
            ckm = bus.ckm;
            Gaz = bus.Gaz;
            dan = bus.dan;
            inproc = bus.inproc;
            totaltillret = bus.totaltillret;
        }
    }
}
