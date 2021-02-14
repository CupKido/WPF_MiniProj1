using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineStation 
    {
        public int LineID { set; get; }
        public int Station { set; get; }
        public int LineStationIndex { set; get; }
        public int PrevStation { set; get; }
        public int NextStation { set; get; }
    }
}
