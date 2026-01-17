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
using VendingSystemWPF.Pages;

namespace VendingSystemWPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для GeneralWindow.xaml
    /// </summary>
    public partial class GeneralWindow : Window
    {
        public GeneralWindow(string userFio, string userRole)
        {
            InitializeComponent();

            FioTextBlock.Text = userFio;
            RoleTextBlock.Text = userRole;
        }

        private void ProfileButtonClick(object sender, RoutedEventArgs e)
        {
            if (ProfilePopup.IsOpen == false)
            {
                ProfilePopup.IsOpen = true;
            }
            else
            {
                ProfilePopup.IsOpen = false;
            }
        }

        private void SidebarButtonClick(object sender, RoutedEventArgs e)
        {
            if (SidebarColumnDefinition.Width.Value == 200)
            {
                SidebarColumnDefinition.Width = new GridLength(30);
                SidebarStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                SidebarColumnDefinition.Width = new GridLength(200);
                SidebarStackPanel.Visibility = Visibility.Visible;
            }
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти?",
                    "ООО Торговые Автоматы",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                AuthWindow authWindow = new AuthWindow();
                authWindow.Show();
                this.Close();
            }
        }

        private void VendingMachinesButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new VendingMachinesPage());
        }

        private void MainButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MainPage());
        }
        private void MonitorTaButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MonitorPage());
        }
    }
}
