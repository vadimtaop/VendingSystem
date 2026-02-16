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
using System.Windows.Shapes;

namespace VendingSystemWeb.Windows
{
    /// <summary>
    /// Логика взаимодействия для GeneralWindow.xaml
    /// </summary>
    public partial class GeneralWindow : Window
    {
        public GeneralWindow()
        {
            InitializeComponent();
        }

        private void VendingMachinesButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("../Pages/MainPage.xaml", UriKind.Relative);
        }

        private void CalendarButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("../Pages/CalendarPage.xaml", UriKind.Relative);
        }

        private void ScheduleButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("../Pages/SchedulePage.xaml", UriKind.Relative);
        }
    }
}
