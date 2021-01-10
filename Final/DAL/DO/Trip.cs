using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DO
{
    class Trip
    {
        int ID { set; get; }
        string UserName { set; get; }
        int LineID { set; get; }
        int InStation { set; get; }
        TimeSpan InAt { set; get; }
        int OutStation { set; get; }
        TimeSpan OutAt { set; get; }
    }
}
