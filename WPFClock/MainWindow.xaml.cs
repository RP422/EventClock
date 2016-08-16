using System;
using System.Windows;
using System.Windows.Controls;
using ClockWithEvents;

namespace WPFClock
{
    public partial class MainWindow : Window
    {
        private Clock ticker = new Clock();
        private delegate void NoArg();

        public MainWindow()
        {
            InitializeComponent();
            NoArg start = ticker.Start;
            start.BeginInvoke(null, null);
        }
        
        private void Ticker_UpdateMilliseconds(int currentTime)
        {
            MillisecondsDisplay.Content = currentTime.ToString("D3");
        }
        private void Ticker_UpdateSeconds(int currentTime)
        {
            SecondsDisplay.Content = currentTime.ToString("D2");
        }
        private void Ticker_UpdateMinutes(int currentTime)
        {
            MinutesDisplay.Content = currentTime.ToString("D2");
        }
        private void Ticker_UpdateHours(int currentTime)
        {
            HoursDisplay.Content = currentTime.ToString("D2");
        }
        
        private void Ticker_MillisecondsChangedOnDifferentThread(int currentTime)
        {
            MillisecondsDisplay.Dispatcher.BeginInvoke(new Action<int>(Ticker_UpdateMilliseconds), currentTime);
        }
        private void Ticker_SecondsChangedOnDifferentThread(int currentTime)
        {
            SecondsDisplay.Dispatcher.BeginInvoke(new Action<int>(Ticker_UpdateSeconds), currentTime);
        }
        private void Ticker_MinutesChangedOnDifferentThread(int currentTime)
        {
            MinutesDisplay.Dispatcher.BeginInvoke(new Action<int>(Ticker_UpdateMinutes), currentTime);
        }
        private void Ticker_HoursChangedOnDifferentThread(int currentTime)
        {
            HoursDisplay.Dispatcher.BeginInvoke(new Action<int>(Ticker_UpdateHours), currentTime);
        }

        private void HandleHours(object sender, RoutedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
            ManageHourWiring(box.IsChecked);
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