using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusOnTrip
    {
        public int ID { set; get; }
        public int LicenseNum { set; get; }
        public int LineID { set; get; }
        public TimeSpan PlannedTakeOff { set; get; }
        public TimeSpan ActualTakeOff { set; get; }
        public int PrevStation;
        public TimeSpan PrevStationAt { set; get; }
        public TimeSpan NextStationAt { set; get; }

    }
}
