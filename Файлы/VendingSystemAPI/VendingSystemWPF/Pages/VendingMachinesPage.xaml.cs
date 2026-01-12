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
using VendingSystemWPF.ADO;
using VendingSystemWPF.Windows;

namespace VendingSystemWPF.Pages
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
            var data = AppData.db.VendingMachines.ToList();

            VendingMachinesDataGrid.ItemsSource = data;

            CountTextBlock.Text = $"Всего найдено {data.Count()} шт.";
        }

        private void FilterTextChanged(object sender, TextChangedEventArgs e)
        {
            FilterAndLimit();
        }

        private void LimitTextChanged(object sender, TextChangedEventArgs e)
        {
            FilterAndLimit();
        }

        private void FilterAndLimit()
        {
            try
            {
                var data = AppData.db.VendingMachines.ToList();

                string textFilter = FilterTextBox.Text.ToLower();

                if (!string.IsNullOrEmpty(textFilter))
                {
                    data = data.Where(p => p.Name.ToLower().Contains(textFilter)).ToList();
                }

                if (int.TryParse(LimitTextBox.Text, out int limit) && limit > 0)
                {
                    data = data.Take(limit).ToList();
                }

                int countData = data.Count;

                VendingMachinesDataGrid.ItemsSource = data;
                CountTextBlock.Text = $"Всего найдено {countData} шт.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            AddVendingMachineWindow addVendingMachineWindow = new AddVendingMachineWindow();
            addVendingMachineWindow.ShowDialog();

            LoadData();
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить данные?",
                    "ООО Торговые Автоматы",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    if (VendingMachinesDataGrid.SelectedItem is VendingMachines vendingMachines)
                    {
                        AppData.db.VendingMachines.Remove(vendingMachines);
                        AppData.db.SaveChanges();

                        LoadData();

                        MessageBox.Show("Данные успешно удалены!",
                            "ООО Торговые Автоматы",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Данные не выбраны!",
                            "ООО Торговые Автоматы",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}",
                    "ООО Торговые Автоматы",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
