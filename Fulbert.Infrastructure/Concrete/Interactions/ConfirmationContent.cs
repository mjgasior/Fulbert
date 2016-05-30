using Prism.Interactivity.InteractionRequest;
using System.Windows;
using System;
using System.Windows.Controls;

namespace Fulbert.Infrastructure.Concrete.Interactions
{
    public class ConfirmationContent : Control, IInteractionRequestAware
    {
        private const string OK_BUTTON_NAME = "PART_OkButton";
        private const string CANCEL_BUTTON_NAME = "PART_CancelButton";

        public Action FinishInteraction { get; set; }
        public INotification Notification { get; set; }

        public ConfirmationContent()
        {
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            GetButton(OK_BUTTON_NAME).Click += OnOkButtonClick;
            GetButton(CANCEL_BUTTON_NAME).Click += OnCancelButtonClick;
        }

        private Button GetButton(string name)
        {
            return (Template.FindName(name, this) as Button);
        }

        private void OnOkButtonClick(object sender, RoutedEventArgs e)
        {
            (Notification as IConfirmation).Confirmed = true;
            FinishInteraction();
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            (Notification as IConfirmation).Confirmed = false;
            FinishInteraction();
        }
    }
}
