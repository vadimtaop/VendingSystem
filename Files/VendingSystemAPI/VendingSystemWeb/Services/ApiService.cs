using Newtonsoft.Json;
using System.Text;

namespace VendingSystemWeb.Services
{
    public class ApiService
    {
        private HttpClient client;

        public ApiService(HttpClient httpClient)
        {
            client = httpClient;
        }

        public async Task<List<dynamic>> GetMachines()
        {
            var response = await client.GetAsync("https://localhost:7077/api/vendingmachines");
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<dynamic>>(json) ?? new();
        }
    }
}
