using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Trip
    {
        public int ID { set; get; }
        public string UserName { set; get; }
        public int LineID { set; get; }
        public int InStation { set; get; }
        public TimeSpan InAt { set; get; }
        public int OutStation { set; get; }
        public TimeSpan OutAt { set; get; }
    }
}
