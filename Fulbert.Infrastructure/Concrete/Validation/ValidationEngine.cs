using System;
using System.Collections;
using System.ComponentModel;
using Fulbert.Infrastructure.Abstract.Validation;

namespace Fulbert.Infrastructure.Concrete.Validation
{
    public class ValidationEngine : IValidationEngine
    {
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors
        {
            get
            {
                throw new NotImplementedException();
            }
        }        

        public IEnumerable GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
