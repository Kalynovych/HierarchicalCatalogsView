using HierarchicalCatalogsView;
using HierarchicalCatalogsView.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContextFactory<CatalogContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddSingleton<DataService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.Environment.EnvironmentName = "Production";

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
