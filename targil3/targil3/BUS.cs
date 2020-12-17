using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace targil3B
{
    public class BUS
    {
        private string ID;
        private DateTime startdate = new DateTime(1, 1, 1);
        private DateTime lastime = new DateTime(); //last treatment
        private double km;
        private double ckm; // km from last treatment
        private double Gaz = 1200;
        private bool dan = false; // dangerous 
        //public string ID;
        //public DateTime startdate = new DateTime(1, 1, 1);
        //public DateTime lastime = new DateTime(); //last treatment
        //public double km;
        //public double ckm; // km from last treatment
        //public double Gaz = 1200;
        //public bool dan = false; // dangerous 

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
                return startdate.Date.ToShortDateString();
            }
            
        }
        public string plastDate
        {
            get
            {
                return lastime.Date.ToShortDateString();
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

        //public void SetID(string newID) { ID = newID; }
        //public void Setstartdate(Date newDate) { startdate = newDate; }
        //public void Setkm(double newkm) { km = newkm; }
        //public string GetID() {  }
        //public Date Getstartdate() { return startdate; }
        //public double Getkm() { return km; }
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
            if (treatmentneeded(today))
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
        public bool treatmentneeded(DateTime today)
        {
            if (dan == true)
            {
                Console.WriteLine("BUS SELECTED IS DANGEROUS\nPLEASE REPAIR!");
                return true;
            }
            int fromlast_y = today.Year;
            int fromlast_m = today.Month;
            int fromlast_d = today.Day;
            bool fromlast = false;
            fromlast_y = fromlast_y - lastime.Year;
            fromlast_m = fromlast_m - lastime.Month;
            fromlast_d = fromlast_d - lastime.Day;
            if (fromlast_y > 1) { fromlast = true; }
            else if (fromlast_m > 0) { fromlast = true; }
            else if (fromlast_d > 0) { fromlast = true; }
            if ((ckm) > 20000 || fromlast)
            {
                dan = true;
                return true;
            }
            return false;
        }
        public void fillGaz()
        {
            Thread.Sleep(720000);
            Gaz = 1200;
           
        }
        
        public void refillGazThreads()
        {

            new Thread(fillGaz).Start();

        }
        public void repair(DateTime today)
        {
            ckm = 0;
            lastime = today;
            dan = false;
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

    }
}


public class Date
{
    public int day = 0, month = 0, year = 0;
    public int sday
    {
        get
        {
            return day;
        }
        set
        {
            day = value;
        }
    }
    public int smonth
    {
        get
        {
            return month;
        }
        set
        {
            month = value;
        }
    }
    public int syear
    {
        get
        {
            return year;
        }
        set
        {
            year = value;
        }
    }
    //public void Setday(int newday) { day = newday; }
    //public int GetDay() { return day; }       
    //public void Setmonth(int newmonth) { month = newmonth; }
    //public int Getmonth() { return month; }
    //public void Setyear(int newyear) { year = newyear; }
    //public int Getyear() { return year; }
    //<<<<<<< HEAD
    //public Date()
    //{
    //    day = 0;
    //    month = 0;
    //    year = 0;
    //}
    //public Date(int nday, int nmonth, int nyear)
    //{
    //    day = nday;
    //    month = nmonth;
    //    year = nyear;
    //}
    //public static bool operator >(Date a, Date b)
    //{
    //    int tyear, tmonth, tday;
    //    tyear = a.year;
    //    tmonth = a.month;
    //    tday = a.day;
    //    if (tyear < b.year)
    //    {
    //        return false;
    //    }
    //    if (tyear > b.year)
    //    {
    //        return true;
    //    }
    //    if (tmonth < b.month)
    //    {
    //        return false;
    //    }
    //    if (tmonth > b.month)
    //    { return true; }
    //    if (tday < b.day) { return false; }
    //    if (tday > b.day) { return true; }
    //    return false;


    //}
    //public static bool operator <(Date a, Date b)
    //{
    //    int tyear, tmonth, tday;
    //    tyear = a.year;
    //    tmonth = a.month;
    //    tday = a.day;
    //    if (tyear < b.year) 
    //    {
    //        return true;
    //    }
    //    if(tyear > b.year)
    //    {
    //        return false;
    //    }
    //    if(tmonth < b.month)
    //    {
    //        return true;
    //    }
    //    if(tmonth > b.month)
    //    { return false; }
    //    if(tday < b.day) { return true; }
    //    if(tday > b.day) { return false; }
    //    return false; 
    public Date()
    {
        day = 0;
        month = 0;
        year = 0;
    }
    public Date(int nday, int nmonth, int nyear)
    {
        day = nday;
        month = nmonth;
        year = nyear;
    }
    public static bool operator >(Date a, Date b)
    {
        int tyear, tmonth, tday;
        tyear = a.year;
        tmonth = a.month;
        tday = a.day;
        if (tyear < b.year)
        {
            return false;
        }
        if (tyear > b.year)
        {
            return true;
        }
        if (tmonth < b.month)
        {
            return false;
        }
        if (tmonth > b.month)
        { return true; }
        if (tday < b.day) { return false; }
        if (tday > b.day) { return true; }
        return false;
    }
    public static bool operator <(Date a, Date b)
    {
        int tyear, tmonth, tday;
        tyear = a.year;
        tmonth = a.month;
        tday = a.day;
        if (tyear < b.year)
        {
            return true;
        }
        if (tyear > b.year)
        {
            return false;
        }
        if (tmonth < b.month)
        {
            return true;
        }
        if (tmonth > b.month)
        { return false; }
        if (tday < b.day) { return true; }
        if (tday > b.day) { return false; }
        return false;


        //}
        //public static bool operator ==(Date a, Date b)
        //{
        //    return (a.year == b.year && a.month == b.month && a.day == b.day);
        //}
        //public static bool operator !=(Date a, Date b)
        //{
        //    return !(a.year == b.year && a.month == b.month && a.day == b.day);
        //}
        //public static Date operator -(Date a, Date b)
        //{
        //    int nyear, nmonth, nday;

        //    if (a > b)
        //    {
        //        nyear = a.year - b.year;
        //        nmonth = a.month - b.month;
        //        nday = a.day - b.day;
        //        if(nday < 1)
        //        {
        //            nmonth = nmonth - 1;
        //            nday = nday + 30;
        //        }
        //        if(nmonth < 1)
        //        {
        //            nyear = nyear - 1;
        //            nmonth = nmonth + 12;
        //        }
        //        return new Date(nday, nmonth, nyear);
        //    }
        //    if (a == b)
        //    {
        //        return new Date(0, 0, 0);
        //    }
        //    return null;
        //}
        //public void SetDate(int nDay, int nMonth, int nYear)
        //{
        //    day = nDay;
        //    month = nMonth;
        //    year = nYear;
        //}

        //public bool IsOkay(int nday, int nmonth, int nyear)
        //{
        //    if(nday<1|| nday > 30) { return false; }
        //    if (nmonth < 1 || nmonth > 12) { return false; }
        //    if (nyear < 1) { return false; }
        //    SetDate(nday, nmonth, nyear);
        //    return true;
        //}
        //public void askDate(string ofwhat)
        //{
        //    int nday = 0, nmonth = 0, nyear = 0;
        //    bool isokay = false;
        //    while (!isokay)
        //    {
        //        isokay = true;
        //        Console.WriteLine("type {0} date:", ofwhat);
        //        if (!(int.TryParse(Console.ReadLine(), out nday))) { isokay = false; }
        //        if (!(int.TryParse(Console.ReadLine(), out nmonth))) { isokay = false; }
        //        if (!(int.TryParse(Console.ReadLine(), out nyear))) { isokay = false; }
        //        if (isokay) { if (nday < 1 || nday > 30 || nmonth < 1 || nmonth > 12 || nyear < 1) { isokay = false; } }

        //        if (!isokay) { Console.WriteLine("\n******\nERROR\n******\n"); }
        //    }
        //    day = nday;
        //    month = nmonth;
        //    year = nyear;
        //}
    }
}