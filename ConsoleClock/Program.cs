using ClockWithEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClock
{
    class Program
    {
        Clock ticker;

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run();
        }

        private void Run()
        {
            //This line no longer works because we changed the publicly exposed delegate to an event
            //c.secondsChanged = MyClockSecondsHaveChanged;

            //These lines are equivalent, they just point to different methods
            ticker.SecondChanged += new Clock.TimeChangedDelegate(SecondsChangedHandler);
            ticker.MinuteChanged += MinutesChangedHandler;
            
            ticker.Start();
        }

        public Program()
        {
            ticker = new Clock();            
        }

        private void MinutesChangedHandler(int minutes)
        {
            Console.WriteLine(minutes.ToString() + "Minutes");
        }


        void SecondsChangedHandler(int seconds)
        {
            Console.WriteLine(seconds.ToString() + "Seconds");
        }
        

    }
}
