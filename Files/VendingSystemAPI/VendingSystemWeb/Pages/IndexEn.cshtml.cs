using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VendingSystemWeb.Services;

namespace VendingSystemWeb.Pages.En
{
    public class IndexEnModel : PageModel
    {
        private readonly ApiService _apiService;
        public List<dynamic> Machines { get; set; } = new();

        public IndexEnModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task OnGetAsync()
        {
            Machines = await _apiService.GetMachines();
        }
    }
}
