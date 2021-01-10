using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Line
    {
        public int ID { set; get; }
        public int Code { set; get; }
        public Areas Area { set; get; }
        public int FirstStation { set; get; }
        public int LastStation { set; get; }
    }
}
