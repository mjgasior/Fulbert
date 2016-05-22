using System;

namespace Fulbert.BLL.ApplicationModels.Models
{
    public class Pesel
    {
        private static readonly int[] multipliers = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };

        private readonly string _personalIdString;
        private readonly int[] _peselNumbers;

        public bool IsAWoman { get; private set; }

        public Pesel(string personalIdString)
        {
            _personalIdString = personalIdString;
            _peselNumbers = GetPeselNumbers();
            IsAWoman = IsEven(_peselNumbers[9]);
        }

        public static bool IsEven(int value)
        {
            return value % 2 == 0;
        }

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

        private int GetBirthDay()
        {
            return _peselNumbers[4] * 10 + _peselNumbers[5];
        }

        private int GetBirthMonth()
        {
            //int birthMonth = _peselNumbers[3];
            //// here is the error - 17th month returned
            //var month = (_peselNumbers[2] < 2) ? ExtractMonth() : birthMonth;
            //return month;
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

        public override string ToString()
        {
            return _personalIdString;
        }

        public static bool IsValid(string pesel)
        {
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

        private static object CountCheckSum(string pesel)
        {
            int sum = 0;
            for (int i = 0; i < multipliers.Length; i++)
            {
                sum += multipliers[i] * int.Parse(pesel[i].ToString());
            }

            int reszta = sum % 10;
            return reszta == 0 ? reszta.ToString() : (10 - reszta).ToString();
        }
    }
}
