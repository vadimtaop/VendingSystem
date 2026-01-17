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

namespace VendingSystemWPF.Pages
{
    /// <summary>
    /// УПРОЩЕННЫЙ класс для данных мониторинга. Только текст и цифры.
    /// </summary>
    public class SimpleMonitorData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StatusText { get; set; }  // "Работает", "Не работает", "Обслуживание"
        public string EventText { get; set; }   // "Ошибок нет", "Низкий запас", "Замятие"
        public int LoadPercentage { get; set; }
        public decimal CashAmount { get; set; }
    }


    public partial class MonitorPage : Page
    {
        // Список для хранения ВСЕХ данных, полученных с "API"
        private List<SimpleMonitorData> _allData = new List<SimpleMonitorData>();

        public MonitorPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // 1. Загружаем данные (пока заглушки)
            LoadDummyData();
            // 2. Заполняем выпадающие списки для фильтров
            PopulateFilters();
            // 3. Отображаем данные
            UpdateDataView();
        }

        // --- Шаг 1: Загрузка данных (пока эмуляция) ---
        private void LoadDummyData()
        {
            // Просто создаем список объектов. Никаких картинок.
            _allData = new List<SimpleMonitorData>
            {
                new SimpleMonitorData { Id = 1, Name = "Автомат в холле", StatusText = "Работает", EventText = "Ошибок нет", LoadPercentage = 75, CashAmount = 12550 },
                new SimpleMonitorData { Id = 2, Name = "Автомат в столовой", StatusText = "Работает", EventText = "Низкий запас", LoadPercentage = 25, CashAmount = 3200 },
                new SimpleMonitorData { Id = 3, Name = "Автомат на 3 этаже", StatusText = "Не работает", EventText = "Замятие", LoadPercentage = 90, CashAmount = 15400 },
                new SimpleMonitorData { Id = 4, Name = "Автомат у входа", StatusText = "Обслуживание", EventText = "Ошибок нет", LoadPercentage = 100, CashAmount = 0 }
            };
        }

        // --- Шаг 2: Заполнение фильтров ---
        private void PopulateFilters()
        {
            // Добавляем варианты в выпадающие списки, плюс пункт "Все"
            StatusFilterComboBox.Items.Add("Все");
            EventFilterComboBox.Items.Add("Все");

            // Берем уникальные значения из наших данных
            foreach (var status in _allData.Select(d => d.StatusText).Distinct())
            {
                StatusFilterComboBox.Items.Add(status);
            }
            foreach (var evt in _allData.Select(d => d.EventText).Distinct())
            {
                EventFilterComboBox.Items.Add(evt);
            }

            // Выбираем "Все" по умолчанию
            StatusFilterComboBox.SelectedIndex = 0;
            EventFilterComboBox.SelectedIndex = 0;
        }

        // --- Шаг 3: Обновление отображения (с учетом фильтров) ---
        private void UpdateDataView()
        {
            var filteredData = _allData;

            // Фильтр по статусу
            if (StatusFilterComboBox.SelectedIndex > 0)
            {
                string selectedStatus = StatusFilterComboBox.SelectedItem.ToString();
                filteredData = filteredData.Where(d => d.StatusText == selectedStatus).ToList();
            }

            // Фильтр по событию
            if (EventFilterComboBox.SelectedIndex > 0)
            {
                string selectedEvent = EventFilterComboBox.SelectedItem.ToString();
                filteredData = filteredData.Where(d => d.EventText == selectedEvent).ToList();
            }

            // Отображаем отфильтрованные данные
            MonitorDataGrid.ItemsSource = filteredData;

            // Обновляем итоговые значения
            TotalMachinesTextBlock.Text = $"Итого автоматов: {filteredData.Count}";
            TotalCashTextBlock.Text = $"Денег в автоматах: {filteredData.Sum(d => d.CashAmount):C}";
        }

        // --- Обработчик изменения фильтров ---
        private void Filter_Changed(object sender, SelectionChangedEventArgs e)
        {
            // Если данные еще не загружены, ничего не делаем
            if (_allData.Any())
            {
                UpdateDataView();
            }
        }
    }
}
