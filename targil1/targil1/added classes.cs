using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BUS
{
    
    private string ID;
    private Date startdate = new Date();
    private Date lastime = new Date(); //last treatment
    private double km;
    private int Gaz;
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
    public double currentkm
    {
        get
        {
            return km;
        }
        set
        {
            km = value;
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
        Gaz = 0;
    }
    public void SetBus(string id, int nday, int nmonth, int nyear)
    {
        ID = id;
        startdate.SetDate(nday, nmonth, nyear);
    }
    public void SetBus(string id, Date busdate)
    {
        ID = id;
        startdate = busdate;
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
}
