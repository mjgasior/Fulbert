using FluentNHibernate.Mapping;
using Fulbert.Commons.Models.Entities;

namespace Fulbert.DAL.PatientDAL.Mappings
{
    public class AppointmentMap : ClassMap<Appointment>
    {
        public AppointmentMap()
        {
            Id(x => x.Id).Column("Id").GeneratedBy.Guid();
            Map(x => x.Date);
            References(x => x.Patient);
        }
    }
}
