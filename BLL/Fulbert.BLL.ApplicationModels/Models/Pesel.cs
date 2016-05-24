using System;

namespace Fulbert.BLL.ApplicationModels.Models
{
    public class Pesel
    {
        #region Fields and Properties
        private static readonly int[] multipliers = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };

        private readonly string _personalIdString;
        private readonly int[] _peselNumbers;

        public bool IsAWoman { get; private set; }
        #endregion Fields and Properties

        public Pesel(string personalIdString)
        {
            _personalIdString = personalIdString;
            _peselNumbers = GetPeselNumbers();
            IsAWoman = IsEven(_peselNumbers[9]);
        }

        #region Methods
        private static bool IsEven(int value)
        {
            return value % 2 == 0;
        }

        private int GetBirthDay()
        {
            return _peselNumbers[4] * 10 + _peselNumbers[5];
        }

        private int GetBirthMonth()
        {
            return IsEven(_peselNumbers[2]) ? _peselNumbers[3] : _peselNumbers[3] + 10;
        }

        private int GetBirthYear()
        {
            int birthYear = 1900 + _peselNumbers[0] * 10 + _peselNumbers[1];
            if (_peselNumbers[2] >= 2 && _peselNumbers[2] < 8)
            {
                birthYear += (_peselNumbers[2] / 2) * 100;
            }

            if (_peselNumbers[2] >= 8)
            {
                birthYear -= 100;
            }
            return birthYear;
        }

        private int[] GetPeselNumbers()
        {
            int peselLength = 11;
            char[] characters = _personalIdString.ToCharArray();
            int[] digits = new int[peselLength];
            for (int i = 0; i < peselLength; i++)
            {
                digits[i] = int.Parse(characters[i].ToString());
            }
            return digits;
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

        public int GetAge()
        {
            DateTime birthday = GetBirthday();
            DateTime today = DateTime.Today;
            int age = today.Year - GetBirthday().Year;

            if (birthday > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }

        public DateTime GetBirthday()
        {
            return new DateTime(GetBirthYear(), GetBirthMonth(), GetBirthDay());
        }

        public override string ToString()
        {
            return _personalIdString;
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
            }
            catch (Exception)
            {
                toReturn = false;
            }
            return toReturn;
        }
    }
}
