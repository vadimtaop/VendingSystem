using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text.Json;
using VendingSystemWeb.Services;

namespace VendingSystemWeb.Pages
{
    // Класс пользователя (такой же, как в твоей БД/API)
    public class UserFromApi
    {
        public int Id { get; set; }
        public string Email { get; set; }        // Логин (или Email)
        public string PasswordHash { get; set; } // Пароль
        public string Role { get; set; }         // Роль (например, "Manager")
    }

    public class LoginModel : PageModel
    {
        public string ErrorMessage { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync(string username, string password)
        {
            try
            {
                // 1. Стучимся в твой API за списком пользователей
                using (var client = new HttpClient())
                {
                    // !!!!!!! ПРОВЕРЬ ПОРТ !!!!!!!
                    string url = "http://localhost:5022/api/Users";

                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var users = JsonSerializer.Deserialize<List<UserFromApi>>(json, options);

                        // 2. Ищем пользователя с таким логином и паролем
                        // (Считаем, что в поле Email у тебя логин)
                        var user = users.FirstOrDefault(u => u.Email == username && u.PasswordHash == password);

                        if (user != null)
                        {
                            // УРА! НАШЛИ В БД!

                            // 3. Создаем "паспорт" (Cookie)
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, user.Email),
                                new Claim(ClaimTypes.Role, user.Role ?? "Employee")
                            };
                            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                            var principal = new ClaimsPrincipal(identity);

                            await HttpContext.SignInAsync("MyCookieAuth", principal);

                            return RedirectToPage("/Index");
                        }
                    }
                }
            }
            catch
            {
                ErrorMessage = "Ошибка связи с API. Проверьте, запущен ли API-проект.";
                return Page();
            }

            // Если дошли сюда - значит не нашли пользователя
            ErrorMessage = "Неверный логин или пароль!";
            return Page();
        }

        public async Task<IActionResult> OnGetLogoutAsync()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToPage("/Login");
        }
    }
}
