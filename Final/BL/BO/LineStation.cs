using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineStation : IComparable
    {
        public int LineID { set; get; }
        public int Station { set; get; }
        public int LineStationIndex { set; get; }
        public int PrevStation { set; get; }
        public int NextStation { set; get; }

        public int CompareTo(object station)
        {
            if (this.LineStationIndex < (station as LineStation).LineStationIndex)
            {
                return -1;
            }
            else if (this.LineStationIndex == (station as LineStation).LineStationIndex)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
