using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VendingSystemWeb.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
        }

        // Этот метод сработает, когда мы нажмем на кнопку "Войти"
        public IActionResult OnPost()
        {
            // Просто перенаправляем на главную страницу, имитируя успешный вход.
            // Вместо "/Index" укажите главную страницу вашего WEB-приложения.
            return RedirectToPage("/Index");

            // ВСЁ! Больше никакого кода не нужно.
        }
    }
}
