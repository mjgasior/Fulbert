using Fulbert.Infrastructure.Concrete.Mvvm;
using Fulbert.ViewModels;

namespace Fulbert
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : BaseWindowView
    {
        public Shell(IShellViewModel shellViewModel)
            : base(shellViewModel)
        {
            InitializeComponent();
        }
    }
}
