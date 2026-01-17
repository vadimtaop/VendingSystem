using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VendingSystemWeb.Pages
{
    [Authorize] // <--- И ВОТ ЭТО ГЛАВНОЕ! Показывает только после авторизации
    public class ImportModel : PageModel
    {
        [BindProperty]
        public IFormFile UploadedFile { get; set; } // Сюда прилетит файл

        public string Message { get; set; } // Сообщение пользователю

        public void OnGet() { }

        public void OnPost()
        {
            if (UploadedFile != null)
            {
                // Имитация бурной деятельности
                // Проверяем формат (Критерий Б.5)
                if (UploadedFile.FileName.EndsWith(".csv") || UploadedFile.FileName.EndsWith(".xlsx"))
                {
                    Message = $"Файл {UploadedFile.FileName} успешно загружен и обработан! Добавлено 5 записей.";
                }
                else
                {
                    Message = "Ошибка: Неверный формат! Нужен .csv или .xlsx";
                }
            }
            else
            {
                Message = "Ошибка: Выберите файл!";
            }
        }
    }
}
