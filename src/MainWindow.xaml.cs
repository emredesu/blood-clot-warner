﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace blood_clot_warner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime target_time;
        Timer timer;

        int wait_time_hours;
        int wait_time_minutes;
        int wait_time_seconds;

        public MainWindow()
        {
            InitializeComponent();

            // add minutes 1-60 to minutes ComboBox 
            for(int i = 0; i < 60; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = i.ToString();

                MinutesComboBox.Items.Add(item);
            }

            // add seconds 1-60 to seconds ComboBox
            for (int i = 0; i < 60; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = i.ToString();

                SecondsComboBox.Items.Add(item);
            }

            try
            {
                dynamic json_obj = JsonConvert.DeserializeObject(File.ReadAllText("config.json"));
                wait_time_hours = json_obj["wait_time_hours"];
                wait_time_minutes = json_obj["wait_time_minutes"];
                wait_time_seconds = json_obj["wait_time_seconds"];

                UpdateComboBoxValuesToJsonValues();
            }
            catch (DirectoryNotFoundException)
            {
                RecreateJsonWithDefaultValues();
            }
        }

        void RecreateJsonWithDefaultValues()
        {
            JObject new_json = new JObject();

            new_json["wait_time_hours"] = 2;
            new_json["wait_time_minutes"] = 0;
            new_json["wait_time_seconds"] = 0;

            using (StreamWriter file = File.CreateText("config.json"))
            {
                file.Write(new_json.ToString());
            }
        }

        void RecreateJsonWithExistingValues()
        {
            JObject new_json = new JObject();

            new_json["wait_time_hours"] = wait_time_hours;
            new_json["wait_time_minutes"] = wait_time_minutes;
            new_json["wait_time_seconds"] = wait_time_seconds;

            using (StreamWriter file = File.CreateText("config.json"))
            {
                file.Write(new_json.ToString());
            }
        }

        void UpdateComboBoxValuesToJsonValues()
        {
            if (wait_time_hours == 4 && (wait_time_minutes != 0 || wait_time_seconds != 0))
            {
                MessageBox.Show("Wait time can't be longer than 4 hours! Recreating the JSON file with default values.");
                RecreateJsonWithDefaultValues();

                wait_time_hours = 2;
                wait_time_minutes = 0;
                wait_time_seconds = 0;
                UpdateComboBoxValuesToJsonValues();
            }
            else
            {
                HoursComboBox.SelectedValue = wait_time_hours;
                MinutesComboBox.SelectedValue = wait_time_minutes;
                SecondsComboBox.SelectedValue = wait_time_seconds;
            }
        }

        public TimeSpan GetTimeSpanForTimer()
        {
            target_time = DateTime.Now;
            target_time = target_time.AddHours(wait_time_hours);
            target_time = target_time.AddMinutes(wait_time_minutes);
            target_time = target_time.AddSeconds(wait_time_seconds);

            TimeSpan timespan_until_then = target_time - DateTime.Now;
            return timespan_until_then;
        }

        public void OnSaveButtonClicked(object sender, RoutedEventArgs args)
        {
            try
            {
                int hours_passed = int.Parse(((ComboBoxItem)HoursComboBox.SelectedItem).Content.ToString());
                int minutes_passed = int.Parse(((ComboBoxItem)MinutesComboBox.SelectedItem).Content.ToString());
                int seconds_passed = int.Parse(((ComboBoxItem)SecondsComboBox.SelectedItem).Content.ToString());

                if (hours_passed == 4 && (minutes_passed != 0 || seconds_passed != 0))
                {
                    MessageBox.Show("The wait time can't be longer than 4 hours!");

                    // Return combobox values to previously existing values
                    HoursComboBox.SelectedValue = wait_time_hours;
                    MinutesComboBox.SelectedValue = wait_time_minutes;
                    SecondsComboBox.SelectedValue = wait_time_seconds;
                    return;
                }

                wait_time_hours = hours_passed;
                wait_time_minutes = minutes_passed;
                wait_time_seconds = seconds_passed;

                RecreateJsonWithExistingValues();
                StartTimer(GetTimeSpanForTimer());
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

        private void StartTimer(TimeSpan alert_time)
        {
            timer = new Timer(lambda =>
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
