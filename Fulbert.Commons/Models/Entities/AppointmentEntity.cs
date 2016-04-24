using System;

namespace Fulbert.Commons.Models.Entities
{
    public class AppointmentEntity
    {
        public virtual Guid Id { get; protected set; }
        public virtual DateTime Date { get; set; }
        public virtual PatientEntity Patient { get; set; }
    }
}