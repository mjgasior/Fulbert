using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

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
    }
}
