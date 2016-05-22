using Fulbert.Utils;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections;
using Fulbert.Presentation.Localization.Resources;

[assembly: InternalsVisibleTo("Fulbert.BLL.Services.Tests")]
namespace Fulbert.BLL.ApplicationModels.Models
{
    public class Patient : BindableBase, INotifyDataErrorInfo
    {
        public Guid Id { get; private set; }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private string _pesel;
        public string Pesel
        {
            get { return _pesel; }
            set { SetProperty(ref _pesel, value); }
        }

        public ICollection<Appointment> Appointments { get; set; }


        internal Patient(Guid id)
        {
            Id = id;
        }

        public Patient()
        {

        }

        public string ToFullNameString()
        {
            return string.Format(Formatting.S0_1, FirstName, LastName);
        }

        public override string ToString()
        {
            return string.Format(Formatting.S0_1_2, Id, FirstName, LastName);
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool HasErrors
        {
            get
            {
                return !Models.Pesel.IsValid(_pesel);
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (propertyName == "Pesel")
            {
                if (_pesel == null || _pesel.Length < 11)
                {
                    return new List<string>
                    {
                        Labels.PeselTooShort
                    };
                }
                else
                {
                    return new List<string>
                    {
                        Labels.PeselIncorrect
                    };
                }
                
            }
            return null;
        }
    }
}
