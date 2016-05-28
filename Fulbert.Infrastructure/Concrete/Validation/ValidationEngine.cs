using System;
using System.Collections;
using System.ComponentModel;
using Fulbert.Infrastructure.Abstract.Validation;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Practices.ObjectBuilder2;

namespace Fulbert.Infrastructure.Concrete.Validation
{
    /// <summary>
    /// based on code from: https://anthymecaillard.wordpress.com/2012/03/26/wpf-4-5-validation-asynchrone/
    /// </summary>
    public class ValidationEngine : IValidationEngine
    {
        #region Fields and Properties
        private object _lock = new object();
        private readonly INotifyPropertyChanged _validationTarget;
        private ConcurrentDictionary<string, List<string>> _errors = new ConcurrentDictionary<string, List<string>>();
        private readonly Action<string> _errorEventInvoke;

        public bool HasErrors
        {
            get
            {
                return _errors.Any(keyValue => keyValue.Value != null && keyValue.Value.Count > 0);
            }
        }
        #endregion Fields and Properties

        public ValidationEngine(INotifyPropertyChanged validationTarget, Action<string> errorEventInvoke)
        {
            _validationTarget = validationTarget;
            _validationTarget.PropertyChanged += OnPropertyChanged;
            _errorEventInvoke = errorEventInvoke;
            Validate();
        }

        #region Methods
        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IEnumerable<string> errorKeys = await Task.Run(() => Validate());
            errorKeys.ForEach(key => OnErrorsChanged(key));
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

        private IEnumerable<string> Validate()
        {
            lock (_lock)
            {
                var errorsChangedKeysList = new List<string>();
                var validationContext = new ValidationContext(_validationTarget, null, null);
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(_validationTarget, validationContext, validationResults, true);

                foreach (KeyValuePair<string, List<string>> kv in _errors.ToList())
                {
                    if (validationResults.All(result => result.MemberNames.All(m => m != kv.Key)))
                    {
                        List<string> outLi;
                        _errors.TryRemove(kv.Key, out outLi);
                        errorsChangedKeysList.Add(kv.Key);
                    }
                }

                var q = from r in validationResults
                        from m in r.MemberNames
                        group r by m into g
                        select g;

                foreach (var prop in q)
                {
                    List<string> messages = prop.Select(r => r.ErrorMessage).ToList();

                    if (_errors.ContainsKey(prop.Key))
                    {
                        List<string> outLi;
                        _errors.TryRemove(prop.Key, out outLi);
                    }
                    _errors.TryAdd(prop.Key, messages);
                    errorsChangedKeysList.Add(prop.Key);
                }

                return errorsChangedKeysList;
            }
        }

        private void OnErrorsChanged(string propertyName)
        {
            _errorEventInvoke.Invoke(propertyName);
        }
        #endregion Methods
    }
}
