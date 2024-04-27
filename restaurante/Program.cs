using Microsoft.EntityFrameworkCore;
using restaurante.Models;
using restaurante.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Inyeccion por dependencia del string de conexion al conetxto.
builder.Services.AddDbContext<restauranteDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("restauranteDbConnection"));
});

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapHub<EstadoHub>("/estadoHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
