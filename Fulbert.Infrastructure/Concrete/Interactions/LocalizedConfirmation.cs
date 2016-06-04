using Fulbert.Infrastructure.Abstract.Interactions;
using Prism.Interactivity.InteractionRequest;

namespace Fulbert.Infrastructure.Concrete.Interactions
{
    public class LocalizedConfirmation : Confirmation, ILocalizedConfirmation
    {
        public string CancellationMessage { get; private set; }
        public string ConfirmationMessage { get; private set; }

        public LocalizedConfirmation(string cancelString, string confrimationString)
        {
            CancellationMessage = cancelString;
            ConfirmationMessage = confrimationString;
        }
    }
}
