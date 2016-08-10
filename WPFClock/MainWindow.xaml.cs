using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClockWithEvents;
using System.Windows.Threading;

namespace WPFClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Clock ticker = new Clock();

        private delegate void NoArg();

        public MainWindow()
        {
            InitializeComponent();

            /*
             * We could also use the Action type instead of NoArg and save some code.  
             * But then we'd need to understand Generics...
             * so I'll stick with NoArg for now.
            */
            NoArg start = ticker.Start;

            /*
             *  .NET prevents us from updating UI elements from another thread.
             *  Our clock uses Thread.Sleep which would make our app look like it crashed.
             *  We'll use a separate thread for the clock.Start method, then use the Dispatcher
             *  to update the UI in its own sweet time on its own sweet thread.  Think of 
             *  queueing up a message that is then processed by the UI thread when it's able.
             *  
             *  Importantly, we don't have to change the Clock class to take advantage of threading.
             *  All the Dispatch/BeginInvoke magic happens here in the client code.
             * 
             */
            
            /* 
            ticker.MillisecondChanged += Ticker_MillisecondsChangedOnDifferentThread;
            ticker.SecondChanged += Ticker_SecondsChangedOnDifferentThread;
            ticker.MinuteChanged += Ticker_MinutesChangedOnDifferentThread;
            ticker.HourChanged += Ticker_HoursChangedOnDifferentThread;
            */
            start.BeginInvoke(null, null);
            
        }
        
        private void Ticker_UpdateMilliseconds(int currentTime)
        {
            /*
             * This method is executed by the UI thread, and so can modify the label directly.
             */
            MillisecondsDisplay.Content = currentTime.ToString("D3");
        }
        private void Ticker_UpdateSeconds(int currentTime)
        {
            /*
             * This method is executed by the UI thread, and so can modify the label directly.
             */
            SecondsDisplay.Content = currentTime.ToString("D2");
        }
        private void Ticker_UpdateMinutes(int currentTime)
        {
            /*
             * This method is executed by the UI thread, and so can modify the label directly.
             */
            MinutesDisplay.Content = currentTime.ToString("D2");
        }
        private void Ticker_UpdateHours(int currentTime)
        {
            /*
             * This method is executed by the UI thread, and so can modify the label directly.
             */
            HoursDisplay.Content = currentTime.ToString("D2");
        }
        
        private void Ticker_MillisecondsChangedOnDifferentThread(int currentTime)
        {
            /*
             * Here's where the Clock's thread will put a message on the UI thread's queue of work,
             * again, through the use of a delegate
             */
            MillisecondsDisplay.Dispatcher.BeginInvoke(new Action<int>(Ticker_UpdateMilliseconds), currentTime);
        }
        private void Ticker_SecondsChangedOnDifferentThread(int currentTime)
        {
            /*
             * Here's where the Clock's thread will put a message on the UI thread's queue of work,
             * again, through the use of a delegate
             */
            SecondsDisplay.Dispatcher.BeginInvoke(new Action<int>(Ticker_UpdateSeconds), currentTime);
        }
        private void Ticker_MinutesChangedOnDifferentThread(int currentTime)
        {
            /*
             * Here's where the Clock's thread will put a message on the UI thread's queue of work,
             * again, through the use of a delegate
             */
            MinutesDisplay.Dispatcher.BeginInvoke(new Action<int>(Ticker_UpdateMinutes), currentTime);
        }
        private void Ticker_HoursChangedOnDifferentThread(int currentTime)
        {
            /*
             * Here's where the Clock's thread will put a message on the UI thread's queue of work,
             * again, through the use of a delegate
             */
            HoursDisplay.Dispatcher.BeginInvoke(new Action<int>(Ticker_UpdateHours), currentTime);
        }

        private void HandleHours(object sender, RoutedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
            ManageHourWiring(box.IsChecked);
            //HoursDisplay.Dispatcher.BeginInvoke(new Action<bool?>(ManageHourWiring), box.IsChecked);
        }
        private void ManageHourWiring(bool? flag)
        {
            if (flag == true)
            {
                ticker.HourChanged += Ticker_HoursChangedOnDifferentThread;
            }
            else
            {
                ticker.HourChanged -= Ticker_HoursChangedOnDifferentThread;
            }
        }
        private void HandleMinutes(object sender, RoutedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
            ManageMinuteWiring(box.IsChecked);
            //MinutesDisplay.Dispatcher.Invoke(new Action<bool?>(ManageMinuteWiring), box.IsChecked);
        }
        private void ManageMinuteWiring(bool? flag)
        {
            if (flag == true)
            {
                ticker.MinuteChanged += Ticker_MinutesChangedOnDifferentThread;
            }
            else
            {
                ticker.MinuteChanged -= Ticker_MinutesChangedOnDifferentThread;
            }
        }

        private void HandleSeconds(object sender, RoutedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
            ManageSecondWiring(box.IsChecked);
            //SecondsDisplay.Dispatcher.Invoke(new Action<bool?>(ManageSecondWiring), box.IsChecked);
        }
        private void ManageSecondWiring(bool? flag)
        {
            if (flag == true)
            {
                ticker.SecondChanged += Ticker_SecondsChangedOnDifferentThread;
            }
            else
            {
                ticker.SecondChanged -= Ticker_SecondsChangedOnDifferentThread;
            }
        }

        private void HandleMilliseconds(object sender, RoutedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
            ManageMillisecondWiring(box.IsChecked);
            //MillisecondsDisplay.Dispatcher.Invoke(new Action<bool?>(ManageMillisecondWiring), box.IsChecked);
        }
        private void ManageMillisecondWiring(bool? flag)
        {
            if (flag == true)
            {
                ticker.MillisecondChanged += Ticker_MillisecondsChangedOnDifferentThread;
            }
            else
            {
                ticker.MillisecondChanged -= Ticker_MillisecondsChangedOnDifferentThread;
            }
        }
    }
}
