using Fulbert.Commons.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Fulbert.BLL.Services.Tests")]
namespace Fulbert.Commons.Models.Business
{
    public class Patient
    {
        public Guid Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Appointment> Appointments { get; set; }

        internal Patient(Guid id)
        {
            Id = id;
        }

        public Patient()
        {

        }

        public override string ToString()
        {
            return string.Format(Formatting.S0_1_2, Id, FirstName, LastName);
        }
    }
}
