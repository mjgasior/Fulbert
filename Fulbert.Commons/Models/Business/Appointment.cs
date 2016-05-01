using Fulbert.Commons.Utils;
using System;

namespace Fulbert.Commons.Models.Business
{
    public class Appointment
    {
        public Guid Id { get; private set; }
        public DateTime Date { get; set; }
        public Patient Patient { get; set; }

        public Appointment(Guid id)
        {
            Id = id;
        }

        public Appointment()
        {

        }

        public override string ToString()
        {
            return string.Format(Formatting.S0_1, Id, Date);
        }
    }
}
