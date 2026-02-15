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

namespace VendingSystemDesktop.Windows
{
    /// <summary>
    /// Логика взаимодействия для GeneralWindow.xaml
    /// </summary>
    public partial class GeneralWindow : Window
    {
        public GeneralWindow(string name, string role)
        {
            InitializeComponent();


            if (!string.IsNullOrEmpty(name))
            {
                ProfileNameTextBlock.Text = name;
            }
            if (!string.IsNullOrEmpty(role))
            {
                ProfileRoleTextBlock.Text = role;
            }
        }

        private void MainButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("../Pages/MainPage.xaml", UriKind.Relative);
        }

        private void MenuButtonClick(object sender, RoutedEventArgs e)
        {
            if (MenuColumnDefinition.Width.Value == 200)
            {
                MenuColumnDefinition.Width = new GridLength(30);
                MenuTitleTextBlock.Visibility = Visibility.Collapsed;
                MenuStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                MenuColumnDefinition.Width = new GridLength(200);
                MenuTitleTextBlock.Visibility = Visibility.Visible;
                MenuStackPanel.Visibility = Visibility.Visible;
            }
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

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            this.Close();
        }

        private void VendingMachinesButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Source = new Uri("../Pages/VendingMachinesPage.xaml", UriKind.Relative);
        }
    }
}
