using VendingSystemWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Добавляем поддержку Razor Pages (уже есть)
builder.Services.AddRazorPages();

// --- ДОБАВЛЯЕМ ЭТОТ БЛОК ---
builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.Cookie.Name = "VendingManagerCookie";
        options.LoginPath = "/Login"; // Куда перенаправлять, если не вошел
    });
// -----------------------------

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

// --- И ЭТИ ДВЕ СТРОКИ (в этом порядке!) ---
app.UseAuthentication(); // 1. Кто ты?
app.UseAuthorization();  // 2. Можно ли тебе сюда?
// -----------------------------------------

app.MapRazorPages();

app.Run();
