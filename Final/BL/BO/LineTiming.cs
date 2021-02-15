using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineTiming
    {
        public int Code{ get; set; }
        public int LineID { get; set; }
        public TimeSpan TripStartAt { get; set; }
        public string LastLineName { get; set; }
        public TimeSpan AtStation { get; set; }

    }
}
