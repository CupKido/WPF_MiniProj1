using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

public class BUS
{
    
    private string ID;
    private Date startdate = new Date();
    private Date lastime = new Date(); //last treatment
    private double km;
    private double ckm; // km from last treatment
    private int Gaz = 1200;
    private bool dan = false; // dangerous 
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
    public Date Date
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
    public Date lastDate
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
    public int currentGaz
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

    //public void SetID(string newID) { ID = newID; }
    //public void Setstartdate(Date newDate) { startdate = newDate; }
    //public void Setkm(double newkm) { km = newkm; }
    //public string GetID() {  }
    //public Date Getstartdate() { return startdate; }
    //public double Getkm() { return km; }
    public BUS() {
        ID = "0";
        km = 0;
        Gaz = 1200;
        lastime = startdate;
    }

    public void SetBus(string id, int nday, int nmonth, int nyear, bool lt)
    {
        ID = id;
        startdate.SetDate(nday, nmonth, nyear);
        if (lt) { lastime = startdate; }
       
    }
    public void SetBus(string id, Date busdate, bool lt)
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
            if(buses[i].ID == ID)
            {
                notfound = false;
            }
        }
        if(notfound == true) { return -1; }
        return i;
    }
    public bool addkm(double addedkm, Date today)
    {
        if(treatmentneeded(today)) {
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
    public bool treatmentneeded(Date today)
    {
        if(dan == true) { Console.WriteLine("BUS SELECTED IS DANGEROUS\nPLEASE REPAIR!");
            return true;
        }
        //int day = 0, month = 0, year = 0;
        //bool isokay = false;
        //while (!isokay)
        //{
        //    isokay = true;
        //    Console.WriteLine("type today's date:");
        //    if (!(int.TryParse(Console.ReadLine(), out day))) { isokay = false; }
        //    if (!(int.TryParse(Console.ReadLine(), out month))) { isokay = false; }
        //    if (!(int.TryParse(Console.ReadLine(), out year))) { isokay = false; }
        //    if (isokay) { if (day < 1 || day > 30 || month < 1 || month > 12  || year < 1) { isokay = false; } }

        //    if (!isokay) { Console.WriteLine("\n******\nERROR\n******\n"); }
        //}
        //Date today = new Date(day, month, year);
        //Date today = new Date();
        //today.askDate();
        Date fromlast = today - lastime;
        if ((ckm) > 20000 || fromlast.year >= 1)
        {
            dan = true;
            return true;
        }
        return false;
    }
    public int fillGaz()
    {
        if(Gaz == 1200)
        {
            return 0;
        }
        int before = 1200 - Gaz;
        Gaz = 1200;
        return before;
    }
    public void repair(Date today)
    {
        ckm = 0;
        lastime = today;
        dan = false;
    }
    public void printBus()
    {
        Console.WriteLine("\nBUS ID: ");
        printID();
        Console.WriteLine("\nstarting date: {0}.{1}.{2}", startdate.day, startdate.month, startdate.year);
        Console.WriteLine("last reapir date: {0}.{1}.{2}", lastime.day, lastime.month, lastime.year);
        Console.WriteLine("overall km: {0}\nkm since last repair: {1}\nGaz tank: {2}/1200", km,ckm,Gaz);
    }
    public void printID()
    {
        string tempID = ID;
        if(startdate.year < 2018) 
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
        if(tyear > b.year)
        {
            return false;
        }
        if(tmonth < b.month)
        {
            return true;
        }
        if(tmonth > b.month)
        { return false; }
        if(tday < b.day) { return true; }
        if(tday > b.day) { return false; }
        return false; 
        

    }
    public static bool operator ==(Date a, Date b)
    {
        return (a.year == b.year && a.month == b.month && a.day == b.day);
    }
    public static bool operator !=(Date a, Date b)
    {
        return !(a.year == b.year && a.month == b.month && a.day == b.day);
    }
    public static Date operator -(Date a, Date b)
    {
        int nyear, nmonth, nday;
        
        if (a > b)
        {
            nyear = a.year - b.year;
            nmonth = a.month - b.month;
            nday = a.day - b.day;
            if(nday < 1)
            {
                nmonth = nmonth - 1;
                nday = nday + 30;
            }
            if(nmonth < 1)
            {
                nyear = nyear - 1;
                nmonth = nmonth + 12;
            }
            return new Date(nday, nmonth, nyear);
        }
        if (a == b)
        {
            return new Date(0, 0, 0);
        }
        return null;
    }
    public void SetDate(int nDay, int nMonth, int nYear)
    {
        day = nDay;
        month = nMonth;
        year = nYear;
    }

    public bool IsOkay(int nday, int nmonth, int nyear)
    {
        if(nday<1|| nday > 30) { return false; }
        if (nmonth < 1 || nmonth > 12) { return false; }
        if (nyear < 1) { return false; }
        SetDate(nday, nmonth, nyear);
        return true;
    }
    public void askDate(string ofwhat)
    {
        int nday = 0, nmonth = 0, nyear = 0;
        bool isokay = false;
        while (!isokay)
        {
            isokay = true;
            Console.WriteLine("type {0} date:", ofwhat);
            if (!(int.TryParse(Console.ReadLine(), out nday))) { isokay = false; }
            if (!(int.TryParse(Console.ReadLine(), out nmonth))) { isokay = false; }
            if (!(int.TryParse(Console.ReadLine(), out nyear))) { isokay = false; }
            if (isokay) { if (nday < 1 || nday > 30 || nmonth < 1 || nmonth > 12 || nyear < 1) { isokay = false; } }

            if (!isokay) { Console.WriteLine("\n******\nERROR\n******\n"); }
        }
        day = nday;
        month = nmonth;
        year = nyear;
    }
}
