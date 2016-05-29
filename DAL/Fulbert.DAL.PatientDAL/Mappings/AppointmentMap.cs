using FluentNHibernate.Mapping;
using Fulbert.DAL.RepositoryModels.Models;

namespace Fulbert.DAL.PatientDAL.Mappings
{
    public class AppointmentMap : ClassMap<AppointmentEntity>
    {
        public AppointmentMap()
        {
            Id(x => x.Id).Column("Id").GeneratedBy.Guid();
            Map(x => x.Date);
            Map(x => x.Interview);
            References(x => x.Patient);
        }
    }
}
