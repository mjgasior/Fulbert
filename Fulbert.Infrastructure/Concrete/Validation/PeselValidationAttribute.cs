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
            if (pesel == null)
            {
                return false;
            }

            bool toReturn = false;
            try
            {
                string peselString = pesel as string;
                if (peselString.Length == 11)
                {
                    toReturn = CountCheckSum(peselString).Equals(peselString[10].ToString());
                }
            }
            catch (Exception)
            {
                toReturn = false;
            }
            return toReturn;
        }

        private static object CountCheckSum(string pesel)
        {
            int sum = 0;
            for (int i = 0; i < multipliers.Length; i++)
            {
                sum += multipliers[i] * int.Parse(pesel[i].ToString());
            }

            int moduloResult = sum % 10;
            return moduloResult == 0 ? moduloResult.ToString() : (10 - moduloResult).ToString();
        }
    }
}
