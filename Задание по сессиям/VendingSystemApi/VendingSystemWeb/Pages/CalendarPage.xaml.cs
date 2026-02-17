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
using VendingSystemWeb.Ado;

namespace VendingSystemWeb.Pages
{
    /// <summary>
    /// Логика взаимодействия для CalendarPage.xaml
    /// </summary>
    public partial class CalendarPage : Page
    {
        public CalendarPage()
        {
            InitializeComponent();

            LoadData();
        }

        private void LoadData()
        {
            VendingMachinesDataGrid.ItemsSource = AppData.db.VendingMachines.ToList();
        }

        private void CalcButtonClick(object sender, RoutedEventArgs e)
        {
            if (VendingMachinesDataGrid.SelectedItem is VendingMachines selectedVendingMachines)
            {
                if (selectedVendingMachines.NextServiceDate == null)
                {
                    int months = selectedVendingMachines.IntervalService ?? 6; // Если будет глюк то возьмет 6
                    
                    DateTime startDate = selectedVendingMachines.InstallDate ?? DateTime.Now; // Возьмет сегодня если глюк

                    selectedVendingMachines.NextServiceDate = startDate.AddMonths(months);
                    selectedVendingMachines.StatusMachine = "Требуется ТО";
                    selectedVendingMachines.StatusService = "Новая";
                    selectedVendingMachines.NameUser = "Иванов И. И.";

                    AppData.db.SaveChanges();

                    LoadData();

                    MessageBox.Show("Новая дата успешно запланирована");
                }
                else
                {
                    MessageBox.Show("Дата уже запланирована");
                }
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
    }
}
