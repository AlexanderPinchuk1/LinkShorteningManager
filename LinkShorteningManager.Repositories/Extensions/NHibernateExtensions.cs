using LinkShorteningManager.WebApp.Models;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;

namespace LinkShorteningManager.Repositories.Extensions
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
        {
            var configuration = new Configuration();
            configuration.DataBaseIntegration(config =>
            {
                config.Dialect<MySQL55Dialect>();
                config.Driver<NHibernate.Driver.MySqlConnector.MySqlConnectorDriver>();
                config.ConnectionString = connectionString;
            });
            configuration.AddMapping(CreateMappingDocument(new List<Type>() { typeof(LinkMap) }));

            new SchemaUpdate(configuration).Execute(false, true);

            var sessionFactory = configuration.BuildSessionFactory();

            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());

            return services;
        }

        private static HbmMapping CreateMappingDocument(List<Type> types)
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(types);

            return mapper.CompileMappingForAllExplicitlyAddedEntities();
        }
    }
}
