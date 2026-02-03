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
using VendingSystemWPF.ADO;
using static System.Net.Mime.MediaTypeNames;

namespace VendingSystemWPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddVendingMachineWindow.xaml
    /// </summary>
    public partial class AddVendingMachineWindow : Window
    {
        public AddVendingMachineWindow()
        {
            InitializeComponent();

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                TypeComboBox.ItemsSource = AppData.db.MachineTypes.ToList();
                TypeComboBox.DisplayMemberPath = "Title";
                TypeComboBox.SelectedValuePath = "Id";

                StatusComboBox.ItemsSource = AppData.db.MachineStatuses.ToList();
                StatusComboBox.DisplayMemberPath = "Title";
                StatusComboBox.SelectedValuePath = "Id";

                CountryComboBox.ItemsSource = AppData.db.Countries.ToList();
                CountryComboBox.DisplayMemberPath = "Title";
                CountryComboBox.SelectedValuePath = "Id";

                CompanyComboBox.ItemsSource = AppData.db.Companies.ToList();
                CompanyComboBox.DisplayMemberPath = "Title";
                CompanyComboBox.SelectedValuePath = "Id";

                ModemComboBox.ItemsSource = AppData.db.Modems.ToList();
                ModemComboBox.DisplayMemberPath = "Title";
                ModemComboBox.SelectedValuePath = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}",
                    "ООО Торговые Автоматы",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                VendingMachines vendingMachines = new VendingMachines();

                vendingMachines.Name = NameTextBox.Text;
                vendingMachines.Address = AddressTextBox.Text;
                vendingMachines.Model = ModelTextBox.Text;

                vendingMachines.MachineTypeId = (int)TypeComboBox.SelectedValue;

                vendingMachines.SerialNumber = SerialNumberTextBox.Text;
                vendingMachines.InventoryNumber = InventoryNumberTextBox.Text;
                vendingMachines.Manufacturer = ManufacturerTextBox.Text;

                vendingMachines.ManufactureDate = ManufactureDatePicker.SelectedDate.Value;
                vendingMachines.StartDate = StartDatePicker.SelectedDate.Value;

                vendingMachines.ResourceHour = int.Parse(ResourceHourTextBox.Text);

                vendingMachines.StatusId = (int)StatusComboBox.SelectedValue;
                vendingMachines.CountryId = (int)CountryComboBox.SelectedValue;
                vendingMachines.CompanyId = (int)CompanyComboBox.SelectedValue;
                vendingMachines.ModemId = (int)ModemComboBox.SelectedValue;

                vendingMachines.TotalProfit = 1;
                vendingMachines.LastCheckDate = DateTime.Now;
                vendingMachines.InterCheckMonth =1;
                vendingMachines.NextCheckDate = DateTime.Now;
                vendingMachines.ServiceTime = 1;
                vendingMachines.InventoryDate = DateTime.Now;
                vendingMachines.CreateDate = DateTime.Now;

                AppData.db.VendingMachines.Add(vendingMachines);
                AppData.db.SaveChanges();

                MessageBox.Show("Данные успешно добавлены!",
                    "ООО Торговые Автоматы",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}",
                    "ООО Торговые Автоматы",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
