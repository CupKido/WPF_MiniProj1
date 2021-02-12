using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IBL
    {
        #region Buses
        void AddBus(BO.BUS bus);
        BO.BUS GetBUS(int id);
        IEnumerable<BO.BUS> GetAllBuses();
        IEnumerable<BO.BUS> GetBusesBy(Predicate<BO.BUS> predicate);
        void UpdateBus(BO.BUS bus );
        BO.BUS RemoveBus(int LN);

        #endregion

        #region Lines
        void AddLine(BO.Line line);
        BO.Line GetLine(int ID);
        IEnumerable<BO.Line> GetAllLines();
        IEnumerable<BO.Line> GetLinesBy(Predicate<BO.Line> predicate);
        void UpdateLine(BO.Line line);
        BO.Line RemoveLine(int ID);
        #endregion

        #region Stations
        void AddStation(BO.Station station);
        BO.Station GetStaion(int ID);
        IEnumerable<BO.Station> GetAllStations();
        IEnumerable<BO.Station> GetStationsBy(Predicate<BO.Station> predicate);
        void UpdateStation(BO.Station station);
        BO.Station RemoveStation(int ID);
        #endregion
    }
}
