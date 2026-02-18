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
using VendingSystemMobile.Ado;

namespace VendingSystemMobile.Windows
{
    /// <summary>
    /// Логика взаимодействия для GeneralWindow.xaml
    /// </summary>
    public partial class GeneralWindow : Window
    {
        public GeneralWindow()
        {
            InitializeComponent();

            LoadData();
        }

        private void LoadData()
        {
            ServicesListView.ItemsSource = AppData.db.VendingMachines.ToList();
        }

        private void WhiteThemeButtonClick(object sender, RoutedEventArgs e)
        {
            TopGrid.Background = Brushes.White;
            BottomGrid.Background = Brushes.White;
            CenterGrid.Background = Brushes.GhostWhite;
            ServicesListView.Background = Brushes.GhostWhite;
        }

        private void BlackThemeButtonClick(object sender, RoutedEventArgs e)
        {
            TopGrid.Background = Brushes.DimGray;
            BottomGrid.Background = Brushes.DimGray;
            CenterGrid.Background = Brushes.DarkGray;
            ServicesListView.Background = Brushes.DarkGray;
        }

        private void ApplyButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void DeclineButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void CameraButtonClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("microsoft.windows.camera:");
        }
    }
}
