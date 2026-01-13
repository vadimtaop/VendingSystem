using System.Net.Http.Json;
using VendingSystemMobile.Models;
using VendingSystemMobile.Services;

namespace VendingSystemMobile;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadTasks(); // Запускаем загрузку данных
    }

    async Task LoadTasks()
    {
        // Убедись, что твой API-контроллер для списка задач
        // называется ServicesController, иначе измени путь
        string url = ApiService.BaseUrl + "/api/services";

        try
        {
            // Теперь этот код будет работать, так как ServiceTask - отдельный, публичный класс
            var tasks = await ApiService.Client.GetFromJsonAsync<List<ServiceTask>>(url);

            TasksList.ItemsSource = tasks;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка загрузки", $"Не удалось загрузить задачи. Убедись, что API запущен. Ошибка: {ex.Message}", "OK");
        }
    }

    private async void TasksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Проверяем, что пользователь выбрал что-то
        if (e.CurrentSelection.FirstOrDefault() is ServiceTask selectedTask)
        {
            // Переходим на новую страницу и ПЕРЕДАЕМ ей выбранную задачу
            await Navigation.PushAsync(new DetailsPage(selectedTask));

            // Снимаем выделение, чтобы можно было нажать еще раз
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}