using Fulbert.BLL.Services.Services;
using Fulbert.Commons.Models.Business;
using Entity = Fulbert.Commons.Models.Entities;
using Fulbert.DAL.PatientDAL;
using Fulbert.Tests.Common;
using NHibernate;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Fulbert.BLL.Services.Tests.Services
{
    public class PatientServiceIntegration : BaseServiceTest
    {
        private PatientService _patientService;

        public override void Initialize()
        {
            var patientDal = new PatientDal(Database.TEST_DB_NAME);
            _patientService = new PatientService(patientDal);
        }

        #region Tests
        [Test]
        public void Add_new_patient()
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
            IList<Entity.Patient> patients;
            using (ISession session = sessionForTests.OpenSession())
            {
                patients = session.QueryOver<Entity.Patient>().Fetch(x => x.Appointments).Eager.List();
            }

            Assert.IsNotNull(patients);
            Assert.IsNotEmpty(patients);
            Assert.IsTrue(patients.Count == 1);

            Entity.Patient result = patients.First();
            StringAssert.Contains(result.FirstName, firstName);
            StringAssert.Contains(result.LastName, lastName);
        }
        #endregion Tests
    }
}
