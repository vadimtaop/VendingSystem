using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using VendingSystemWeb.Models;
using VendingSystemWeb.Services;

namespace VendingSystemWeb.Pages
{
    [Authorize] // <-- Эта команда делает страницу доступной ТОЛЬКО для вошедших пользователей
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;
        public List<ServiceTaskDto> Tasks { get; set; } = new List<ServiceTaskDto>();

        // --- НАЧАЛО НОВЫХ СВОЙСТВ ДЛЯ КАРТОЧЕК ---
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int InProgressTasks { get; set; }
        // --- КОНЕЦ НОВЫХ СВОЙСТВ ---

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task OnGetAsync()
        {
            Tasks = await _apiService.GetTasksAsync();

            // --- НАЧАЛО НОВОЙ ЛОГИКИ РАСЧЕТА ---
            if (Tasks.Any())
            {
                TotalTasks = Tasks.Count;
                // Так как у нас пока нет статуса с API, поставим заглушки
                // Когда в API появится статус, мы заменим это реальным подсчетом
                CompletedTasks = 0;
                InProgressTasks = Tasks.Count(t => t.Id > 0); // Просто для примера
            }
            // --- КОНЕЦ НОВОЙ ЛОГИКИ РАСЧЕТА ---
        }

        public async Task<IActionResult> OnGetLogoutAsync()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToPage("/Login");
        }
    }
}
