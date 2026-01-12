using System.Net.Http.Json;
using VendingSystemMobile.Services;

namespace VendingSystemMobile;

public partial class AuthPage : ContentPage
{
	public AuthPage()
	{
		InitializeComponent();
	}
    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Формируем URL
            string url = ApiService.BaseUrl + "/api/auth/login";

            // Отправляем запрос (самый короткий способ!)
            var response = await ApiService.Client.PostAsJsonAsync(url, new
            {
                Login = LoginEntry.Text,
                Password = PassEntry.Text
            });

            if (response.IsSuccessStatusCode)
            {
                // Получаем ответ
                var result = await response.Content.ReadFromJsonAsync<dynamic>();

                // Читаем ID пользователя из ответа API
                string userId = result.GetProperty("id").GetInt32().ToString();

                // Сохраняем ID, чтобы помнить, что пользователь вошел
                Preferences.Set("auth_token", userId); // Ключ можно оставить тот же

                // Сохраняем ID в наш статический класс для использования в других окнах
                ApiService.UserId = int.Parse(userId);

                // Переходим на главную
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Application.Current.MainPage = new AppShell();
                });
            }
            else
            {
                await DisplayAlert("Ошибка", "Неверный логин", "ОК");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ошибка сети", ex.Message, "ОК");
        }
    }
}