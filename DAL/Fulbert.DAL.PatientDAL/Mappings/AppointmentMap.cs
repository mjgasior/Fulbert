using FluentNHibernate.Mapping;
using Fulbert.DAL.PatientDAL.Models;

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
