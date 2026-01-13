using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingSystemMobile.Models
{
    // Data Transfer Object - Объект для Передачи Данных
    // Нужен, чтобы безопасно принимать данные с API
    public class ServiceTaskDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        // Добавь сюда другие поля, если твой API их возвращает
    }
}
