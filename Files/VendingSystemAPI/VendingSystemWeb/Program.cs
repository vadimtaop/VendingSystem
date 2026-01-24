using Microsoft.EntityFrameworkCore;
using VendingSystemAPI.Models;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


// --- НАЧАЛО БАЗОВОЙ КОНФИГУРАЦИИ ---

// 1. Добавляем сервисы для Razor Pages.
builder.Services.AddRazorPages();

// 2. Получаем строку подключения из appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 3. Регистрируем ваш DbContext для работы с базой данных
builder.Services.AddDbContext<VendingSystemDbContext>(options =>
    options.UseSqlServer(connectionString));

// --- КОНЕЦ БАЗОВОЙ КОНФИГУРАЦИИ ---





var app = builder.Build(); // было уже

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
