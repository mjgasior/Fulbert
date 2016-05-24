﻿using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Fulbert.Infrastructure.Concrete.Mvvm
{
    // source: https://anthymecaillard.wordpress.com/2012/03/26/wpf-4-5-validation-asynchrone/
    public class ValidatableModel : BindableBase, INotifyDataErrorInfo
    {
        #region Fields and Properties
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private object _lock = new object();
        private ConcurrentDictionary<string, List<string>> _errors = new ConcurrentDictionary<string, List<string>>();


        public bool HasErrors
        {
            get { return _errors.Any(kv => kv.Value != null && kv.Value.Count > 0); }
        }

        #endregion Fields and Properties

        #region Methods
        protected override void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            //await ValidateAsync();
            Validate(); // making it sync because of command refresh on error changed event
        }

        public void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (propertyName == null)
            {
                return new List<string>();
            }
            List<string> errorsForName;
            _errors.TryGetValue(propertyName, out errorsForName);
            return errorsForName;
        }

        public Task ValidateAsync()
        {
            return Task.Run(() => Validate());
        }

        public void Validate()
        {
            lock (_lock)
            {
                var validationContext = new ValidationContext(this, null, null);
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(this, validationContext, validationResults, true);

                foreach (var kv in _errors.ToList())
                {
                    if (validationResults.All(r => r.MemberNames.All(m => m != kv.Key)))
                    {
                        List<string> outLi;
                        _errors.TryRemove(kv.Key, out outLi);
                        OnErrorsChanged(kv.Key);
                    }
                }

                var q = from r in validationResults
                        from m in r.MemberNames
                        group r by m into g
                        select g;

                foreach (var prop in q)
                {
                    var messages = prop.Select(r => r.ErrorMessage).ToList();

                    if (_errors.ContainsKey(prop.Key))
                    {
                        List<string> outLi;
                        _errors.TryRemove(prop.Key, out outLi);
                    }
                    _errors.TryAdd(prop.Key, messages);
                    OnErrorsChanged(prop.Key);
                }
            }
        }
        #endregion Methods
    }
}
