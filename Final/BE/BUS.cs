using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using BE;

namespace BE
{
    [Serializable()]
    public class BUS : ISerializable
    {
        private string ID;
        private DateTime startdate = new DateTime(1, 1, 1);
        private DateTime lastime = new DateTime(); //last treatment
        private double km;
        private double ckm; // km from last treatment
        private double Gaz = 1200;
        private bool dan = false; // dangerous 
        private bool inproc = false;
        //private MainWindow current1;
        //private BusDetails current2;
        TimeSpan totaltillret = new TimeSpan();

        public string currentID
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
        public DateTime Date
        {
            get
            {
                return startdate;
            }
            set
            {
                startdate = value;
            }
        }
        public DateTime lastDate
        {
            get
            {
                return lastime;
            }
            set
            {
                lastime = value;
            }
        }
        public double kmfs // km from start
        {
            get
            {
                return km;
            }
            set
            {
                if (value >= km) { km = value; }
            }
        }
        public double currentkm
        {
            get
            {
                return ckm;
            }
            set
            {
                if (value >= km) { km = value; }
            }
        }
        public double currentGaz
        {
            get
            {
                return Gaz;
            }
            set
            {
                Gaz = value;
            }
        }
        public bool inprocc
        {
            get
            {
                return inproc;
            }
        }
        public string pID
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
        public string pStartDate
        {
            get
            {
                string temp = startdate.Day + "/" + startdate.Month + "/" + startdate.Year;
                return temp;
            }

        }
        public string plastDate
        {
            get
            {
                string temp = lastime.Day + "/" + lastime.Month + "/" + lastime.Year;
                return temp;
            }

        }
        public string pkm // km from start
        {
            get
            {
                return string.Format("{0:0.00}", km);

            }

        }
        public string pckm
        {
            get
            {
                return string.Format("{0:0.00}", ckm);
            }

        }
        public string pGaz
        {
            get
            {
                return string.Format("{0:0.00}", Gaz);
            }
        }
        public string pColor
        {
            get
            {
                if (treatmentneeded(0))
                {

                    return "#FFB90606";
                }
                else if (inproc)
                {
                    return "#FF2317";
                }
                else
                {
                    return "#FF048B05";
                }
            }

        }
        public string pTime
        {
            get
            {
                string text = "";
                if (totaltillret.Minutes < 10)
                {
                    text += "0";
                }
                text += totaltillret.Minutes + ":";
                if (totaltillret.Seconds < 10)
                {
                    text += "0";
                }
                text += totaltillret.Seconds;
                return text;
            }

        }

        public BUS()
        {
            ID = "0";
            km = 0;
            Gaz = 1200;
            lastime = startdate;
        }
        public BUS(string nID, double nkm, DateTime start, DateTime last)
        {
            ID = nID;
            km = nkm;
            startdate = start;
            lastime = last;
            Gaz = 1200;
        }
        public BUS(string nID, double nkm, double nckm, DateTime start, DateTime last, double ngaz)
        {
            ID = nID;
            km = nkm;
            ckm = nckm;
            startdate = start;
            lastime = last;
            Gaz = ngaz;

        }

        public void SetBus(string id, int nday, int nmonth, int nyear, bool lt)
        {
            ID = id;
            startdate.AddDays(nday);
            startdate.AddMonths(nmonth);
            startdate.AddYears(nyear);
            if (lt) { lastime = startdate; }

        }
        public void SetBus(string id, DateTime busdate, bool lt)
        {
            ID = id;
            startdate = busdate;
            if (lt) { lastime = startdate; }
        }

        public int find(string ID, BUS[] buses)
        {
            bool notfound = true;
            int i = 0;
            for (; i < buses.Length && notfound; ++i)
            {
                if (buses[i].ID == ID)
                {
                    notfound = false;
                }
            }
            if (notfound == true) { return -1; }
            return i;
        }
        public bool addkm(double addedkm, DateTime today)
        {
            if (treatmentneeded(0))
            {
                Console.WriteLine("\n******\nERROR\nREPAIRING NEEDED\nPLEASE CHECK\n******\n");
                return false;
            }
            if (Gaz < addedkm)
            {
                Console.WriteLine("\n******\nERROR\nNOT ENOUGH GAZ\nPLEASE FILL\n******\n");
                return false;
            }
            km = km + addedkm;
            ckm = ckm + addedkm;
            Gaz = Gaz - (int)addedkm;

            return true;
        }
        public bool treatmentneeded(double nkm)
        {
            TimeSpan dif = DateTime.Now - lastDate;
            bool fromlast = false;
            if (dif.Days > 365)
            {
                fromlast = true;
            }
            if ((ckm + nkm) > 20000 || fromlast)
            {
                dan = true;
                return true;
            }
            return false;
        }
        
        
        

        
        public void printBus()
        {
            Console.WriteLine("\nBUS ID: ");
            printID();
            Console.WriteLine("\nstarting date: " + startdate.ToString("dd.MM.yyyy"));
            Console.WriteLine("last reapir date: " + lastime.ToString("dd.MM.yyyy"));
            Console.WriteLine("overall km: {0}\nkm since last repair: {1}\nGaz tank: {2}/1200", km, ckm, Gaz);
        }
        public void printID()
        {
            string tempID = ID;
            if (startdate.Year < 2018)
            {
                tempID = tempID.Insert(2, "-");
                tempID = tempID.Insert(6, "-");
            }
            else
            {
                tempID = tempID.Insert(3, "-");
                tempID = tempID.Insert(6, "-");
            }
            Console.WriteLine(tempID);

        }
        static Random r = new Random();
        public void addride()
        {

            double nkm = (r.Next() % 1200) + r.NextDouble();
            ckm += nkm;
            km += nkm;
            Gaz -= nkm;
        }



        
        public override string ToString()
        {
            return "Bus ID: " + ID;
        }
        public void addckm(double ackm)
        {
            ckm = ackm;
        }
        

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ID", ID);
            info.AddValue("SD", startdate);
            info.AddValue("LD", lastime);
            info.AddValue("ckm", ckm);
            info.AddValue("km", km);
            info.AddValue("gaz", currentGaz);
        }

        public BUS(SerializationInfo info, StreamingContext context)
        {
            ID = (string)info.GetValue("ID", typeof(string));
            startdate = (DateTime)info.GetValue("SD", typeof(DateTime));
            lastime = (DateTime)info.GetValue("LD", typeof(DateTime));
            ckm = (double)info.GetValue("ckm", typeof(double));
            km = (double)info.GetValue("km", typeof(double));
            currentGaz = (double)info.GetValue("gaz", typeof(double));
        }
    }
}
