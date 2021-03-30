using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using WindowsInput;
using WindowsInput.Native;

namespace AutoKeyApp.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;

        private Brush chosenColor = Brushes.LightSkyBlue;

        private bool isRunning = false;

        public MainWindow()
        {
            InitializeComponent();

            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(SimulateKey_Tick);


            LoadAllSettings();
        }

        private void WindowMove_MoseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            AutoKeySettings.Default.TimeSpan = int.Parse(TimeSpanTextBox.Text);
            AutoKeySettings.Default.Save();

            Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        protected override void OnClosing(CancelEventArgs e)
        {

            base.OnClosing(e);
        }


        private void LoadAllSettings()
        {
            LoadChosenButton(AutoKeySettings.Default.ChosenKey);
            LoadTimeFramButton(AutoKeySettings.Default.ChosenTimeSpan);
        }

        private void LoadChosenButton(string content)
        {
            switch (content)
            {
                case "F4":
                    F4Button.Background = chosenColor;
                    break;
                case "F5":
                    F5Button.Background = chosenColor;
                    break;
                case "F6":
                    F6Button.Background = chosenColor;
                    break;
            }
        }

        private void LoadTimeFramButton(string v)
        {
            switch (v)
            {
                case "Sec":
                    SecButton.Background = chosenColor;
                    break;
                case "Min":
                    MinButton.Background = chosenColor;
                    break;
                case "Hour":
                    HourButton.Background = chosenColor;
                    break;
            }
        }


        #region Timed Events
        private void SimulateKey_Tick(object sender, EventArgs e)
        {
            // TODO: simulate chosen Button Click

            InputSimulator keySim = new InputSimulator();
            string key = AutoKeySettings.Default.ChosenKey;

            switch (key)
            {
                case "F4":
                    keySim.Keyboard.KeyPress(VirtualKeyCode.F4);
                    break;
                case "F5":
                    keySim.Keyboard.KeyPress(VirtualKeyCode.F5);
                    break;
                case "F6":
                    keySim.Keyboard.KeyPress(VirtualKeyCode.F6);
                    break;
            }
        }
        #endregion

        private void ChosenButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: change choosen button

            F4Button.Background = Brushes.LightGray;
            F5Button.Background = Brushes.LightGray;
            F6Button.Background = Brushes.LightGray;

            Button chosen = sender as Button;
            LoadChosenButton(chosen.Content.ToString());

            AutoKeySettings.Default.ChosenKey = chosen.Content.ToString();
            AutoKeySettings.Default.Save();

        }



        private void TimeButtonChange_Click(object sender, RoutedEventArgs e)
        {
            // Update Time change
            SecButton.Background = Brushes.LightGray;
            MinButton.Background = Brushes.LightGray;
            HourButton.Background = Brushes.LightGray;

            Button chosen = sender as Button;
            LoadTimeFramButton(chosen.Content.ToString());

            AutoKeySettings.Default.ChosenTimeSpan = chosen.Content.ToString();
            AutoKeySettings.Default.Save();
        }



        private void StartTimer_Click(object sender, RoutedEventArgs e)
        {
            // TODO start and stop _timer


            string timeFrame = AutoKeySettings.Default.ChosenTimeSpan;

            int timeSpan = int.Parse(TimeSpanTextBox.Text);

            TimeSpan t = new TimeSpan(0, 0, 1);

            switch (timeFrame)
            {
                case "Sec":
                    t = new TimeSpan(0, 0, timeSpan);
                    break;
                case "Min":
                    t = new TimeSpan(0, timeSpan, 0);
                    break;
                case "Hour":
                    t = new TimeSpan(timeSpan, 0, 0);
                    break;

            }

            // set time interval
            _timer.Interval = t;


            if (isRunning)
            {
                _timer.Stop();
                StartButton.Content = "Start";
                isRunning = false;
            }
            else
            {
                _timer.Start();
                StartButton.Content = "Stop";
                isRunning = true;
            }
        }

    }
}
