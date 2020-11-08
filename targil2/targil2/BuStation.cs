using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace targil2
{
    class BuStation
    {
        string ID = null;
        double Latitude = new double();
        double Longitude = new double();
        string Address;
        public override string ToString()
        {
            return "Bus station code: " + ID + ", " + Latitude + "°N " + Longitude + "°E";
        }
    }
}
