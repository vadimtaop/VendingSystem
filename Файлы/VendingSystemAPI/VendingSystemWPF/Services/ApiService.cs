using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VendingSystemWPF.Services
{
    public class ApiService // internal поменять на public
    {
        private HttpClient http = new HttpClient();

        public async Task<dynamic> GetMachines()
        {
            var resp = await http.GetAsync("https://localhost:7077/api/vendingmachines");
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject(json);
        }

        public async Task<dynamic> Login(string login, string password)
        {
            var body = JsonConvert.SerializeObject(new { Login = login, Password = password });

            var content = new StringContent(body, Encoding.UTF8, "application/json");

            var resp = await http.PostAsync("https://localhost:7077/api/auth/login", content);

            var json = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
            {
                return null;
            }

            return JsonConvert.DeserializeObject(json);
        }
    }
}
