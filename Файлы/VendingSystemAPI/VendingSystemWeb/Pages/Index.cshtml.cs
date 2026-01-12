using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using VendingSystemWeb.Services;

namespace VendingSystemWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApiService _apiService;
        public List<dynamic> Machines { get; set; } = new();

        public IndexModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task OnGetAsync()
        {
            Machines = await _apiService.GetMachines();
        }
    }
}
