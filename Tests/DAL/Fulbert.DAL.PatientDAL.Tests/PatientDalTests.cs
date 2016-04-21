using Fulbert.DAL.PatientDAL.Abstract;
using Fulbert.DAL.PatientDAL.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace Fulbert.DAL.PatientDAL.Tests
{
    public class PatientDalTests : BaseTest
    {
        private PatientDal _patientDal;

        public override void Initialize()
        {
            _patientDal = new PatientDal("AAV");
        }

        [Test]
        public void Add_patient_to_database()
        {
            // Arrange

            // Act
            _patientDal.AddPatient(new Patient
            {
                FirstName = "Peter",
                LastName = "Steele"
            });

            // Assert
        }

        [Test]
        public void Get_all_patients_from_database()
        {
            // Arrange

            // Act
            IEnumerable<Patient> patients = _patientDal.GetAllPatients();

            // Assert
            Assert.IsNotEmpty(patients);
        }

        [Test]
        public void Delete_patient_from_database()
        {

        }
    }
}
