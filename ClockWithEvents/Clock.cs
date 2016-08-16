using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockWithEvents
{
    public class Clock
    {
        public delegate void TimeChangedDelegate(int currentTime);
        public event TimeChangedDelegate MillisecondChanged;
        public event TimeChangedDelegate SecondChanged;
        public event TimeChangedDelegate MinuteChanged;
        public event TimeChangedDelegate HourChanged;

        public const int MILLISECONDS_IN_SECOND = 1000;
        public const int SECONDS_IN_MINUTE = 5;
        public const int MINUTES_IN_HOUR = 3;
        public const int HOURS_IN_DAY = 3;

        int milliseconds;

        public void Start()
        {
            for (int i = 0; i < 1000000; i++)
            {
                milliseconds++;
                System.Threading.Thread.Sleep(1);

                OnMillisecondChanged();
                if (milliseconds % MILLISECONDS_IN_SECOND == 0)
                {
                    OnSecondChanged();
                }
                if (milliseconds % (MILLISECONDS_IN_SECOND * SECONDS_IN_MINUTE) == 0)
                {
                    OnMinuteChanged();
                }
                if (milliseconds % (MILLISECONDS_IN_SECOND * SECONDS_IN_MINUTE * MINUTES_IN_HOUR) == 0)
                {
                    OnHourChanged();
                }
            }            
        }

        private void OnHourChanged()
        {
            if (HourChanged != null)
            {
                HourChanged.Invoke(milliseconds / (MILLISECONDS_IN_SECOND * SECONDS_IN_MINUTE * MINUTES_IN_HOUR) % HOURS_IN_DAY);
            }
        }
        private void OnMinuteChanged()
        {
            if (MinuteChanged != null)
            {
                MinuteChanged.Invoke(milliseconds / (MILLISECONDS_IN_SECOND * SECONDS_IN_MINUTE) % MINUTES_IN_HOUR);
            }
        }
        private void OnSecondChanged()
        {
            if (SecondChanged != null)
            {
                SecondChanged.Invoke(milliseconds / MILLISECONDS_IN_SECOND % SECONDS_IN_MINUTE);
            }
        }
        private void OnMillisecondChanged()
        {
            if (MillisecondChanged != null)
            {
                MillisecondChanged.Invoke(milliseconds % MILLISECONDS_IN_SECOND);
            }
        }
    }
}