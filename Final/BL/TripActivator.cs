using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BO;
using System.ComponentModel;

namespace BL
{
    class TripActivator
    {
        #region singletone
        static readonly TripActivator instance = new TripActivator();
        static TripActivator() { }// static ctor to ensure instance init is done just before first usage
        TripActivator() { } // default => private
        public static TripActivator Instance { get => instance; }
        #endregion

        private IBL bl = BLFactory.GetBL(1);
        public int StationCode
        {
            get
            {
                return stationCode;
            }


            set
            {
                stationCode = value;
                stationChanged.Invoke(stationCode);
            }
        }
        private int stationCode;
        public LineTiming trip;

        public event Action<int> stationChanged;
        bool cancel;
        //private IEnumerable<LineTrip> GetAllTripTimes()
        //{
        //    List<LineTrip> list = new List<LineTrip>();

        //    foreach (BO.LineTrip item in myDal.GetAllLineTrips())
        //    {
        //        BO.LineTrip ThisTS = new LineTrip();
        //        ThisTS.StartAt = item.StartAt;
        //        while (ThisTS.StartAt <= item.FinishAt)
        //        {
        //            list.Add(ThisTS);
        //            ThisTS.StartAt.Add(item.Frequency);
        //        }
        //    }
        //    return list;
        //}

        //public TimeSpan getClosestLT()
        //{
        //    List<TimeSpan> list = GetAllTripTimes().ToList();
        //    list.Sort();
        //    return (from item in list
        //            where Clock.instance.Time < item
        //            select item).FirstOrDefault();
        //}

        //void StartTripActivator()
        //{
        //    cancel = false;
        //    new Thread(() =>
        //    {
        //        TimeSpan temp = new TimeSpan();
        //        while (!cancel)
        //        {
        //            temp = getClosestLT();
        //            Thread.Sleep(Convert.ToInt32(Math.Floor((temp.TotalMilliseconds - Clock.instance.Time.TotalMilliseconds - 60) * (1000f / Clock.instance.Rate))));
        //            StartTrip
        //        }
        //    }).Start();

        //}
        void StartTrip(LineTrip Line)
        {
            trip.LineID = Line.LineID;
            if (bl.GetAllLines().ToList().FirstOrDefault(p => p.ID == Line.LineID) != null)
            {
                List<LineStation> stations = bl.GetAllLineStationsBy(p => p.LineID == trip.LineID).ToList();
                TimeSpan timeFromFirst = bl.TimeBetweenTwo(bl.GetAllLines().ToList().Find(p => p.ID == Line.LineID).FirstStation, stationCode, Line.LineID);
                TimeSpan frequncy = Line.StartAt;

                while (frequncy < Clock.instance.Time)
                {
                    frequncy.Add(Line.Frequency);
                }
                trip.TripStartAt = frequncy.Subtract(Line.Frequency);
                trip.AtStation = trip.TripStartAt.Add(timeFromFirst);
                trip.LastLineName = bl.GetStation(bl.GetAllLines().ToList().Find(p => p.ID == Line.ID).LastStation).Name;
            }
            else
                throw new BadLineIdException(Line.LineID, "Line can not be found");
        }
    }
}