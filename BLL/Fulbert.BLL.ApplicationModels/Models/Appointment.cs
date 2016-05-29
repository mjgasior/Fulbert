using Fulbert.Utils;
using Prism.Mvvm;
using System;

namespace Fulbert.BLL.ApplicationModels.Models
{
    public class Appointment : BindableBase
    {
        public Guid Id { get; private set; }
        public DateTime Date { get; set; }

        private string _interview;
        public string Interview
        {
            get { return _interview; }
            set { SetProperty(ref _interview, value); }
        }

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
