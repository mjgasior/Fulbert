using System;
using System.Collections.Generic;

namespace Fulbert.Commons.Models.Business
{
    public class Patient
    {
        public Guid Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Appointment> Appointments { get; set; }

        public Patient(Guid id)
        {
            Id = id;
        }

        public Patient()
        {

        }
    }
}
