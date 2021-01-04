using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class station
    {
        int ID { get; set; }

        string name { get; set; }

        double longitude { get; set; }
        double latitude { get; set; }

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
