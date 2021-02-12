using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Station
    {
        public int Code { get; set; } //Key

        public string Name { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }

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
