using Fulbert.DAL.RepositoryModels.Abstract;
using Fulbert.DAL.RepositoryModels.Models;
using Fulbert.Tests.Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fulbert.DAL.PatientDAL.Tests
{
    [Category(TestCategories.DAL)]
    public class PatientDalTests : BaseTest
    {
        private IPatientDal _patientDal;

        public override void Initialize()
        {
            _patientDal = new PatientDal(Database.TEST_DB_NAME);
        }

        #region Tests
        [Test]
        public void Add_patient_to_database()
        {
            // Arrange
            string firstName = "Peter";
            string lastName = "Steele";
            DateTime appointmentDate = DateTime.Now;

            // Act
            var patient = new PatientEntity
                          {
                            FirstName = firstName,
                            LastName = lastName,
                          };
            patient.AddAppointment(new AppointmentEntity { Date = appointmentDate });
            _patientDal.SaveOrUpdatePatient(patient);

            IList<PatientEntity> query = DatabaseTools.GetPatientFromDatabase(firstName, lastName);

            // Assert
            Assert.IsNotNull(query);
            Assert.IsNotEmpty(query);
            Assert.IsTrue(query.Count == 1);

            PatientEntity result = query.First();
            StringAssert.Contains(result.FirstName, firstName);
            StringAssert.Contains(result.LastName, lastName);

            ICollection<AppointmentEntity> appointments = result.Appointments;
            Assert.IsNotEmpty(appointments);
            Assert.IsTrue(appointments.Count == 1);
        }

        [Test]
        public void Delete_patient_from_database()
        {
            // Arrange
            string firstName = "Josh";
            string lastName = "Silver";
            DateTime appointmentDate = DateTime.Now;

            DatabaseTools.AddPatientToDatabase(firstName, lastName, appointmentDate);
            IList<PatientEntity> patients = DatabaseTools.GetPatientFromDatabase(firstName, lastName);

            // Act
            _patientDal.DeletePatient(patients.First());

            // Assert
            IList<PatientEntity> patientsAfterDelete = DatabaseTools.GetPatientFromDatabase(firstName, lastName);
            Assert.IsNotNull(patientsAfterDelete);
            Assert.IsEmpty(patientsAfterDelete);

            IList<AppointmentEntity> appointments = DatabaseTools.GetAllAppointments();
            Assert.IsNotNull(appointments);
            Assert.IsEmpty(appointments);
        }

        [Test]
        public void Get_patient_by_id()
        {
            // Arrange
            string firstName = "Sal";
            string lastName = "Abruscato";
            DateTime appointmentDate = DateTime.Now;

            DatabaseTools.AddPatientToDatabase(firstName, lastName, appointmentDate);
            Guid patientId = DatabaseTools.GetPatientFromDatabase(firstName, lastName).First().Id;

            // Act
            PatientEntity patient = _patientDal.GetPatientById(patientId);

            // Assert
            StringAssert.Contains(patient.FirstName, firstName);
            StringAssert.Contains(patient.LastName, lastName);

            Assert.IsNotEmpty(patient.Appointments);
            Assert.AreEqual(patient.Appointments.First().Date.Date, appointmentDate.Date);
        }

        [Test]
        public void Get_all_patients_from_database()
        {
            // Arrange
            string firstName = "Johnny";
            string lastName = "Kelly";
            DateTime appointmentDate = DateTime.Now;

            DatabaseTools.AddPatientToDatabase(firstName, lastName, appointmentDate);
            
            // Act
            IEnumerable<PatientEntity> patients = _patientDal.GetAllPatients();

            // Assert
            Assert.IsNotNull(patients);
            Assert.IsNotEmpty(patients);
            Assert.IsTrue(patients.Count() == 1);

            PatientEntity result = patients.First();
            StringAssert.Contains(result.FirstName, firstName);
            StringAssert.Contains(result.LastName, lastName);

            ICollection<AppointmentEntity> appointments = result.Appointments;
            Assert.IsNotEmpty(appointments);
            Assert.IsTrue(appointments.Count == 1);
            Assert.AreEqual(appointments.First().Date.Date, appointmentDate.Date);
        }
        
        [Test]
        public void Get_all_appointments_from_database()
        {
            // Arrange
            string firstName = "Kenny";
            string lastName = "Hickey";
            DateTime appointmentDate = DateTime.Now;

            DatabaseTools.AddPatientToDatabase(firstName, lastName, appointmentDate);

            // Act
            IList<AppointmentEntity> appointments = _patientDal.GetAllAppointments();

            // Assert
            Assert.IsNotNull(appointments);
            Assert.IsNotEmpty(appointments);
            Assert.IsTrue(appointments.Count() == 1);
            AppointmentEntity appointment = appointments.First();
            Assert.AreEqual(appointment.Date.Date, appointmentDate.Date);

            Assert.AreEqual(appointment.Patient.FirstName, firstName);
            Assert.AreEqual(appointment.Patient.LastName, lastName);
        }

        [Test]
        public void Add_additional_appointment_to_a_patient()
        {
            // Arrange
            string firstName = "Type O Negative";
            string lastName = "Carnivore";
            DateTime newerAppointmentDate = DateTime.Now;
            DateTime olderAppointmentDate = DateTime.Now - TimeSpan.FromDays(7);

            DatabaseTools.AddPatientToDatabase(firstName, lastName, newerAppointmentDate);

            IEnumerable<PatientEntity> patients = _patientDal.GetAllPatients();
            PatientEntity patient = patients.First(x => x.FirstName == firstName);

            // Act
            patient.AddAppointment(new AppointmentEntity { Date = olderAppointmentDate });
            _patientDal.SaveOrUpdatePatient(patient);

            // Assert
            IList<AppointmentEntity> appointments = DatabaseTools.GetAllAppointments();
            appointments.OrderBy(x => x.Date);
            Assert.That(appointments.Count, Is.EqualTo(2));
            Assert.AreEqual(appointments.First().Date.Date, olderAppointmentDate.Date);
            Assert.AreEqual(appointments.Last().Date.Date, newerAppointmentDate.Date);

            Assert.AreEqual(appointments.First().Patient.Id, appointments.Last().Patient.Id);
        }
        #endregion Tests
    }
}
