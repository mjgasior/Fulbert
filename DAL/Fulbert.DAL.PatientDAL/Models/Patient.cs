using System;

namespace Fulbert.DAL.PatientDAL.Models
{
    public class Patient
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual Guid Id { get; set; }
    }
}
