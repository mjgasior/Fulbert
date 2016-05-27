using Fulbert.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using Fulbert.Infrastructure.Concrete.Mvvm;
using Fulbert.Infrastructure.Concrete.Validation;
using Fulbert.Presentation.Localization.Resources;

[assembly: InternalsVisibleTo("Fulbert.BLL.Services.Tests")]
namespace Fulbert.BLL.ApplicationModels.Models
{
    public class Patient : ValidatableModel
    {
        public Guid Id { get; private set; }

        private string _firstName;
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Labels), ErrorMessageResourceName = nameof(Labels.ErrorRequired))]
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Labels), ErrorMessageResourceName = nameof(Labels.ErrorRequired))]
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private string _pesel;
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Labels), ErrorMessageResourceName = nameof(Labels.ErrorRequired))]
        [RegularExpression("^[0-9]{11}$", ErrorMessageResourceType = typeof(Labels), ErrorMessageResourceName = nameof(Labels.ErrorPeselFormat))]
        [PeselValidation(ErrorMessageResourceType = typeof(Labels), ErrorMessageResourceName = nameof(Labels.ErrorPeselValidation))]
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

        #region Methods
        public string ToFullNameString()
        {
            return string.Format(Formatting.S0_1, FirstName, LastName);
        }

        public override string ToString()
        {
            return string.Format(Formatting.S0_1_2, Id, FirstName, LastName);
        }
        #endregion Methods
    }
}
