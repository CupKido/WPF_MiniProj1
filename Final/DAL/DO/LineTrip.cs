using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineTrip
    {
        public int ID { set; get; }
        public int LineID { set; get; }
        public TimeSpan StartAt { set; get; }
        public TimeSpan Frequency { set; get; }
        public TimeSpan FinishAt { set; get; }
    }
}
