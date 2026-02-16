using Microsoft.Win32;
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
using VendingSystemDesktop.Ado;
using VendingSystemDesktop.Windows;

namespace VendingSystemDesktop.Pages
{
    /// <summary>
    /// Логика взаимодействия для VendingMachinesPage.xaml
    /// </summary>
    public partial class VendingMachinesPage : Page
    {
        public VendingMachinesPage()
        {
            InitializeComponent();

            LoadData();
        }

        private void LoadData()
        {
            VendingMachinesDataGrid.ItemsSource = AppData.db.VendingMachines.ToList();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            AddVendingMachinesWindow addVendingMachinesWindow = new AddVendingMachinesWindow();
            addVendingMachinesWindow.ShowDialog();

            LoadData();
        }

        private void ExportButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog { Filter = "CSV|*.csv" };
            if (dialog.ShowDialog() == true)
            {
                var data = (VendingMachinesDataGrid.ItemsSource as List<VendingMachines>).Select(x => $"{x.Name};{x.Model};{x.Location}");
            }
        }

        private void FilterTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterTextBox.Text))
            {
                LoadData();
                return;
            }

            VendingMachinesDataGrid.ItemsSource = AppData.db.VendingMachines.Where(v => v.Name.Contains(FilterTextBox.Text)).ToList();
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (VendingMachinesDataGrid.SelectedItem is VendingMachines selectedVendingMachine)
            {
                AppData.db.VendingMachines.Remove(selectedVendingMachine);
                AppData.db.SaveChanges();

                MessageBox.Show("Успешно удален (добавить подтверждение и try catch)");
            }
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void UnlinkButtonClick(object sender, RoutedEventArgs e)
        {
            if (VendingMachinesDataGrid.SelectedItem is VendingMachines selectedVendingMachine)
            {
                selectedVendingMachine.Modem = "";

                AppData.db.SaveChanges();

                MessageBox.Show("Модем успешно отвязан (добавить подтверждение и try catch)");
            }
        }

        private async void MessageButtonClick(object sender, RoutedEventArgs e)
        {
            if (MessagePopup.IsOpen == false)
            {
                MessagePopup.IsOpen = true;

                await Task.Delay(10000);

                MessagePopup.IsOpen = false;
            }
        }
    }
}
