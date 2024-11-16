using RentX.Handlers;
using RentX.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string? connString = builder.Configuration.GetConnectionString("RentXConnection");
builder.Services.AddSqlite<RentXContext>(connString);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapGet("/", () => "hello!");
app.MapControllers(); // To map controller routes



app.Run();
