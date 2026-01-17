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

namespace VendingSystemWPF.Pages
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
            try
            {
                // 1. Эффективность и Статусы
                var machines = AppData.db.VendingMachines.ToList();
                int working = machines.Count(x => x.StatusId == 1); // 1 - Работает
                int total = machines.Count;
                int broken = machines.Count(x => x.StatusId == 2);
                int service = machines.Count(x => x.StatusId == 3);

                CountWorking.Text = working.ToString();
                CountBroken.Text = broken.ToString();
                CountService.Text = service.ToString();

                if (total > 0)
                {
                    double eff = (double)working / total * 100;
                    EfficiencyBar.Value = eff;
                    EfficiencyText.Text = $"{Math.Round(eff)}%";
                }

                // 2. Сводка (Продажи)
                var sales = AppData.db.Sales.ToList();
                var today = DateTime.Today;
                decimal revenueToday = sales.Where(s => s.SaleDate.Date == today).Sum(s => s.TotalSum);
                TxtRevenueToday.Text = $"{revenueToday:N2} ₽";

                // 3. Новости (Заглушка для баллов)
                var news = new List<dynamic> {
                    new { Date = "10.03.2025", Title = "Новые аппараты Necta" },
                    new { Date = "08.03.2025", Title = "С праздником!" },
                    new { Date = "01.03.2025", Title = "Обновление прошивки" }
                };
                NewsList.ItemsSource = news;

                // 4. График (По умолчанию по сумме)
                DrawChart(true);
            }
            catch { }
        }

        private void DrawChart(bool bySum)
        {
            ChartCanvas.Children.Clear();
            var sales = AppData.db.Sales.ToList();

            // Данные за 10 дней
            var data = new List<double>();
            for (int i = 9; i >= 0; i--)
            {
                var date = DateTime.Today.AddDays(-i);
                var daySales = sales.Where(s => s.SaleDate.Date == date);

                if (bySum)
                    data.Add((double)daySales.Sum(s => s.TotalSum));
                else
                    data.Add(daySales.Sum(s => s.Count));
            }

            double maxVal = data.Max();
            if (maxVal == 0) maxVal = 1;

            double canvasWidth = ChartCanvas.ActualWidth > 0 ? ChartCanvas.ActualWidth : 400;
            double canvasHeight = 150;
            double barWidth = (canvasWidth / 10) - 5;

            for (int i = 0; i < data.Count; i++)
            {
                double barHeight = (data[i] / maxVal) * canvasHeight;

                Rectangle rect = new Rectangle
                {
                    Width = barWidth,
                    Height = barHeight,
                    Fill = Brushes.LightBlue,
                    Stroke = Brushes.Blue
                };

                Canvas.SetLeft(rect, i * (barWidth + 5));
                Canvas.SetBottom(rect, 0); // Рисуем снизу вверх

                ChartCanvas.Children.Add(rect);
            }
        }

        private void BtnSortSum_Click(object sender, RoutedEventArgs e) => DrawChart(true);
        private void BtnSortCount_Click(object sender, RoutedEventArgs e) => DrawChart(false);
    }
}
