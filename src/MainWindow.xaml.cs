using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace blood_clot_warner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime target_time;
        Timer timer;

        public MainWindow()
        {
            InitializeComponent();

            // add minutes 1-60 to minutes ComboBox 
            for(int i = 0; i < 60; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = i.ToString();
                if (i == 0)
                    item.IsSelected = true;

                MinutesComboBox.Items.Add(item);
            }

            // add seconds 1-60 to seconds ComboBox
            for (int i = 0; i < 60; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = i.ToString();
                if (i == 0)
                    item.IsSelected = true;

                SecondsComboBox.Items.Add(item);
            }
        }

        public void OnSaveButtonClicked(object sender, RoutedEventArgs args)
        {
            try
            {
                int hours = int.Parse(((ComboBoxItem)HoursComboBox.SelectedItem).Content.ToString());
                int minutes = int.Parse(((ComboBoxItem)MinutesComboBox.SelectedItem).Content.ToString());
                int seconds = int.Parse(((ComboBoxItem)SecondsComboBox.SelectedItem).Content.ToString());

                if (hours == 4 && (minutes != 0 || seconds != 0))
                {
                    MessageBox.Show("The wait time can't be longer than 4 hours!");
                    return;
                }

                target_time = DateTime.Now;
                target_time = target_time.AddHours(hours);
                target_time = target_time.AddMinutes(minutes);
                target_time = target_time.AddSeconds(seconds);

                TimeSpan timespan_until_then = target_time - DateTime.Now;
                SetTimer(timespan_until_then);
                MessageBox.Show($"Done! You will be alerted at {target_time.ToLongTimeString()}");
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went horribly wrong.");
            }
        }
        
        // todo: change the text on this button with a question mark image
        public void OnHelpButtonClicked(object sender, RoutedEventArgs args)
        {
            MessageBox.Show("todo :D");
        }

        // todo: change the text on this button with a github logo image
        public void OnGithubButtonClicked(object sender, RoutedEventArgs args)
        {
            MessageBox.Show("todo :D");
        }

        private void SetTimer(TimeSpan alert_time)
        {
            Timer timer = new Timer(lambda =>
            {
                ShowGetUpNotification(); // Call this function at the end of the passed time span.
            }, null, alert_time, Timeout.InfiniteTimeSpan);
        }

        private void ShowGetUpNotification()
        {
            MessageBox.Show("Time's up! Get up and do some stretching before returning to your chair.");
        } 
    }
}
