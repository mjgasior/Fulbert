using Fulbert.Infrastructure.Concrete.Validation;
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
            _peselNumbers = PeselParser.GetPeselNumbers(personalIdString);
            IsAWoman = IsEven(_peselNumbers[9]);
        }

        #region Methods
        private static bool IsEven(int value)
        {
            return value % 2 == 0;
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
            return PeselParser.GetBirthday(_peselNumbers);
        }

        public static bool IsValid(string pesel)
        {
            return PeselParser.IsValid(pesel);
        }

        public override string ToString()
        {
            return _personalIdString;
        }
    }
}
