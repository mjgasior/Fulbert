using FluentNHibernate.Mapping;
using Fulbert.DAL.PatientDAL.Models;

namespace Fulbert.DAL.PatientDAL.Mappings
{
    public class PatientMap : ClassMap<Patient>
    {
        public PatientMap()
        {
            Id(x => x.Id).Column("Id").GeneratedBy.Guid();
            Map(x => x.FirstName);
        }
    }
}
