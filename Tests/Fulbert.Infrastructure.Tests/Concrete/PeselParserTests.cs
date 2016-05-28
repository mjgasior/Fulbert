using Fulbert.Infrastructure.Concrete.Validation;
using Fulbert.Tests.Common;
using NUnit.Framework;
using System;

namespace Fulbert.Infrastructure.Tests.Concrete
{
    [Category(TestCategories.INFRASTRUCTURE)]
    public class PeselParserTests
    {
        #region Tests
        [Test]
        [TestCase("000000000000")]
        [TestCase("00000000000")]
        [TestCase("0")]
        [TestCase("")]
        [TestCase("14141400001")]
        [TestCase("10103200003")]
        public void Is_PESEL_number_invalid(string peselString)
        {
            // Act & Arrange
            Assert.IsFalse(PeselParser.IsValid(peselString));
        }

        [Test]
        public void Is_birthday_valid()
        {
            // Arrange
            int[] birthdayDigits = new int[] { 7, 6, 0, 4, 3, 0 };
            DateTime birthday = new DateTime(1976, 4, 30);

            // Act
            DateTime peselBirthday = PeselParser.GetBirthday(birthdayDigits);

            // Assert
            Assert.That(peselBirthday.Date, Is.EqualTo(birthday.Date));
        }

        [Test]
        public void Get_right_numbers_from_string_PESEL()
        {
            // Arrange
            string peselString = "02070803628";
            int[] peselDigits = new int[] { 0, 2, 0, 7, 0, 8, 0, 3, 6, 2, 8 };

            // Act
            int[] resultDigits = PeselParser.GetPeselNumbers(peselString);

            // Assert
            Assert.That(peselDigits, Is.EqualTo(resultDigits));
        }
        #endregion Tests
    }
}
