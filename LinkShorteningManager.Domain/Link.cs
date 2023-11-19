using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace LinkShorteningManager.WebApp.Models
{
    public class Link
    {
        public virtual Guid Id { get; set; }
        public virtual string? Url { get; set; }
        public virtual string? ShortKey { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual int ClickCount { get; set; }
    }



    public class LinkMap : ClassMapping<Link>
    {
        public LinkMap()
        {
            Id(link => link.Id, id =>
            {
                id.Generator(Generators.Guid);
                id.Column("Id");
            });
            Property(link => link.Url, url =>
            {
                url.Length(1000);
                url.NotNullable(true);
            });
            Property(link => link.ShortKey, shortKey =>
            {
                shortKey.Length(6);
                shortKey.NotNullable(true);
            });

            Property(link => link.CreationDate, creationDate =>
            {
                creationDate.NotNullable(true);
            });

            Property(link => link.ClickCount, clicksCount =>
            {
                clicksCount.NotNullable(true);
            });

            Table("Link");
        }
    }
}
