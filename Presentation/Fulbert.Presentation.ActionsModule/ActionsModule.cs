using Microsoft.Practices.Unity;
using Fulbert.Infrastructure.Concrete.Mvvm;
using Fulbert.Commons.Abstract.BLL;
using Fulbert.BLL.Services.Services;
using Fulbert.Commons.Abstract.DAL;
using Fulbert.DAL.PatientDAL;

namespace Fulbert.Presentation.ActionsModule
{
    public class ActionsModule : BaseModule
    {
        public override void Initialization()
        {
            
        }

        public override void TypeRegistration()
        {
            Container.RegisterType<IPatientDal, PatientDal>();
            Container.RegisterType<IPatientService, PatientService>();
        }
    }
}
