using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class BusOnTrip
    {
        int ID { set; get; }
        int LicenseNum { set; get; }
        int LineID { set; get; }
        TimeSpan PlannedTakeOff { set; get; }
        TimeSpan ActualTakeOff { set; get; }
        int PrevStation;
        TimeSpan PrevStationAt { set; get; }
        TimeSpan NextStationAt { set; get; }

    }
}
