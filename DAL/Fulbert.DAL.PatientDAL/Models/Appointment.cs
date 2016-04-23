using System;

namespace Fulbert.DAL.PatientDAL.Models
{
    public class Appointment
    {
        public virtual Guid Id { get; protected set; }
        public virtual DateTime Date { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
