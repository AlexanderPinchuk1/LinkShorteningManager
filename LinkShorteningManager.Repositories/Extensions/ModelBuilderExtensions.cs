using LinkShorteningManager.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkShorteningManager.Repositories.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Link>().HasData(new List<Link>()
            {
                new()
                {
                    Id = Guid.Parse("1449cb7b-9a35-4f7e-aec0-900d2296a1ca"),
                    Url = "https://www.google.com/",
                    ShortKey = "fg3xj6",
                    ClickCount = 0,
                    CreationDate =  new DateTime(2019,05,09,09,15,00)
                },
                new()
                {
                    Id = Guid.Parse("f8631251-6ed9-4a4d-a909-7471f96b88c4"),
                    Url = "https://yandex.by/",
                    ShortKey = "tc4sjr",
                    ClickCount = 0,
                    CreationDate =  new DateTime(2023,08,05,10,45,00)
                },
            });
        }
    }
}
