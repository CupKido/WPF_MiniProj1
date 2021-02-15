using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;



namespace BL
{
    class Clock
    {
        #region singleton

        static readonly Clock instacne = new Clock();

        static Clock() { }
        Clock() { }
        public static Clock instance { get => instacne; }

        #endregion

        internal volatile bool Cancel;

        public event Action<TimeSpan> TimeChangeEvent;

        public TimeSpan Start { get; set; }

        private TimeSpan time;

        public TimeSpan Time
        {

            get
            {
                return time;
            }

            set
            {
                time = value;
                TimeChangeEvent?.Invoke(value);
            }
        }

        public int Rate { get; set; }

        public void StartClock()
        {
            time = Start;
            Cancel = false;
            new Thread(() =>
            {
                while (!Cancel)
                {
                    Start = Time;
                    Thread.Sleep(Rate);
                    Time = Start.Add(new TimeSpan(0, 0, 1));
                }
            }).Start();
        }

        public void StopClock()
        {
            Start = new TimeSpan(0,0,0);
            Time = new TimeSpan(0,0,0);
            Cancel = true;
            Rate = 0;
            TimeChangeEvent = null;
        }


    }
}
