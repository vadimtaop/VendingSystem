using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VendingSystemAPI.Models;

namespace VendingSystemWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly VendingSystemDbContext _context;
        public List<VendingMachine> VendingMachines { get; set; }

        public IndexModel(VendingSystemDbContext context)
        {
            _context = context;
        }

        // Этот метод просто загружает твою таблицу. И всё.
        public void OnGet()
        {
            VendingMachines = _context.VendingMachines.ToList();
        }
    }
}
