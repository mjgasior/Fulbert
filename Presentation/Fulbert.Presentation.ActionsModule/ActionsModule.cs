using Microsoft.Practices.Unity;
using Fulbert.Infrastructure.Concrete.Mvvm;
using Fulbert.BLL.Services.Services;
using Fulbert.BLL.ApplicationModels.Abstract;
using Fulbert.DAL.PatientDAL;
using Fulbert.DAL.RepositoryModels.Abstract;

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
