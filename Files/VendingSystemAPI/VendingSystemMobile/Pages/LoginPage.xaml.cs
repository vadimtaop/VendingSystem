using VendingSystemMobile.Models;
using VendingSystemMobile.Services;

namespace VendingSystemMobile.Pages;

public partial class LoginPage : ContentPage
{
    ApiService _api = new ApiService();

    public LoginPage()
    {
        InitializeComponent();
        CheckAutoLogin();
    }

    private async void CheckAutoLogin()
    {
        // Критерий: Автоматический вход
        if (Preferences.ContainsKey("UserId"))
        {
            Application.Current.MainPage = new AppShell();
        }
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var login = LoginEntry.Text;
        var pass = PasswordEntry.Text;

        var user = await _api.Login(login, pass);

        if (user != null)
        {
            // 1. Сохраняем данные пользователя
            Preferences.Set("UserId", user.Id);
            Preferences.Set("UserRole", user.Role);

            // --- НАЧАЛО НОВОГО КОДА ---

            // 2. Запрашиваем "сырые" задачи у API
            List<ServiceTaskDto> apiTasks = await _api.GetTasksAsync();

            if (apiTasks.Any()) // Any() - проверяет, есть ли в списке хоть что-то
            {
                // 3. Превращаем "сырые" задачи в "локальные" для нашей базы данных
                var localTasks = apiTasks.Select(apiTask => new LocalServiceTask
                {
                    Id = apiTask.Id,
                    Description = apiTask.Description,
                    Address = apiTask.Address,
                    Status = "Новая", // Задаем статус по умолчанию
                    Date = DateTime.Now // Задаем дату по умолчанию
                }).ToList();

                // 4. Сохраняем весь список в локальную базу данных
                await App.Database.SaveTasksAsync(localTasks);
            }

            // --- КОНЕЦ НОВОГО КОДА ---

            // 5. И только теперь, когда данные сохранены, переходим на главный экран
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await DisplayAlert("Ошибка", "Неверный логин или пароль", "OK");
        }
    }
}