using Fulbert.DAL.PatientDAL.Models;
using NUnit.Framework;

namespace Fulbert.DAL.PatientDAL.Tests
{
    public class PatientDalTests : BaseTest
    {
        [Test]
        public void Add_patient_to_database()
        {
            // Arrange
            var patientDal = new PatientDal();

            // Act
            patientDal.AddPatient(new Patient { FirstName = "test" });


            // Assert
        }
    }
}
