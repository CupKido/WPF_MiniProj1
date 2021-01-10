using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Station
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public double longitude { get; set; }
        public double latitude { get; set; }

        //public object Clone()
        //{
        //    var sta = (station)MemberwiseClone();
        //    sta.ID = ID;
        //    sta.name = name;
        //    sta.longitude = longitude;
        //    sta.latitude = latitude;
        //    return sta;
        //}
    }
}
