using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Fulbert.DAL.PatientDAL.Tests")]
namespace Fulbert.DAL.PatientDAL
{
    internal class NHibernateConfig
    {
        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
              .Database(PostgreSQLConfiguration.PostgreSQL82
                            .ConnectionString(c =>
                                c.Host("127.0.0.1")
                                .Port(5432)
                                .Database("AAV")
                                .Username("postgres")
                                .Password("123456")))
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHibernateConfig>())
              .BuildSessionFactory();
        }

        public static ISessionFactory CreateSessionFactoryWithDBReset(string databaseName)
        {
            return Fluently.Configure()
              .Database(PostgreSQLConfiguration.PostgreSQL82
                            .ConnectionString(c =>
                                c.Host("127.0.0.1")
                                .Port(5432)
                                .Database(databaseName)
                                .Username("postgres")
                                .Password("123456")))
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHibernateConfig>())
              .ExposeConfiguration(config =>
              {
                  var export = new SchemaExport(config);
                  export.Drop(false, true);
                  export.Create(true, true);
              })
              .BuildSessionFactory();
        }
    }
}
