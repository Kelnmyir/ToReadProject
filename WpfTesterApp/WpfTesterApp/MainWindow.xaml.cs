using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfTesterApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Closed += MainWindow_OnClosed;
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Do you really want to quit?", 
                "MyApp", 
                MessageBoxButton.YesNo, 
                MessageBoxImage.Asterisk);
            if (result == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            MessageBox.Show("Bye!");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string msg = "You clicked the button!";
            if ((bool)Application.Current.Properties["GodMode"])
                msg += " Cheater!";
            MessageBox.Show(msg);
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            this.Title = (e.GetPosition(this).ToString());
        }

        private void MyCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            ClickMe.Content = e.Key.ToString();
        }
    }
}
