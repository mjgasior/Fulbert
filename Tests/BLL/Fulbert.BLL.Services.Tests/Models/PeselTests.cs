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
        public void Is_gender_valid()
        {
            // Arrange
            string peselString = "02271400004";
            // numbers 0, 2, 4, 6, 8 – denote female gender
            // numbers 1, 3, 5, 7, 9 – denote male gender

            // Act
            bool isValid = Pesel.IsValid(peselString);
            Assert.IsTrue(isValid);

            var pesel = new Pesel(peselString);

            // Assert
            Assert.IsTrue(pesel.IsAWoman);
        }

        [Test]
        public void Is_birthday_valid()
        {
            // Arrange
            string peselString = "02271400004";
            DateTime birthday = new DateTime(2014, 7, 14);

            // Act
            bool isValid = Pesel.IsValid(peselString);
            Assert.IsTrue(isValid);

            var pesel = new Pesel(peselString);
            DateTime peselBirthday = pesel.GetBirthday();

            // Assert
            Assert.That(peselBirthday, Is.EqualTo(birthday));
        }
    }
}
