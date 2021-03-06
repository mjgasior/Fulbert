﻿using Fulbert.Infrastructure.Abstract.Validation;
using Fulbert.Infrastructure.Concrete.Validation;
using Prism.Mvvm;
using System;
using System.Collections;
using System.ComponentModel;

namespace Fulbert.Infrastructure.Concrete.Mvvm
{
    public abstract class ValidatableModel : BindableBase, INotifyDataErrorInfo
    {
        #region Fields and Properties
        private IValidationEngine _validator;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors
        {
            get { return _validator.HasErrors; }
        }

        #endregion Fields and Properties

        public ValidatableModel()
        {
            _validator = new ValidationEngine(this, OnErrorsChanged);
        }

        #region Methods

        public void ForceValidation()
        {
            OnPropertyChanged(null);
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _validator.GetErrors(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion Methods
    }
}
