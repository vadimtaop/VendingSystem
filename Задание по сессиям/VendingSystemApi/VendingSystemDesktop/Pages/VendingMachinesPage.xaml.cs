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
            VendingMachinesListView.ItemsSource = AppData.db.VendingMachines.ToList();


            CountTextBlock.Text = $"Кол-во ТА: {VendingMachinesDataGrid.Items.Count}";
            CountFilterTextBlock.Text = $"Кол-во общее: {VendingMachinesDataGrid.Items.Count}";
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            AddVendingMachinesWindow addVendingMachinesWindow = new AddVendingMachinesWindow();
            addVendingMachinesWindow.ShowDialog();

            LoadData();
        }

        // Экспорт не рабочий, открывает просто диалог
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

            CountFilterTextBlock.Text = $"Кол-во общее: {VendingMachinesDataGrid.Items.Count}";
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
                selectedVendingMachine.Modem = "-1";

                AppData.db.SaveChanges();

                MessageBox.Show("Модем успешно отвязан (добавить ПОДТВЕРЖДЕНИЕ от пользователя и try catch)");
            }
        }

        private async void CriticalMessageButtonClick(object sender, RoutedEventArgs e)
        {
            if (MessagePopup.IsOpen == false)
            {
                MessageTextBlock.Text = "❌\nОшибка!\nНет сдачи";
                MessageStackPanel.Background = Brushes.LightCoral;

                MessagePopup.IsOpen = true;

                MessageHistories messageHistories = new MessageHistories();

                messageHistories.Type = "Критический";
                messageHistories.Note = "Нет сдачи";
                messageHistories.Date = DateTime.Now;

                AppData.db.MessageHistories.Add(messageHistories);
                AppData.db.SaveChanges();

                await Task.Delay(10000);

                MessagePopup.IsOpen = false;
            }
            else
            {
                MessagePopup.IsOpen = false;
            }
        }

        private async void WarningMessageButtonClick(object sender, RoutedEventArgs e)
        {
            if (MessagePopup.IsOpen == false)
            {
                MessageTextBlock.Text = "⚠️\nПредупреждение!\nЗаканчивается товар (осталось 2 шт)";
                MessageStackPanel.Background = Brushes.LightYellow;

                MessagePopup.IsOpen = true;

                MessageHistories messageHistories = new MessageHistories();

                messageHistories.Type = "Предупреждение";
                messageHistories.Note = "Заканчивается товар (осталось 2 шт)";
                messageHistories.Date = DateTime.Now;

                AppData.db.MessageHistories.Add(messageHistories);
                AppData.db.SaveChanges();

                await Task.Delay(7000);

                MessagePopup.IsOpen = false;
            }
            else
            {
                MessagePopup.IsOpen = false;
            }
        }

        private async void InfoMessageButtonClick(object sender, RoutedEventArgs e)
        {
            if (MessagePopup.IsOpen == false)
            {
                MessageTextBlock.Text = "✓\nИнформация!\nТовар успешно добавлен";
                MessageStackPanel.Background = Brushes.AliceBlue;

                MessagePopup.IsOpen = true;

                MessageHistories messageHistories = new MessageHistories();

                messageHistories.Type = "Информационный";
                messageHistories.Note = "Товар успешно добавлен";
                messageHistories.Date = DateTime.Now;

                AppData.db.MessageHistories.Add(messageHistories);
                AppData.db.SaveChanges();

                await Task.Delay(5000);

                MessagePopup.IsOpen = false;
            }
            else
            {
                MessagePopup.IsOpen = false;
            }
        }

        private void CloseMessageButtonClick(object sender, RoutedEventArgs e)
        {
            MessagePopup.IsOpen = false;
        }

        private void ListTypeButtonClick(object sender, RoutedEventArgs e)
        {
            if (VendingMachinesDataGrid.Visibility == Visibility.Visible)
            {
                VendingMachinesDataGrid.Visibility = Visibility.Collapsed;
                VendingMachinesListView.Visibility = Visibility.Visible;
            }
            else
            {
                VendingMachinesDataGrid.Visibility = Visibility.Visible;
                VendingMachinesListView.Visibility = Visibility.Collapsed;
            } 
        }
    }
}
