using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Line
    {
        public int ID { set; get; } //Key
        public int Code { set; get; }
        public Areas Area { set; get; }
        public int FirstStation { set; get; }
        public int LastStation { set; get; }
    }
}
