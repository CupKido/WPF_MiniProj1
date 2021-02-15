using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    public interface IBL
    {
        #region Simulation

        void StartSimulator(TimeSpan Time, int Second, Action<TimeSpan> updateTime);

        #endregion

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
        BO.Line GetLine(int ID, int code);
        IEnumerable<BO.Line> GetAllLines();
        IEnumerable<BO.Line> GetLinesBy(Predicate<BO.Line> predicate);
        void UpdateLine(BO.Line line);
        BO.Line RemoveLine(int ID);
        #endregion

        #region Stations
        void AddStation(BO.Station station);
        BO.Station GetStation(int ID);
        IEnumerable<BO.Station> GetAllStations();
        IEnumerable<BO.Station> GetStationsBy(Predicate<BO.Station> predicate);
        void UpdateStation(BO.Station station);
        BO.Station RemoveStation(int ID);
        #endregion

        #region Users

        void AddUser(BO.User user);

        IEnumerable<BO.User> GetAllUsers();

        BO.User GetUser(BO.User ThatUser);

        BO.User GetUser(string UserName);

        void UpdateUser(BO.User user);

        BO.User RemoveUser(BO.User user);

        #endregion

        #region LineStation
        void AddLineStation(LineStation station);

        IEnumerable<LineStation> GetAllLineStations();

        IEnumerable<LineStation> GetAllLineStationsBy(Predicate<LineStation> perdicate);

        LineStation GetLineStation(int Code, int line);


        void UpdateLineStation(LineStation station);


        LineStation DeleteLineStation(int Code, int line);

        #endregion

        #region adjacent

        void AddAdjacentStations(AdjacentStations adjacentstation);

        AdjacentStations GetAdjacentStations(int station1, int station2);

        IEnumerable<AdjacentStations> GetAllAdjacentStations();

        void UpdateAdjacentStations(AdjacentStations adjacentstations);

        AdjacentStations RemoveAdjacentStations(int station1, int station2);

        #endregion
        void SetStationPanel(int station, Action<LineTiming> updateBus);

    }
}
