using Fulbert.BLL.Services.Services;
using Fulbert.DAL.PatientDAL;
using Fulbert.Tests.Common;
using NHibernate;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;
using Fulbert.DAL.PatientDAL.Tests;
using Fulbert.BLL.ApplicationModels.Models;
using Fulbert.DAL.RepositoryModels.Models;

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

        [Test]
        public void Integrated_verify_if_AutoMapper_works_properly()
        {

            // Arrange
            DateTime appointmentDate = DateTime.Now;
            var appointment = new Appointment
            {
                Date = appointmentDate
            };

            string firstName = "Josh";
            string lastName = "Homme";

            DatabaseTools.AddPatientToDatabase(firstName, lastName, appointmentDate);
            Guid patientId = DatabaseTools.GetPatientFromDatabase(firstName, lastName).First().Id;

            DateTime appointmentDate2 = DateTime.Now - TimeSpan.FromDays(5);
            var appointment2 = new Appointment
            {
                Date = appointmentDate2
            };

            DateTime appointmentDate3 = DateTime.Now - TimeSpan.FromDays(10);
            var appointment3 = new Appointment
            {
                Date = appointmentDate3
            };

            // Act
            Patient patient = _patientService.GetPatientById(patientId);
            patient.Appointments.Add(appointment2);
            patient.Appointments.Add(appointment3);
            _patientService.UpdatePatient(patient);

            // Assert
            Patient patient2 = _patientService.GetPatientById(patientId);
            Assert.That(patient2.Appointments.Count, Is.EqualTo(3));
            Assert.That(patient2.FirstName, Is.EqualTo(firstName));
            Assert.That(patient2.LastName, Is.EqualTo(lastName));

            IList<PatientEntity> patientEntities = DatabaseTools.GetPatientFromDatabase(firstName, lastName);
            Assert.AreEqual(patientEntities.Count, 1);
            Assert.AreEqual(patientEntities.First().Appointments.Count, 3);
        }

        [Test]
        public void Integrated_verify_if_AutoMapper_works_properly_with_two_patients()
        {
            // Arrange
            string firstName = "Fernando";
            string lastName = "Ribeiro";
            DateTime appointmentDate = DateTime.Now;

            DatabaseTools.AddPatientToDatabase(firstName, lastName, appointmentDate);
            Guid patientId = DatabaseTools.GetPatientFromDatabase(firstName, lastName).First().Id;

            DateTime appointmentDate2 = DateTime.Now - TimeSpan.FromDays(5);
            var appointment2 = new Appointment
            {
                Date = appointmentDate2
            };

            DateTime appointmentDate3 = DateTime.Now - TimeSpan.FromDays(10);
            var appointment3 = new Appointment
            {
                Date = appointmentDate3
            };

            string firstName2 = "Wayne";
            string lastName2 = "Static";
            DatabaseTools.AddPatientToDatabase(firstName2, lastName2, appointmentDate, appointmentDate2);
            Guid patientId2 = DatabaseTools.GetPatientFromDatabase(firstName2, lastName2).First().Id;

            // Act
            Patient patient = _patientService.GetPatientById(patientId);
            patient.Appointments.Add(appointment2);
            patient.Appointments.Add(appointment3);
            _patientService.UpdatePatient(patient);

            // Assert
            Patient patientCheck = _patientService.GetPatientById(patientId);
            Assert.That(patientCheck.Appointments.Count, Is.EqualTo(3));
            Assert.That(patientCheck.FirstName, Is.EqualTo(firstName));
            Assert.That(patientCheck.LastName, Is.EqualTo(lastName));

            Patient patient2Check = _patientService.GetPatientById(patientId2);
            Assert.That(patient2Check.Appointments.Count, Is.EqualTo(2));
            Assert.That(patient2Check.FirstName, Is.EqualTo(firstName2));
            Assert.That(patient2Check.LastName, Is.EqualTo(lastName2));
        }

        [Test]
        public void Integrated_get_all_patients()
        {
            // Arrange
            string firstName = "Shavo";
            string lastName = "Odadjian";
            DateTime appointmentDate = DateTime.Now;
            DatabaseTools.AddPatientToDatabase(firstName, lastName, appointmentDate, appointmentDate, appointmentDate);
            DatabaseTools.AddPatientToDatabase("Daron", "Malakian", appointmentDate);
            DatabaseTools.AddPatientToDatabase("John", "Dolmayan", appointmentDate);

            // Act
            ICollection<Patient> patients = _patientService.GetAllPatients();

            // Assert
            Assert.That(patients.Count, Is.EqualTo(3));

            Patient patient = patients.First(x => x.FirstName == firstName);
            Assert.That(patient.FirstName, Is.EqualTo(firstName));
            Assert.That(patient.LastName, Is.EqualTo(lastName));
            Assert.That(patient.Appointments.Count, Is.EqualTo(3));

            patient.Appointments.ToList().ForEach(appointment =>
            {
                Assert.That(appointment.Date.Date, Is.EqualTo(appointmentDate.Date));
            });

            Assert.That(patients.Any(x => x.FirstName == "Daron"));
            Assert.That(patients.Any(x => x.FirstName == "John"));
        }
        #endregion Tests
    }
}
