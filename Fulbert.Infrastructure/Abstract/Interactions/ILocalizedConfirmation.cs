using Prism.Interactivity.InteractionRequest;

namespace Fulbert.Infrastructure.Abstract.Interactions
{
    public interface ILocalizedConfirmation : IConfirmation
    {
        string ConfirmationMessage { get; }
        string CancellationMessage { get; }
    }
}
