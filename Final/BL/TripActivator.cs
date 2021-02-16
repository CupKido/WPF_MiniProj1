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
        int StationCode;
        Action<LineTiming> Update;
        LineTiming trip; 

        void StartTrip(LineTrip Line)
        {
            trip.LineID = Line.LineID;
            if (bl.GetAllLines().ToList().FirstOrDefault(p => p.ID == Line.LineID) != null)
            {
                List<LineStation> stations = bl.GetAllLineStationsBy(p => p.LineID == trip.LineID).ToList();
                TimeSpan timeFromFirst = bl.TimeBetweenTwo(bl.GetAllLines().ToList().Find(p => p.ID == Line.LineID).FirstStation, StationCode, Line.LineID);
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