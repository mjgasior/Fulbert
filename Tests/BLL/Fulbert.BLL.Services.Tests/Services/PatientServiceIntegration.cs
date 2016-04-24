using Fulbert.BLL.Services.Services;
using Fulbert.Commons.Models.Business;
using Fulbert.DAL.PatientDAL;
using Fulbert.Tests.Common;
using NHibernate;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;
using Fulbert.DAL.PatientDAL.Tests;
using Fulbert.Commons.Models.Entities;

namespace Fulbert.BLL.Services.Tests.Services
{
    public class PatientServiceIntegration : BaseIntegrationTest
    {
        private PatientService _patientService;

        public override void Initialize()
        {
            var patientDal = new PatientDal(Database.TEST_DB_NAME);
            _patientService = new PatientService(patientDal);
        }

        #region Tests
        [Test]
        public void Integrated_add_new_patient()
        {
            // Arrange
            string firstName = "Dave";
            string lastName = "Grohl";
            Patient patient = new Patient
            {
                FirstName = firstName,
                LastName = lastName
            };

            // Act
            _patientService.AddNewPatient(patient);

            // Assert
            ISessionFactory sessionForTests = NHibernateConfig.CreateSessionFactory(Database.TEST_DB_NAME);
            IList<PatientEntity> patients;
            using (ISession session = sessionForTests.OpenSession())
            {
                patients = session.QueryOver<PatientEntity>().Fetch(x => x.Appointments).Eager.List();
            }

            Assert.IsNotNull(patients);
            Assert.IsNotEmpty(patients);
            Assert.IsTrue(patients.Count == 1);

            PatientEntity result = patients.First();
            StringAssert.Contains(result.FirstName, firstName);
            StringAssert.Contains(result.LastName, lastName);
        }

        [Test]
        public void Integrated_add_appointment_to_patient()
        {
            // Arrange
            DateTime appointmentDate = DateTime.Now;
            var appointment = new Appointment
            {
                Date = appointmentDate
            };

            string firstName = "Foo";
            string lastName = "Fighters";
            DatabaseTools.AddPatientToDatabase(firstName, lastName, appointmentDate);
            PatientEntity patientEntity = DatabaseTools.GetPatientFromDatabase(firstName, lastName).First();

            Assert.AreEqual(patientEntity.Appointments.Count, 1);

            // Act
            _patientService.AddAppointmentToPatient(patientEntity.Id, appointment);

            // Assert
            PatientEntity updatedPatientEntity = DatabaseTools.GetPatientFromDatabase(firstName, lastName).First();
            Assert.AreEqual(updatedPatientEntity.Appointments.Count, 2);
            Assert.AreEqual(updatedPatientEntity.Appointments.First().Date.Date, appointmentDate.Date);
            Assert.AreEqual(updatedPatientEntity.Appointments.Last().Date.Date, appointmentDate.Date);
        }
        #endregion Tests
    }
}
