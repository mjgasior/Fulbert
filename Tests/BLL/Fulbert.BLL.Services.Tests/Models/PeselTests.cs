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
            DateTime birthday = new DateTime(2002, 7, 14);

            // Act
            bool isValid = Pesel.IsValid(peselString);
            Assert.IsTrue(isValid);

            var pesel = new Pesel(peselString);
            DateTime peselBirthday = pesel.GetBirthday();

            // Assert
            Assert.That(peselBirthday, Is.EqualTo(birthday));
        }

    //        Rozważmy PESEL osoby urodzonej 8 lipca 1902 roku, płci żeńskiej(parzysta końcówka numeru z serii – 0362). Czyli mamy wówczas 0207080362. Teraz kolejne cyfry należy przemnożyć przez odpowiednie wagi i dodać do siebie.
    //0*1 + 2*3 + 0*7 + 7*9 + 0*1 + 8*3 + 0*7 + 3*9 + 6*1 + 2*3 = 0 + 6 + 0 + 63 + 0 + 24 + 0 + 27 + 6 + 6 = 132
    //Wynik dzielimy modulo przez 10.
    //132 mod 10 = 2
    //A następnie odejmujemy od 10
    //10 - 2 = 8.
    //I wynik dzielimy znów modulo 10
    //8 mod 10 = 8
    //Cyfra kontrolna to 8, zatem nasz prawidłowy numer PESEL to: 02070803628
    }
}
