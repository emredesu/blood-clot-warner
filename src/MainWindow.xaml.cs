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

namespace blood_clot_warner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            MessageBox.Show("todo :D");
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
    }
}
