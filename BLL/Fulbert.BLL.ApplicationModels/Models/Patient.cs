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
            set
            {
                ValidatePesel(value);
                SetProperty(ref _pesel, value);
            }
        }

        public ICollection<Appointment> Appointments { get; set; }

        internal Patient(Guid id)
        {
            Id = id;
        }

        public Patient()
        {

        }

        #region Methods
        public string ToFullNameString()
        {
            return string.Format(Formatting.S0_1, FirstName, LastName);
        }

        public override string ToString()
        {
            return string.Format(Formatting.S0_1_2, Id, FirstName, LastName);
        }

        public void ValidatePesel(string valueToValidate)
        {
            string property = nameof(Pesel);
            if (!Models.Pesel.IsValid(valueToValidate))
            {
                AddError(property, Labels.PeselIncorrect, false);
            }
            else
            {
                RemoveError(property, Labels.PeselIncorrect);
            }

            if (valueToValidate == null || valueToValidate.Length < 11)
            {
                AddError(property, Labels.PeselTooShort, true);
            }
            else
            {
                RemoveError(property, Labels.PeselTooShort);
            }
        }
        #endregion Methods

        #region INotifyDataErrorInfo
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        public bool HasErrors
        {
            get { return _errors.Count > 0; }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_errors.ContainsKey(propertyName))
            {
                return null;
            }
            return _errors[propertyName];
        }

        public void AddError(string propertyName, string error, bool isWarning)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors[propertyName] = new List<string>();
            }

            if (!_errors[propertyName].Contains(error))
            {
                if (isWarning)
                {
                    _errors[propertyName].Add(error);
                }
                else
                {
                    _errors[propertyName].Insert(0, error);
                }
                RaiseErrorsChanged(propertyName);
            }
        }

        public void RemoveError(string propertyName, string error)
        {
            if (_errors.ContainsKey(propertyName) && _errors[propertyName].Contains(error))
            {
                _errors[propertyName].Remove(error);
                if (_errors[propertyName].Count == 0)
                {
                    _errors.Remove(propertyName);
                }
                RaiseErrorsChanged(propertyName);
            }
        }

        public void RaiseErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        #endregion INotifyDataErrorInfo
    }
}
