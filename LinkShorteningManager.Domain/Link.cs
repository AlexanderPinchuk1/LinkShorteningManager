using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkShorteningManager.WebApp.Models
{
    public class Link
    {
        public Guid Id { get; set; }
        public string? Url { get; set; }
        public string? ShortKey { get; set; }
        public DateTime CreationDate { get; set; }
        public int ClickCount { get; set; }
    }



    public class LinkConfig : IEntityTypeConfiguration<Link>
    {
        public void Configure(EntityTypeBuilder<Link> builder)
        {
            builder.HasKey(link => link.Id);
            builder.Property(link => link.Url)
                .IsRequired()
                .HasMaxLength(1000);
            builder.Property(link => link.ShortKey)
                .IsRequired()
                .HasMaxLength(6);
            builder.Property(link => link.CreationDate)
                .IsRequired();
            builder.Property(link => link.ClickCount)
                .IsRequired();
            builder
                .ToTable(name: "Link");
        }
    }
}
