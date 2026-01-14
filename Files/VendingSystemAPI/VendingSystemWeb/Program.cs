using VendingSystemWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// --- НАЧАЛО НОВОГО КОДА ---

// 1. Добавляем "паспортный контроль" с помощью Cookies
builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    options.Cookie.Name = "MyCookieAuth";
    options.LoginPath = "/Login"; // Указываем, куда перенаправлять, если пользователь не вошел
});

// 2. Добавляем сервисы Razor Pages и наш ApiService
builder.Services.AddRazorPages();
builder.Services.AddSingleton<VendingSystemWeb.Services.ApiService>(); // Регистрируем наш ApiService

// --- КОНЕЦ НОВОГО КОДА ---

var app = builder.Build();

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

// --- НАЧАЛО НОВОГО КОДА ---
// Включаем "паспортный контроль" и авторизацию
app.UseAuthentication();
app.UseAuthorization();
// --- КОНЕЦ НОВОГО КОДА ---

app.MapRazorPages();

app.Run();
