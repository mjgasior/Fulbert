using System;
using System.ComponentModel.DataAnnotations;

namespace Fulbert.Infrastructure.Concrete.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class PeselValidationAttribute : ValidationAttribute
    {
        private static readonly int[] multipliers = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };

        public override bool IsValid(object pesel)
        {
            return PeselParser.IsValid((string)pesel);
        }
    }
}
