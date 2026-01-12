using System;
using System.Collections.Generic;

namespace VendingSystemAPI.Models;

public class ServiceTask
{
    public int Id { get; set; }
    public string Description { get; set; } // Описание проблемы
    public string Address { get; set; }     // Адрес автомата
    public DateTime Date { get; set; }      // Дата
    public string Status { get; set; }      // Статус (Новая, В работе)
}
