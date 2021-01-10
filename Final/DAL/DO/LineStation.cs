using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DO
{
    class LineStation
    {
        int LineID { set; get; }
        int Station { set; get; }
        int LineStationIndex { set; get; }
        int PrevStation { set; get; }
        int NextStation { set; get; }
    }
}
