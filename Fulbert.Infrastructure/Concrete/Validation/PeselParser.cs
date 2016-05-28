using System;

namespace Fulbert.Infrastructure.Concrete.Validation
{
    public class PeselParser
    {
        private static readonly int[] multipliers = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };

        #region Methods
        private static bool IsEven(int value)
        {
            return value % 2 == 0;
        }

        private static int GetBirthDay(int[] peselNumbers)
        {
            return peselNumbers[4] * 10 + peselNumbers[5];
        }

        private static int GetBirthMonth(int[] peselNumbers)
        {
            return IsEven(peselNumbers[2]) ? peselNumbers[3] : peselNumbers[3] + 10;
        }

        private static int GetBirthYear(int[] peselNumbers)
        {
            int birthYear = 1900 + peselNumbers[0] * 10 + peselNumbers[1];
            if (peselNumbers[2] >= 2 && peselNumbers[2] < 8)
            {
                birthYear += (peselNumbers[2] / 2) * 100;
            }

            if (peselNumbers[2] >= 8)
            {
                birthYear -= 100;
            }
            return birthYear;
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
        #endregion Methods

        public static int[] GetPeselNumbers(string peselString)
        {
            int peselLength = 11;
            char[] characters = peselString.ToCharArray();
            int[] digits = new int[peselLength];
            for (int i = 0; i < peselLength; i++)
            {
                digits[i] = int.Parse(characters[i].ToString());
            }
            return digits;
        }

        public static DateTime GetBirthday(int[] peselNumbers)
        {
            return new DateTime(GetBirthYear(peselNumbers), GetBirthMonth(peselNumbers), GetBirthDay(peselNumbers));
        }

        public static bool IsValid(string pesel)
        {
            if (pesel == null)
            {
                return false;
            }

            bool toReturn = false;
            try
            {
                if (pesel.Length == 11)
                {
                    toReturn = CountCheckSum(pesel).Equals(pesel[10].ToString());
                }
                int[] peselNumbers = GetPeselNumbers(pesel);
                DateTime birthDate = GetBirthday(peselNumbers);
            }
            catch (Exception)
            {
                toReturn = false;
            }
            return toReturn;
        }
    }
}
