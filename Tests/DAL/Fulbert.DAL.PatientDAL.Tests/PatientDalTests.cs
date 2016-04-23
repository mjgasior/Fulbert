using Fulbert.DAL.PatientDAL.Models;
using NHibernate;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;


namespace Fulbert.DAL.PatientDAL.Tests
{
    public class PatientDalTests : BaseTest
    {
        private const string TEST_DB_NAME = "AAV";
        private PatientDal _patientDal;

        public override void Initialize()
        {
            _patientDal = new PatientDal(TEST_DB_NAME);
        }

        [Test]
        public void Add_patient_to_database()
        {
            // Arrange
            string firstName = "Peter";
            string lastName = "Steele";

            // Act
            _patientDal.AddPatient(new Patient
            {
                FirstName = firstName,
                LastName = lastName
            });

            IList<Patient> query = GetPatientFromDatabase(firstName, lastName);

            // Assert
            Assert.IsNotNull(query);
            Assert.IsNotEmpty(query);
            Assert.IsTrue(query.Count == 1);

            Patient result = query.First();
            StringAssert.Contains(result.FirstName, firstName);
            StringAssert.Contains(result.LastName, lastName);
        }

        [Test]
        public void Get_all_patients_from_database()
        {
            // Arrange
            string firstName = "Johnny";
            string lastName = "Kelly";

            AddPatientToDatabase(firstName, lastName);
            
            // Act
            IEnumerable<Patient> patients = _patientDal.GetAllPatients();

            // Assert
            Assert.IsNotNull(patients);
            Assert.IsNotEmpty(patients);
            Assert.IsTrue(patients.Count() == 1);

            Patient result = patients.First();
            StringAssert.Contains(result.FirstName, firstName);
            StringAssert.Contains(result.LastName, lastName);
        }

        [Test]
        public void Delete_patient_from_database()
        {
            // Arrange
            string firstName = "Josh";
            string lastName = "Silver";

            AddPatientToDatabase(firstName, lastName);
            IList<Patient> patients = GetPatientFromDatabase(firstName, lastName);

            // Act
            _patientDal.DeletePatient(patients.First());

            // Assert
            IList<Patient> patientsAfterDelete = GetPatientFromDatabase(firstName, lastName);
            Assert.IsNotNull(patientsAfterDelete);
            Assert.IsEmpty(patientsAfterDelete);
        }

        #region Methods
        private IList<Patient> GetPatientFromDatabase(string firstName, string lastName)
        {
            ISessionFactory sessionForTests = NHibernateConfig.CreateSessionFactory(TEST_DB_NAME);
            using (ISession session = sessionForTests.OpenSession())
            {
                return session.QueryOver<Patient>()
                    .Where(k => k.FirstName == firstName && k.LastName == lastName).List();
            }
        }

        private void AddPatientToDatabase(string firstName, string lastName)
        {
            Patient patient = new Patient
            {
                FirstName = firstName,
                LastName = lastName
            };

            ISessionFactory sessionForTests = NHibernateConfig.CreateSessionFactory(TEST_DB_NAME);
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
