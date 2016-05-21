namespace Fulbert.Infrastructure.Abstract.Mvvm
{
    public interface IActivableViewModel : IViewModel
    {
        void Activate();
        void Deactivate();
    }
}
