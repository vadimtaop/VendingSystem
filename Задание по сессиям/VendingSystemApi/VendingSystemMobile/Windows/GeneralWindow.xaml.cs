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
            VendingMachinesListView.ItemsSource = AppData.db.VendingMachines.ToList().OrderBy(v => v.StartServiceDate).ToList();
        }

        private void WhiteThemeButtonClick(object sender, RoutedEventArgs e)
        {
            TopGrid.Background = Brushes.White;
            BottomGrid.Background = Brushes.White;
            CenterGrid.Background = Brushes.GhostWhite;
            VendingMachinesListView.Background = Brushes.GhostWhite;
            KeepGrid.Background = Brushes.GhostWhite;
            CancelGrid.Background = Brushes.GhostWhite;
        }

        private void BlackThemeButtonClick(object sender, RoutedEventArgs e)
        {
            TopGrid.Background = Brushes.DimGray;
            BottomGrid.Background = Brushes.DimGray;
            CenterGrid.Background = Brushes.DarkGray;
            VendingMachinesListView.Background = Brushes.DarkGray;
            KeepGrid.Background = Brushes.DarkGray;
            CancelGrid.Background = Brushes.DarkGray;
        }

        private void ApplyButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button.DataContext is VendingMachines selectedVendingMachine)
            {
                StatusHistories statusHistories = new StatusHistories();

                statusHistories.VendingMachineId = selectedVendingMachine.VendingMachineId;
                statusHistories.OldStatus = selectedVendingMachine.StatusService;
                statusHistories.Date = DateTime.Now;

                selectedVendingMachine.StartServiceDate = DateTime.Now;
                selectedVendingMachine.DeadlineDays = 5;
                selectedVendingMachine.StatusService = "В работе";

                statusHistories.NewStatus = selectedVendingMachine.StatusService;
                AppData.db.StatusHistories.Add(statusHistories);

                // Протокол
                Protocols protocols = new Protocols();

                protocols.VendingMachineId = selectedVendingMachine.VendingMachineId;
                protocols.Date = DateTime.Now;
                protocols.Note = "Протокол сформирован";

                AppData.db.Protocols.Add(protocols);


                AppData.db.SaveChanges();

                LoadData();
            }
        }

        private void DeclineButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button.DataContext is VendingMachines selectedVendingMachine)
            {
                _VendingMachineId = selectedVendingMachine.VendingMachineId;

                CenterScrollViewer.Visibility = Visibility.Collapsed;
                CancelGrid.Visibility = Visibility.Visible;
            }
        }

        private void CameraButtonClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("microsoft.windows.camera:");
        }

        private void KeepButtonClick(object sender, RoutedEventArgs e)
        {
            if (VendingMachinesListView.SelectedItem is VendingMachines selectedVendingMachine) 
            {
                CenterScrollViewer.Visibility = Visibility.Collapsed;
                KeepGrid.Visibility = Visibility.Visible;

                TitleKeepTextBlock.Text = $"Заметка для заявки {selectedVendingMachine.VendingMachineId}";

                KeepTextBox.Text = selectedVendingMachine.Note;

                _VendingMachineId = selectedVendingMachine.VendingMachineId;
            }
        }

        private int _VendingMachineId = 0;

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            var vendingMachine = AppData.db.VendingMachines.FirstOrDefault(v => v.VendingMachineId == _VendingMachineId);

            if (vendingMachine != null)
            {
                vendingMachine.Note = KeepTextBox.Text;

                AppData.db.SaveChanges();

                LoadData();

                CenterScrollViewer.Visibility = Visibility.Visible;
                KeepGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            CenterScrollViewer.Visibility = Visibility.Visible;
            KeepGrid.Visibility = Visibility.Collapsed;
        }

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            var vendingMachine = AppData.db.VendingMachines.FirstOrDefault(v => v.VendingMachineId == _VendingMachineId);

            if (vendingMachine != null)
            {
                StatusHistories statusHistories = new StatusHistories();

                statusHistories.VendingMachineId = vendingMachine.VendingMachineId;
                statusHistories.OldStatus = vendingMachine.StatusService;
                statusHistories.Date = DateTime.Now;

                vendingMachine.StatusService = "Отменена";
                vendingMachine.CancelNote = CancelTextBox.Text;

                statusHistories.NewStatus = vendingMachine.StatusService;
                AppData.db.StatusHistories.Add(statusHistories);

                // Протокол
                Protocols protocols = new Protocols();

                protocols.VendingMachineId = vendingMachine.VendingMachineId;
                protocols.Date = DateTime.Now;
                protocols.Note = "Протокол сформирован";

                AppData.db.Protocols.Add(protocols);

                AppData.db.SaveChanges();

                LoadData();

                CenterScrollViewer.Visibility = Visibility.Visible;
                CancelGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            CenterScrollViewer.Visibility = Visibility.Visible;
            CancelGrid.Visibility = Visibility.Collapsed;
        }
    }
}
