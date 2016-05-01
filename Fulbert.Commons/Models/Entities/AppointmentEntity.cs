using System;

namespace Fulbert.Commons.Models.Entities
{
    public class AppointmentEntity : BaseEntity
    {
        public virtual DateTime Date { get; set; }
        public virtual PatientEntity Patient { get; set; }

        internal AppointmentEntity(Guid id)
        {
            Id = id;
        }

        public AppointmentEntity()
        {

        }
    }
}