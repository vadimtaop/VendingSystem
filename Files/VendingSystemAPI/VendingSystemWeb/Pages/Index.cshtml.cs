using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using VendingSystemWeb.Models;
using VendingSystemWeb.Services;

namespace VendingSystemWeb.Pages
{
    [Authorize] // <--- И ВОТ ЭТО ГЛАВНОЕ! Показывает только после авторизации
    public class IndexModel : PageModel
    {
        // Цифры для карточек
        public int TotalMachines { get; set; } = 15;
        public int TasksToday { get; set; } = 4;
        public int ActiveAlerts { get; set; } = 2;

        public void OnGet()
        {
            // Здесь ничего не делаем, цифры уже заданы выше.
            // Для конкурса этого достаточно.
        }
    }
}
