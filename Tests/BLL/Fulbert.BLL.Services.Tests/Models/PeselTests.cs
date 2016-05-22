using Fulbert.BLL.ApplicationModels.Models;
using Fulbert.Tests.Common;
using NUnit.Framework;
using System;

namespace Fulbert.BLL.Services.Tests.Models
{
    [Category(TestCategories.APPLICATION)]
    public class PeselTests : BaseTest
    {
        [Test]
        [TestCase("10241401823", ExpectedResult = true)]
        [TestCase("62010415616", ExpectedResult = false)]
        public bool Is_gender_valid(string peselString)
        {
            // Arrange

            // Act
            AssertIsPeselValid(peselString);

            var pesel = new Pesel(peselString);

            // Assert
            return pesel.IsAWoman;
        }

        [Test]
        [TestCase("11521218830", 2111, 12, 12)]
        [TestCase("02271400004", 2002, 7, 14)]
        [TestCase("76043007270", 1976, 4, 30)]
        [TestCase("02070803628", 1902, 7, 8)]
        public void Is_birthday_valid(string peselString, int year, int month, int day)
        {
            // Arrange
            DateTime birthday = new DateTime(year, month, day);

            // Act
            AssertIsPeselValid(peselString);

            var pesel = new Pesel(peselString);
            DateTime peselBirthday = pesel.GetBirthday();

            // Assert
            Assert.That(peselBirthday, Is.EqualTo(birthday));
        }

        [Test]
        public void Is_age_calculated_correctly()
        {
            // Arrange
            string peselString = "74082615670";
            DateTime now = DateTime.Now;
            DateTime birthday = new DateTime(1974, 08, 26);

            int age = DateTime.Now.Year - birthday.Year;
            if (now.Month < birthday.Month && now.Day < birthday.Day)
            {
                age--;
            }

            // Act
            AssertIsPeselValid(peselString);
            var pesel = new Pesel(peselString);
            int ageFromPesel = pesel.GetAge();

            // Assert
            Assert.That(ageFromPesel, Is.EqualTo(age));
        }

        private void AssertIsPeselValid(string peselString)
        {
            bool isValid = Pesel.IsValid(peselString);
            Assert.IsTrue(isValid);
        }
    }
}

