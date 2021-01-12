using System;
using S7.Net;
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
using static SimpleScada.PlcSettings;
using SimpleScada.Screens;

namespace SimpleScada
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public PlcSettings plcConnect = new PlcSettings();
        public static MainScreen mainScreen;
        private bool lastState;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, EventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!cboxCpuType.SelectedItem.ToString().Equals("Simulation"))
            {
                plcConnect.Connect(cboxCpuType.SelectedItem.ToString(), txtIpAddress.Text, txtRack.Text, txtSlot.Text);
                if (plcConnect.IsConnected())
                {
                    mainScreen = new MainScreen();
                    mainScreen.Show();
                    App.Current.MainWindow.Hide();
                }
            }
            else
            {
                mainScreen = new MainScreen();
                mainScreen.Show();
                App.Current.MainWindow.Hide();
            }
        }

        private void cboxCpuType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (cboxCpuType.SelectedItem.ToString().Equals("Simulation"))
            {
                lastState = true;
                txtIpAddress.IsEnabled = false;
                txtRack.IsEnabled = false;
                txtSlot.IsEnabled = false;
            }
            else if(lastState && cboxCpuType.SelectedItem.ToString()!= "Simulation")
            {
                txtIpAddress.IsEnabled = true;
                txtRack.IsEnabled = true;
                txtSlot.IsEnabled = true;
                lastState = false;
            }
        }
    }
}
