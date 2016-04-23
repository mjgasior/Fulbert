using NUnit.Framework;
using Rhino.Mocks;
using Fulbert.BLL.Services.Services;
using Fulbert.Commons.Abstract.BLL;
using Fulbert.Commons.Abstract.DAL;
using Entity = Fulbert.Commons.Models.Entities;
using Fulbert.Commons.Models.Business;
using System;

namespace Fulbert.BLL.Services.Tests.Services
{
    public class PatientServiceTests : BaseServiceTest
    {
        private IPatientService _patientService;
        private IPatientDal _patientDalMock;

        public override void Initialize()
        {
            _patientDalMock = MockRepository.GenerateMock<IPatientDal>();
            _patientService = new PatientService(_patientDalMock);
        }

        [Test]
        public void Add_new_patient()
        {
            // Arrange
            _patientDalMock.Stub(x => x.SaveOrUpdatePatient(Arg<Entity.Patient>.Is.Anything)).Repeat.Once();

            Patient patient = new Patient
            {
                FirstName = "Dave",
                LastName = "Grohl"
            };

            // Act
            _patientService.AddNewPatient(patient);

            // Assert
            _patientDalMock.VerifyAllExpectations();
        }

        [Test]
        public void Add_appointment_to_patient()
        {
            // Arrange
            Guid patientId = Guid.NewGuid();

            var appointment = new Appointment
            {
                Date = DateTime.Now
            };

            // Act
            _patientService.AddAppointmentToPatient(patientId, appointment);

            // Assert
        }
    }
}
