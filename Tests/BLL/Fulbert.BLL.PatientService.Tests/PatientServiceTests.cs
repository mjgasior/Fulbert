using Fulbert.BLL.PatientService.Abstract;
using Fulbert.Tests.Common;

namespace Fulbert.BLL.PatientService.Tests
{ 
    public class PatientServiceTests : BaseTest
    {
        private IPatientService _patientService;

        public override void Initialize()
        {
            _patientService = new PatientService();
        }
    }
}
