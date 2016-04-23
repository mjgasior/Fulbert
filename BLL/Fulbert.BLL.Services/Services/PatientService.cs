using Fulbert.Commons.Abstract.BLL;
using Fulbert.Commons.Abstract.DAL;
using Fulbert.Commons.Models.Business;
using Entity = Fulbert.Commons.Models.Entities;

namespace Fulbert.BLL.Services.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientDal _patientDal;

        public PatientService(IPatientDal patientDal)
        {
            _patientDal = patientDal;
        }

        public void AddNewPatient(Patient patient)
        {
            Entity.Patient patientEntity = CreateEntity(patient);
            _patientDal.SaveOrUpdatePatient(patientEntity);
        }

        private Entity.Patient CreateEntity(Patient patient)
        {
            return new Entity.Patient
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName
            };
        }
    }
}
