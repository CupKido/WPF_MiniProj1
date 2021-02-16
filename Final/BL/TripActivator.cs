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

        //void StartTrip (int Lineid)
        //{
        //    LineTiming trip = new LineTiming() { LineID = Lineid } ;

        //    List<LineStation> stations = bl.GetAllLineStationsBy(p => p.LineID == trip.LineID).ToList();
        //    List<AdjacentStations> adjacentStations = (from station in stations
        //                                               select bl.GetAllAdjacentStations().ToList().Find(p => (p.Station1 == station.Station && p.Station2 == station.NextStation)|| (p.Station2 == station.Station && p.Station1 == station.NextStation))).ToList();
        //    trip.
        //}
    }
}
