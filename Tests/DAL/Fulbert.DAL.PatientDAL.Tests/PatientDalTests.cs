using Fulbert.Commons.Abstract.DAL;
using Fulbert.Commons.Models.Entities;
using Fulbert.Tests.Common;
using NHibernate;
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
            var patient = new Patient
                          {
                            FirstName = firstName,
                            LastName = lastName,
                          };
            patient.AddAppointment(new Appointment { Date = appointmentDate });
            _patientDal.SaveOrUpdatePatient(patient);

            IList<Patient> query = GetPatientFromDatabase(firstName, lastName);

            // Assert
            Assert.IsNotNull(query);
            Assert.IsNotEmpty(query);
            Assert.IsTrue(query.Count == 1);

            Patient result = query.First();
            StringAssert.Contains(result.FirstName, firstName);
            StringAssert.Contains(result.LastName, lastName);

            ICollection<Appointment> appointments = result.Appointments;
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

            AddPatientToDatabase(firstName, lastName, appointmentDate);
            IList<Patient> patients = GetPatientFromDatabase(firstName, lastName);

            // Act
            _patientDal.DeletePatient(patients.First());

            // Assert
            IList<Patient> patientsAfterDelete = GetPatientFromDatabase(firstName, lastName);
            Assert.IsNotNull(patientsAfterDelete);
            Assert.IsEmpty(patientsAfterDelete);

            IList<Appointment> appointments = GetAllAppointments();
            Assert.IsNotNull(appointments);
            Assert.IsEmpty(appointments);
        }

        [Test]
        public void Get_all_patients_from_database()
        {
            // Arrange
            string firstName = "Johnny";
            string lastName = "Kelly";
            DateTime appointmentDate = DateTime.Now;

            AddPatientToDatabase(firstName, lastName, appointmentDate);
            
            // Act
            IEnumerable<Patient> patients = _patientDal.GetAllPatients();

            // Assert
            Assert.IsNotNull(patients);
            Assert.IsNotEmpty(patients);
            Assert.IsTrue(patients.Count() == 1);

            Patient result = patients.First();
            StringAssert.Contains(result.FirstName, firstName);
            StringAssert.Contains(result.LastName, lastName);

            ICollection<Appointment> appointments = result.Appointments;
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

            AddPatientToDatabase(firstName, lastName, appointmentDate);

            // Act
            IList<Appointment> appointments = _patientDal.GetAllAppointments();

            // Assert
            Assert.IsNotNull(appointments);
            Assert.IsNotEmpty(appointments);
            Assert.IsTrue(appointments.Count() == 1);
            Appointment appointment = appointments.First();
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

            AddPatientToDatabase(firstName, lastName, newerAppointmentDate);

            IEnumerable<Patient> patients = _patientDal.GetAllPatients();
            Patient patient = patients.First(x => x.FirstName == firstName);

            // Act
            patient.AddAppointment(new Appointment { Date = olderAppointmentDate });
            _patientDal.SaveOrUpdatePatient(patient);

            // Assert
            IList<Appointment> appointments = GetAllAppointments();
            appointments.OrderBy(x => x.Date);
            Assert.AreEqual(appointments.Count, 2);
            Assert.AreEqual(appointments.First().Date.Date, olderAppointmentDate.Date);
            Assert.AreEqual(appointments.Last().Date.Date, newerAppointmentDate.Date);

            Assert.AreEqual(appointments.First().Patient.Id, appointments.Last().Patient.Id);
        }
        #endregion Tests

        #region Methods
        private IList<Appointment> GetAllAppointments()
        {
            ISessionFactory sessionForTests = NHibernateConfig.CreateSessionFactory(Database.TEST_DB_NAME);
            using (ISession session = sessionForTests.OpenSession())
            {
                return session.QueryOver<Appointment>().List();
            }
        }

        private IList<Patient> GetPatientFromDatabase(string firstName, string lastName)
        {
            ISessionFactory sessionForTests = NHibernateConfig.CreateSessionFactory(Database.TEST_DB_NAME);
            using (ISession session = sessionForTests.OpenSession())
            {
                return session.QueryOver<Patient>().Fetch(x => x.Appointments).Eager
                    .Where(k => k.FirstName == firstName && k.LastName == lastName).List();
            }
        }

        private void AddPatientToDatabase(string firstName, string lastName, DateTime appointmentDate)
        {
            var appointment = new Appointment
            {
                Date = appointmentDate
            };

            Patient patient = new Patient
            {
                FirstName = firstName,
                LastName = lastName
            };
            patient.AddAppointment(appointment);

            ISessionFactory sessionForTests = NHibernateConfig.CreateSessionFactory(Database.TEST_DB_NAME);
            using (ISession session = sessionForTests.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(patient);
                    transaction.Commit();
                }
            }
        }
        #endregion Methods
    }
}
