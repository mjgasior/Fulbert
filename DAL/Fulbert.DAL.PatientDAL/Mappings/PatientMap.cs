using FluentNHibernate.Mapping;
using Fulbert.Commons.Models.Entities;

namespace Fulbert.DAL.PatientDAL.Mappings
{
    public class PatientMap : ClassMap<Patient>
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
