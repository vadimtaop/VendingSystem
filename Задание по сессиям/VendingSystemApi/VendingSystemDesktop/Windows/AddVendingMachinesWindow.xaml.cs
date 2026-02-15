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
using VendingSystemDesktop.Ado;

namespace VendingSystemDesktop.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddVendingMachinesWindow.xaml
    /// </summary>
    public partial class AddVendingMachinesWindow : Window
    {
        public AddVendingMachinesWindow()
        {
            InitializeComponent();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(NameTextBox.Text))
                {
                    MessageBox.Show("Заполните поле: Название");
                    return;
                }

                if (string.IsNullOrEmpty(ModelTextBox.Text))
                {
                    MessageBox.Show("Заполните поле: Модель");
                    return;
                }

                if (string.IsNullOrEmpty(CompanyTextBox.Text))
                {
                    MessageBox.Show("Заполните поле: Компания");
                    return;
                }

                if (string.IsNullOrEmpty(ModemComboBox.Text))
                {
                    MessageBox.Show("Заполните поле: Модем");
                    return;
                }

                if (string.IsNullOrEmpty(LocationTextBox.Text))
                {
                    MessageBox.Show("Заполните поле: Адресс");
                    return;
                }

                if (string.IsNullOrEmpty(InstallDateDatePicker.Text))
                {
                    MessageBox.Show("Заполните поле: Дата установки");
                    return;
                }



                VendingMachines vendingMachines = new VendingMachines();

                vendingMachines.Name = NameTextBox.Text;
                vendingMachines.Model = ModelTextBox.Text;
                vendingMachines.Company = CompanyTextBox.Text;

                ComboBoxItem comboBoxItem = (ComboBoxItem)ModemComboBox.SelectedItem;
                vendingMachines.Modem = comboBoxItem.Content.ToString();

                vendingMachines.Location = LocationTextBox.Text;
                vendingMachines.InstallDate = InstallDateDatePicker.SelectedDate.Value;

                AppData.db.VendingMachines.Add(vendingMachines);
                AppData.db.SaveChanges();

                MessageBox.Show("Данные успешно сохранены");

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex}");
            }
        }
    }
}
