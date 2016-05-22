using Fulbert.Utils;
using System;
using System.Collections.Generic;

namespace Fulbert.DAL.RepositoryModels.Models
{
    public class PatientEntity : BaseEntity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Pesel { get; set; }
        public virtual ICollection<AppointmentEntity> Appointments { get; set; }
        
        internal PatientEntity(Guid id) : this()
        {
            Id = id;
        }

        public PatientEntity()
        {
            Appointments = new List<AppointmentEntity>();
        }

        public virtual void AddAppointment(AppointmentEntity appointment)
        {
            appointment.Patient = this;
            Appointments.Add(appointment);
        }

        public override string ToString()
        {
            return string.Format(Formatting.S0_1_2_3, Id, FirstName, LastName, Pesel);
        }
    }
}
