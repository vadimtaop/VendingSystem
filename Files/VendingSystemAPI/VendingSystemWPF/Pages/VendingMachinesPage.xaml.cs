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
using VendingSystemWPF.ADO;
using VendingSystemWPF.Services;
using VendingSystemWPF.Windows;

namespace VendingSystemWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для VendingMachinesPage.xaml
    /// </summary>
    public partial class VendingMachinesPage : Page
    {
        private List<VendingMachineRemote> _allMachines = new List<VendingMachineRemote>();

        // Добавь эти поля в начало класса VendingMachinesPage
        private int _currentPage = 1;
        private int _itemsPerPage = 10; // Можешь сделать TextBox для этого, как раньше
        private int _totalPages = 1;

        public VendingMachinesPage()
        {
            InitializeComponent();
            LoadDataFromApi(); // Вызываем один раз при создании
        }

        // МЕТОД 1: ЗАГРУЗКА ДАННЫХ С API
        private void LoadDataFromApi()
        {
            CountTextBlock.Text = "Загрузка...";
            _allMachines = ApiService.GetList(); // Используем новый синхронный метод
            UpdateDataGrid(); // Обновляем отображение
        }

        // МЕТОД 2: ОБНОВЛЕНИЕ ТАБЛИЦЫ (С ФИЛЬТРОМ)
        // ПОЛНОСТЬЮ ЗАМЕНИ свой метод UpdateDataGrid() на этот, с логикой Skip/Take
        private void UpdateDataGrid()
        {
            var viewSource = _allMachines;

            if (!string.IsNullOrWhiteSpace(FilterTextBox.Text))
            {
                viewSource = viewSource.Where(m => m.Name.ToLower().Contains(FilterTextBox.Text.ToLower())).ToList();
            }

            // --- ЛОГИКА ПАГИНАЦИИ ---
            int totalItems = viewSource.Count;
            _totalPages = (int)Math.Ceiling((double)totalItems / _itemsPerPage);
            if (_totalPages == 0) _totalPages = 1;
            if (_currentPage > _totalPages) _currentPage = _totalPages;

            // Отбираем данные для ТЕКУЩЕЙ страницы
            var pagedData = viewSource.Skip((_currentPage - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();

            // Внутри метода UpdateDataGrid() найди строку
            VendingMachinesDataGrid.ItemsSource = pagedData;

            // И сразу после нее добавь эту:
            VendingMachinesListView.ItemsSource = pagedData;

            // Обновляем тексты
            CountTextBlock.Text = $"Показано: {pagedData.Count} из {totalItems}";
            TxtPages.Text = $"Страница {_currentPage} из {_totalPages}";

            // Включаем/выключаем кнопки
            PrevButton.IsEnabled = _currentPage > 1;
            NextButton.IsEnabled = _currentPage < _totalPages;
        }


        // Добавь эти два обработчика для кнопок в конец файла
        private void BtnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                UpdateDataGrid();
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                UpdateDataGrid();
            }
        }


        // И НЕ ЗАБУДЬ сбрасывать страницу при фильтрации
        private void FilterTextChanged(object sender, TextChangedEventArgs e)
        {
            _currentPage = 1; // Сброс на первую страницу
            UpdateDataGrid();
        }

        // --- Обработчики кнопок ---

        // КНОПКА "ОБНОВИТЬ" - просто перезагружает данные с API
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadDataFromApi();
        }

        // КНОПКА "УДАЛИТЬ" - РАБОТАЕТ ЧЕРЕЗ ADO
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var machineToDeleteApi = button.DataContext as VendingMachineRemote;
            if (machineToDeleteApi == null) return;

            if (MessageBox.Show($"Удалить '{machineToDeleteApi.Name}'? (Это действие выполнится через ADO)", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // 1. Находим этот же объект в БД через ADO
                var machineInDb = AppData.db.VendingMachines.Find(machineToDeleteApi.Id);

                if (machineInDb != null)
                {
                    // 2. Удаляем из БД и сохраняем
                    AppData.db.VendingMachines.Remove(machineInDb);
                    AppData.db.SaveChanges();

                    MessageBox.Show("Удалено! Теперь нажмите 'Обновить', чтобы увидеть изменения.");
                }
                else
                {
                    MessageBox.Show("Ошибка: автомат не найден в локальной базе данных!");
                }
            }
        }

        // КНОПКА "ДОБАВИТЬ"
        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            new Windows.AddVendingMachineWindow().ShowDialog();
            MessageBox.Show("Добавлено! Теперь нажмите 'Обновить', чтобы увидеть изменения.");
        }

        // Переключение вида (пока заглушка)
        private void ToggleView_Checked(object sender, RoutedEventArgs e)
        {
            // Если галочка стоит - показываем плитки, иначе - таблицу
            bool showListView = ToggleViewCheck.IsChecked == true;
            VendingMachinesListView.Visibility = showListView ? Visibility.Visible : Visibility.Collapsed;
            VendingMachinesDataGrid.Visibility = showListView ? Visibility.Collapsed : Visibility.Visible;
        }
        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1. Берем для экспорта ВСЕ отфильтрованные данные, а не только одну страницу.
                var dataToExport = _allMachines;
                if (!string.IsNullOrWhiteSpace(FilterTextBox.Text))
                {
                    string filterText = FilterTextBox.Text.ToLower();
                    dataToExport = dataToExport.Where(m => m.Name.ToLower().Contains(filterText)).ToList();
                }

                if (!dataToExport.Any())
                {
                    MessageBox.Show("Нет данных для экспорта.");
                    return;
                }

                // 2. Создаем строку с помощью StringBuilder - это эффективно
                var sb = new StringBuilder();

                // Добавляем заголовки (важно для CSV)
                sb.AppendLine("ID;Название;Компания;Адрес;Статус;Дата ввода");

                // 3. Проходим по каждой записи и добавляем ее в файл
                foreach (var item in dataToExport)
                {
                    // Собираем строку, разделяя поля точкой с запятой
                    string line = string.Join(";",
                        item.Id,
                        item.Name,
                        item.Company?.Title, // Безопасный вызов, если компания null
                        item.Address,
                        item.Status?.Title,  // Безопасный вызов, если статус null
                        item.StartDate.ToString("dd.MM.yyyy")
                    );
                    sb.AppendLine(line);
                }

                // 4. Предлагаем пользователю сохранить файл
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV файл (*.csv)|*.csv", // Фильтр, чтобы предлагать только .csv
                    FileName = $"Vending_Machines_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    // Сохраняем все, что собрали, в файл
                    // Encoding.UTF8 - важно, чтобы русские буквы не превратились в "кракозябры"
                    File.WriteAllText(saveFileDialog.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show($"Экспорт успешно выполнен!\nФайл сохранен по пути: {saveFileDialog.FileName}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
