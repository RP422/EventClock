using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockWithEvents
{
    public class Clock
    {
        //This line defines a type.  It is similar in purpose to "public class AnInternalClass{...}"
        public delegate void TimeChangedDelegate(int currentTime);

        //This line declares a variable of a particular type; a custom delegate we created above.  
        //It is similar to the "public ... anInternalClassInstance" line below
        //Normally class level members should be private.  Events are public if we want external classes to handle them.
        public event TimeChangedDelegate MillisecondChanged;
        public event TimeChangedDelegate SecondChanged;
        public event TimeChangedDelegate MinuteChanged;
        public event TimeChangedDelegate HourChanged;

        public const int MILLISECONDS_IN_SECOND = 1000;
        public const int SECONDS_IN_MINUTE = 5;
        public const int MINUTES_IN_HOUR = 5;

        int milliseconds;

        public void Start()
        {
            //will throw, because anInternalClassInstance is not set to an instance
            //Console.WriteLine(anInternalClassInstance.MyProperty);

            for (int i = 0; i < 1000000; i++)
            {
                milliseconds++;
                //Sleeping for 1 millisecond is a little silly and inefficient, but it will help keep our math easy to follow
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

                //The Clock is no longer aware of the console.  It notifies calling code of changes through events
                //rather than writing or printing changes itself.
                //Console.WriteLine(seconds);
            }            
        }

        private void OnHourChanged()
        {
            if (HourChanged != null)
            {
                HourChanged.Invoke(milliseconds / (MILLISECONDS_IN_SECOND * SECONDS_IN_MINUTE * MINUTES_IN_HOUR));
            }
        }

        private void OnMinuteChanged()
        {
            if (MinuteChanged != null)
            {
                MinuteChanged.Invoke(milliseconds / (MILLISECONDS_IN_SECOND * SECONDS_IN_MINUTE));
            }
        }

        private void OnSecondChanged()
        {
            if (SecondChanged != null)
            {
                //this line is incorrect.  Fix it! :)
                SecondChanged.Invoke(milliseconds / MILLISECONDS_IN_SECOND);
            }
        }

        private void OnMillisecondChanged()
        {
            if (MillisecondChanged != null)
            {
                MillisecondChanged.Invoke(milliseconds);
            }
        }
    }
}
