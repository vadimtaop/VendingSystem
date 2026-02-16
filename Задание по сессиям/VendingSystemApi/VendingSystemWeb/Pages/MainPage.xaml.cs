using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            LoadData();
        }

        private void LoadData()
        {
            VendingMachinesDataGrid.ItemsSource = AppData.db.VendingMachines.ToList();
        }

        private void ImportButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                string[] lines = File.ReadAllLines(dialog.FileName);

                foreach (string line in lines)
                {
                    string[] data = line.Split(';');

                    VendingMachines vendingMachines = new VendingMachines();

                    vendingMachines.Name = data[0];
                    vendingMachines.Model = data[1];
                    vendingMachines.Company = data[2];
                    vendingMachines.Modem = data[3];
                    vendingMachines.Location = data[4];

                    // Возможны проблемы с датой (можно засунуть в try catch и ставить сегоднешнее число DateTime.Now)
                    vendingMachines.InstallDate = DateTime.Parse(data[5]);

                    AppData.db.VendingMachines.Add(vendingMachines);
                    AppData.db.SaveChanges();

                    LoadData();
                }
            }
        }
    }
}
