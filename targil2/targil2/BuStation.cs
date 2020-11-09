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
        public string GSID //GS for GetSet
        {
            get
            {
                return ID;
            }
            set
            {
                ID = value;
            }
        }
        public double GSLatitude //GS for GetSet
        {
            get
            {
                return Latitude;
            }
            set
            {
                Latitude = value;
            }
        }
        public double GSLongitude //GS for GetSet
        {
            get
            {
                return Longitude;
            }
            set
            {
                Longitude = value;
            }
        }
        public string GSAddress //GS for GetSet
        {
            get
            {
                return Address;
            }
            set
            {
                Address = value;
            }
        }
        public override string ToString()
        {
            return "Bus station code: " + ID + ", " + Latitude + "°N " + Longitude + "°E " + Address;
        }
        public BuStation(string newID, double newLatitude, double newLongitude, string newAddress)
        {
            ID = newID;
            Latitude = newLatitude;
            Longitude = newLongitude;
            Address = newAddress;
        }
        public BuStation(string newID, string newAddress)
        {
            ID = newID;
            Random r = new Random();
            Latitude = 31 + (r.NextDouble() * (33.3 - 31));
            Longitude = 34.3 + (r.NextDouble() * (35.5 - 34.3));
            Address = newAddress;
        }
        public BuStation()
        {
            Random r = new Random();
            Latitude = 31 + (r.NextDouble() * (33.3-31));
            Longitude = 34.3 + (r.NextDouble() * (35.5 - 34.3));
        }

    }
}
