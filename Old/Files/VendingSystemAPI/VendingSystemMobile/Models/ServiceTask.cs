using SQLite;
using System;
using System.Collections.Generic;

namespace VendingSystemMobile.Models;

[Table("ServiceTasks")] // <--- Говорит, что это таблица в БД
public class LocalServiceTask
{
    [PrimaryKey] // <--- Говорит, что Id - это уникальный ключ
    public int Id { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }

    // --- ВОТ ГЛАВНЫЕ ИЗМЕНЕНИЯ ДЛЯ БАЛЛОВ ---

    // 1. Для логики принятия/отклонения задачи
    public string Status { get; set; } = "Новая";

    // 2. Для сортировки или отображения
    public DateTime Date { get; set; }

    // 3. Для синхронизации с сервером (оффлайн-режим)
    public bool IsSynced { get; set; } = true;
}

