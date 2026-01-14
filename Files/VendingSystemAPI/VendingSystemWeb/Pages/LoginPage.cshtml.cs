using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using VendingSystemWeb.Services;

namespace VendingSystemWeb.Pages
{
    public class LoginPageModel : PageModel
    {
        private readonly ApiService _apiService;

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }

        public LoginModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _apiService.Login(Input.Login, Input.Password);
            if (user != null)
            {
                // Создаем "паспорт" пользователя
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Input.Login),
                    new Claim(ClaimTypes.Role, user.Role),
                };
                var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
                var authProperties = new AuthenticationProperties();

                // "Впускаем" пользователя на сайт
                await HttpContext.SignInAsync("MyCookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToPage("/Index"); // Перенаправляем на главную
            }

            return Page();
        }
    }
}
