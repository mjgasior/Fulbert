using System.Collections;

namespace Fulbert.Infrastructure.Abstract.Validation
{
    public interface IValidationEngine
    {
        bool HasErrors { get; }
        IEnumerable GetErrors(string propertyName);
    }
}
