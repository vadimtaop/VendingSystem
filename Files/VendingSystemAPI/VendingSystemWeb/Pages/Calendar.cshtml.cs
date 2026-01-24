using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VendingSystemAPI.Models;

namespace VendingSystemWeb.Pages
{
    public class CalendarEvent
    {
        public string Title { get; set; }
        public DateTime EventDate { get; set; }
        public string ColorCssClass { get; set; }
    }

    public class CalendarModel : PageModel
    {
        // Список событий, который мы покажем
        public List<CalendarEvent> Events { get; set; } = new List<CalendarEvent>();

        // ЭТОТ МЕТОД ТЕПЕРЬ СУПЕР-ПРОСТОЙ
        public void OnGet()
        {
            // --- ОДНО КРАСНОЕ ---
            Events.Add(new CalendarEvent
            {
                EventDate = DateTime.Now.AddDays(-10),
                Title = "Просрочено: Ремонт 'Автомат-1'",
                ColorCssClass = "event-red"
            });

            // --- ОДНО ЖЕЛТОЕ ---
            Events.Add(new CalendarEvent
            {
                EventDate = DateTime.Now.AddDays(5),
                Title = "Скоро: ТО 'Автомат-2'",
                ColorCssClass = "event-yellow"
            });

            // --- А ТЕПЕРЬ НЕСКОЛЬКО ЗЕЛЕНЫХ (ПРОСТО КОПИПАСТ) ---
            Events.Add(new CalendarEvent
            {
                EventDate = DateTime.Now.AddDays(30),
                Title = "Плановое ТО 'Автомат-3'",
                ColorCssClass = "event-green"
            });
        }
    }
}
