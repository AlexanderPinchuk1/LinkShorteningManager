using LinkShorteningManager.Foundation;
using LinkShorteningManager.Foundation.Interfaces;
using LinkShorteningManager.Repositories.Extensions;
using LinkShorteningManager.Repositories.Repository;
using LinkShorteningManager.Repositories.Repository.Interfaces;
using LinkShorteningManager.WebApp.Models;
using LinkShorteningManager.WebApp.Profiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNHibernate(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddAutoMapper(typeof(LinkMappingProfile));

builder.Services.AddScoped<IRepository<Link>, Repository<Link>>();
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
