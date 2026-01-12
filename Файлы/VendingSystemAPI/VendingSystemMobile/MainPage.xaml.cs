using System.Net.Http.Json;
using VendingSystemMobile.Services;

namespace VendingSystemMobile
{
    public partial class MainPage : ContentPage
    {
        // Это простая модель для получения задач с API
        public class ServiceTask
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public string Address { get; set; }
        }


        public partial class MainPage : ContentPage
        {
            public MainPage()
            {
                InitializeComponent();
            }

            protected override async void OnAppearing()
            {
                base.OnAppearing();
                await LoadTasks();
            }

            async Task LoadTasks()
            {
                try
                {
                    string url = ApiService.BaseUrl + "/api/services";

                    // Теперь этот код будет работать, так как ServiceTask - публичный класс
                    var tasks = await ApiService.Client.GetFromJsonAsync<List<ServiceTask>>(url);

                    TasksList.ItemsSource = tasks;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ошибка", $"Не удалось загрузить задачи: {ex.Message}", "OK");
                }
            }
        }
    }
}
