using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    class LineTrip
    {
        int ID { set; get; }
        int LineID { set; get; }
        TimeSpan StartAt { set; get; }
        TimeSpan Frequency { set; get; }
        TimeSpan FinishAt { set; get; }
    }
}
