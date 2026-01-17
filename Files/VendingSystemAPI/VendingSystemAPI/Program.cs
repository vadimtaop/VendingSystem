using Microsoft.EntityFrameworkCore;
using VendingSystemAPI.Models;

var builder = WebApplication.CreateBuilder(args);



// Добавляем DbContext (подключение к БД)
builder.Services.AddDbContext<VendingSystemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Добавляем CORS (для WPF)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Эта строка должна быть на месте!
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});


// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseRouting(); // Добавить
app.UseCors("AllowAll"); // Добавить

app.UseAuthorization();

app.MapControllers();

app.Run();
