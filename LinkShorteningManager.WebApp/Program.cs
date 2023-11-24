using LinkShorteningManager.Foundation;
using LinkShorteningManager.Foundation.Interfaces;
using LinkShorteningManager.Repositories;
using LinkShorteningManager.Repositories.UnitOfWork;
using LinkShorteningManager.Repositories.UnitOfWork.Interfaces;
using LinkShorteningManager.WebApp.Profiles;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<LinkShorteningManagerDbContext>(options =>
    options.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString)));

builder.Services.AddAutoMapper(typeof(LinkMappingProfile));
builder.Services.AddScoped<ILinkShorteningManagerUnitOfWork, LinkShorteningManagerUnitOfWork>();
builder.Services.AddScoped<ILinkService, LinkService>();
builder.Services.AddScoped<IRandomGenerator, RandomGenerator>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        "ShortUrl",
        "{key}",
        new { controller = "Link", action = "Index", key = "Default", });

    endpoints.MapControllerRoute(
        "Default",
        "{controller=Link}/{action=Index}/{id?}");
});

app.Run();
