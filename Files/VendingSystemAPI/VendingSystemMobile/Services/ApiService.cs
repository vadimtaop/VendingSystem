using Newtonsoft.Json;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json; // Обязательно подключи это!
using System.Text;
using System.Threading.Tasks;
using VendingSystemMobile.Models;

namespace VendingSystemMobile.Services
{
    public class ApiService
    {
        // ВНИМАНИЕ: Для Android эмулятора localhost это 10.0.2.2
        // Если запускаешь Windows приложение - localhost
        private readonly string _baseUrl = DeviceInfo.Platform == DevicePlatform.Android
            ? "http://10.0.2.2:5022/api" // <-- Используй свой порт, если он другой
            : "http://localhost:5022/api";

        private readonly HttpClient _client;

        public ApiService()
        {
            _client = new HttpClient();
        }

        // Возвращаем конкретный класс, а не dynamic
        public async Task<LoginResponseDto> Login(string login, string password)
        {
            var json = JsonConvert.SerializeObject(new { Login = login, Password = password });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // Используем _client и _baseUrl с подчеркиванием
                var response = await _client.PostAsync($"{_baseUrl}/Auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<LoginResponseDto>(result);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ошибка API", ex.Message, "OK");
            }

            return null;
        }

        // Метод для получения списка задач с сервера
        public async Task<List<ServiceTaskDto>> GetTasksAsync()
        {
            try
            {
                // Используем _client и _baseUrl, которые у нас уже есть
                var response = await _client.GetAsync($"{_baseUrl}/Services");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    // Десериализуем в наш безопасный "контейнер"
                    return JsonConvert.DeserializeObject<List<ServiceTaskDto>>(json);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ошибка API", $"Не удалось загрузить задачи: {ex.Message}", "OK");
            }

            return new List<ServiceTaskDto>(); // Возвращаем пустой список в случае ошибки
        }
    }
}
