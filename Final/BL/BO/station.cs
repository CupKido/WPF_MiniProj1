using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class station
    {
        public int ID { get; set; }

        public string name { get; set; }

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
