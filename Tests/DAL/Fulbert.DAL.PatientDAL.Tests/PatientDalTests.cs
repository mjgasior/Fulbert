using Fulbert.DAL.PatientDAL.Abstract;
using Fulbert.DAL.PatientDAL.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace Fulbert.DAL.PatientDAL.Tests
{
    public class PatientDalTests : BaseTest
    {
        [Test]
        public void Add_patient_to_database()
        {
            // Arrange
            IPatientDal patientDal = new PatientDal();

            // Act
            patientDal.AddPatient(new Patient { FirstName = "test" });

            // Assert
        }

        [Test]
        public void Get_all_patients_from_database()
        {
            // Arrange
            IPatientDal patientDal = new PatientDal();

            // Act
            IEnumerable<Patient> patients = patientDal.GetAllPatients();

            // Assert
        }
    }
}
