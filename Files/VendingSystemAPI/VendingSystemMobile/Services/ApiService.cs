using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json; // Обязательно подключи это!

namespace VendingSystemMobile.Services
{
    public static class ApiService
    {
        // 10.0.2.2 - это "localhost" для Android эмулятора
        // Если запускаешь на Windows машине (не эмулятор), то localhost
        public static string BaseUrl = DeviceInfo.Platform == DevicePlatform.Android
                                       ? "http://10.0.2.2:5022" // Вставь сюда СВОЙ PORT API (http, не https для простоты)
                                       : "http://localhost:5022";

        public static HttpClient Client = new HttpClient();

        public static int UserId = 0; // Сюда запомним ID после входа
    }
}
