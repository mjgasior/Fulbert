using System;
using System.Collections.Generic;

namespace Fulbert.Commons.Models.Entities
{
    public class PatientEntity
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual ICollection<AppointmentEntity> Appointments { get; set; }
        public virtual Guid Id { get; set; }

        public PatientEntity()
        {
            Appointments = new List<AppointmentEntity>();
        }

        public virtual void AddAppointment(AppointmentEntity appointment)
        {
            appointment.Patient = this;
            Appointments.Add(appointment);
        }
    }
}
