using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VendingSystemWeb.Pages
{
    [Authorize] // <--- И ВОТ ЭТО ГЛАВНОЕ! Показывает только после авторизации
    public class CalendarModel : PageModel
    {
        // Хранилище: Дата -> Цвет ("red", "green", "orange")
        public Dictionary<DateTime, string> EventColors { get; set; } = new Dictionary<DateTime, string>();

        // Класс для данных с API (копия того, что отдает твой API)
        public class ApiMachine
        {
            public DateTime? LastCheckDate { get; set; }
            public int? InterCheckMonth { get; set; }
        }

        public async Task OnGet()
        {
            // 1. Получаем данные с твоего API
            try
            {
                using (var client = new HttpClient())
                {
                    // !!!!!!! ПРОВЕРЬ ПОРТ !!!!!!!
                    string url = "http://localhost:5022/api/VendingMachines";
                    var json = await client.GetStringAsync(url);

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReferenceHandler = ReferenceHandler.IgnoreCycles
                    };
                    var machines = JsonSerializer.Deserialize<List<ApiMachine>>(json, options);

                    // 2. Рассчитываем цвета (Логика Б.6)
                    var today = DateTime.Today;
                    foreach (var m in machines)
                    {
                        // Дата следующего ТО = Последняя проверка + Интервал
                        var nextDate = m.LastCheckDate?.AddMonths(m.InterCheckMonth ?? 0);

                        if (nextDate.HasValue)
                        {
                            string color = "green"; // По умолчанию - норма
                            if (nextDate < today) color = "red"; // Просрочено
                            else if ((nextDate.Value - today).TotalDays <= 5) color = "orange"; // Скоро

                            // Сохраняем в словарь
                            EventColors[nextDate.Value.Date] = color;
                        }
                    }
                }
            }
            catch
            {
                // Если API не работает, просто будет пустой календарь. Не падаем.
            }
        }
    }
}
