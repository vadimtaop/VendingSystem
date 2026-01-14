using Newtonsoft.Json;
using System.Text;
using VendingSystemWeb.Models;

namespace VendingSystemWeb.Services
{
    public class ApiService
    {
        // 1. УБИРАЕМ ПРОВЕРКУ ПЛАТФОРМЫ.
        // ApiService в WEB-проекте всегда будет использовать один и тот же адрес.
        private readonly string _baseUrl = "http://localhost:5022/api"; // <-- Убедись, что порт 5022 верный!

        private readonly HttpClient _client;

        public ApiService()
        {
            _client = new HttpClient();
        }

        public async Task<LoginResponseDto> Login(string login, string password)
        {
            var json = JsonConvert.SerializeObject(new { Login = login, Password = password });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync($"{_baseUrl}/Auth/login", content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<LoginResponseDto>(result);
                }
            }
            catch (Exception ex)
            {
                // 2. УБИРАЕМ ВЫЗОВ МОБИЛЬНОГО ОКНА.
                // Просто логируем ошибку в консоль для отладки, не показывая ее пользователю.
                Console.WriteLine($"API Login Error: {ex.Message}");
            }

            return null; // Возвращаем null в случае любой ошибки
        }

        public async Task<List<ServiceTaskDto>> GetTasksAsync()
        {
            try
            {
                var response = await _client.GetAsync($"{_baseUrl}/Services");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ServiceTaskDto>>(json);
                }
            }
            catch (Exception ex)
            {
                // 2. УБИРАЕМ ВЫЗОВ МОБИЛЬНОГО ОКНА.
                Console.WriteLine($"API GetTasks Error: {ex.Message}");
            }

            return new List<ServiceTaskDto>(); // Возвращаем пустой список в случае любой ошибки
        }
    }
}
