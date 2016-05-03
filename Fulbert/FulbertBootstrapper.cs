using System.Windows;
using Prism.Unity;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using System;
using Fulbert.ViewModels;
using Fulbert.Modules.PatientModule;

namespace Fulbert
{
    public class FulbertBootstrapper : UnityBootstrapper
    {
        #region Overrides
        protected override DependencyObject CreateShell()
        {
            Container.RegisterType<IShellViewModel, ShellViewModel>();
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            RegisterModule(typeof(PatientModule));
        }
        #endregion Overrides

        #region Methods
        private void RegisterModule(Type module)
        {
            ModuleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = module.Name,
                ModuleType = module.AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable
            });
        }
        #endregion Methods
    }
}
