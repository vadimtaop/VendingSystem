using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VendingSystemWPF.Services
{
    public class VendingMachineRemote
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Model { get; set; }
        public System.DateTime StartDate { get; set; }
        public CompanyObj Company { get; set; }
        public StatusObj Status { get; set; }
        public class CompanyObj { public string Title { get; set; } }
        public class StatusObj { public string Title { get; set; } }
    }

    public static class ApiService
    {
        private static string Url = "http://localhost:5022/api/VendingMachines"; // ПРОВЕРЬ ПОРТ!
        private static HttpClient client = new HttpClient();

        // СИНХРОННЫЙ МЕТОД - ПРОСТО И НАДЕЖНО
        public static List<VendingMachineRemote> GetList()
        {
            try
            {
                // .Result - ждет, пока данные скачаются, и блокирует поток.
                // Для конкурса - идеально, т.к. убирает все ошибки с потоками.
                string json = client.GetStringAsync(Url).Result;
                return JsonConvert.DeserializeObject<List<VendingMachineRemote>>(json);
            }
            catch
            {
                MessageBox.Show("Ошибка API! Убедитесь, что API-проект запущен.", "Критическая ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<VendingMachineRemote>(); // Возвращаем пустой список
            }
        }
    }
}
