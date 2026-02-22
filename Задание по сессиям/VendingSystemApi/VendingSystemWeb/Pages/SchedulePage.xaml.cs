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
    /// Логика взаимодействия для SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        public SchedulePage()
        {
            InitializeComponent();

            LoadData();
        }

        private void LoadData()
        {
            VendingMachinesDataGrid.ItemsSource = AppData.db.VendingMachines.ToList();
        }

        private void ApplyButtonClick(object sender, RoutedEventArgs e)
        {
            if (VendingMachinesDataGrid.SelectedItem is VendingMachines selectedVendingMachines)
            {
                StatusHistories statusHistories = new StatusHistories();

                statusHistories.VendingMachineId = selectedVendingMachines.VendingMachineId;
                statusHistories.OldStatus = selectedVendingMachines.StatusService;
                statusHistories.Date = DateTime.Now;


                selectedVendingMachines.StatusMachine = "На ремонте/на обслуживании";
                selectedVendingMachines.StatusService = "В работе";


                statusHistories.NewStatus = selectedVendingMachines.StatusService;
                AppData.db.StatusHistories.Add(statusHistories);

                AppData.db.SaveChanges();

                LoadData();
            }
        }

        private void DoneButtonClick(object sender, RoutedEventArgs e)
        {
            if (VendingMachinesDataGrid.SelectedItem is VendingMachines selectedVendingMachines)
            {
                StatusHistories statusHistories = new StatusHistories();

                statusHistories.VendingMachineId = selectedVendingMachines.VendingMachineId;
                statusHistories.OldStatus = selectedVendingMachines.StatusService;
                statusHistories.Date = DateTime.Now;


                selectedVendingMachines.StatusMachine = "Работает";
                selectedVendingMachines.StatusService = "Завершен";


                statusHistories.NewStatus = selectedVendingMachines.StatusService;
                AppData.db.StatusHistories.Add(statusHistories);

                AppData.db.SaveChanges();

                LoadData();
            }
        }

        private void DeclineButtonClick(object sender, RoutedEventArgs e)
        {
            if (VendingMachinesDataGrid.SelectedItem is VendingMachines selectedVendingMachines)
            {
                StatusHistories statusHistories = new StatusHistories();

                statusHistories.VendingMachineId = selectedVendingMachines.VendingMachineId;
                statusHistories.OldStatus = selectedVendingMachines.StatusService;
                statusHistories.Date = DateTime.Now;


                selectedVendingMachines.StatusMachine = "Работает";
                selectedVendingMachines.StatusService = "Отклонен";


                statusHistories.NewStatus = selectedVendingMachines.StatusService;
                AppData.db.StatusHistories.Add(statusHistories);

                AppData.db.SaveChanges();

                LoadData();
            }
        }

        private void WarningButtonClick(object sender, RoutedEventArgs e)
        {
            if (VendingMachinesDataGrid.SelectedItem is VendingMachines selectedVendingMachines)
            {
                StatusHistories statusHistories = new StatusHistories();

                statusHistories.VendingMachineId = selectedVendingMachines.VendingMachineId;
                statusHistories.OldStatus = selectedVendingMachines.StatusService;
                statusHistories.Date = DateTime.Now;


                selectedVendingMachines.StatusMachine = "Требуется ТО";
                selectedVendingMachines.StatusService = "Аварийная";
                selectedVendingMachines.Priority = "Высокий";


                statusHistories.NewStatus = selectedVendingMachines.StatusService;
                AppData.db.StatusHistories.Add(statusHistories);

                AppData.db.SaveChanges();

                LoadData();
            }
        }

        private void VadimButtonClick(object sender, RoutedEventArgs e)
        {
            if (VendingMachinesDataGrid.SelectedItem is VendingMachines selectedVendingMachines)
            {
                selectedVendingMachines.NameUser = "Вадимов В. В.";

                AppData.db.SaveChanges();

                LoadData();
            }
        }

        private void PetrButtonClick(object sender, RoutedEventArgs e)
        {
            if (VendingMachinesDataGrid.SelectedItem is VendingMachines selectedVendingMachines)
            {
                selectedVendingMachines.NameUser = "Петров П. П.";

                AppData.db.SaveChanges();

                LoadData();
            }
        }

        private void IvanButtonClick(object sender, RoutedEventArgs e)
        {
            if (VendingMachinesDataGrid.SelectedItem is VendingMachines selectedVendingMachines)
            {
                selectedVendingMachines.NameUser = "Иванов И. И.";

                AppData.db.SaveChanges();

                LoadData();
            }
        }
    }
}
