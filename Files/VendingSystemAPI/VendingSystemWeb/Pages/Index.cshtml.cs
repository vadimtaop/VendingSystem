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

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        // Этот метод выполняется при загрузке страницы
        public async Task OnGetAsync()
        {
            Tasks = await _apiService.GetTasksAsync();
        }
    }
}
