using FluentNHibernate.Mapping;
using Fulbert.DAL.RepositoryModels.Models;

namespace Fulbert.DAL.PatientDAL.Mappings
{
    public class PatientMap : ClassMap<PatientEntity>
    {
        public PatientMap()
        {
            Id(x => x.Id).Column("Id").GeneratedBy.Guid();
            Map(x => x.FirstName);
            Map(x => x.LastName);
            HasMany(x => x.Appointments)
                .Inverse()
                .Cascade.All();
        }
    }
}
