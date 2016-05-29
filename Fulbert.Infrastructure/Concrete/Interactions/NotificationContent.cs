using Prism.Interactivity.InteractionRequest;
using System.Windows;
using System;
using System.Windows.Controls;

namespace Fulbert.Infrastructure.Concrete.Interactions
{
    public class NotificationContent : Control, IInteractionRequestAware
    {
        private const string CLOSE_BUTTON_NAME = "PART_OkButton";

        public Action FinishInteraction { get; set; }
        public INotification Notification { get; set; }

        public NotificationContent()
        {
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            GetOkButton().Click += OnOkButtonClick;
        }

        private Button GetOkButton()
        {
            return (Template.FindName(CLOSE_BUTTON_NAME, this) as Button);
        }

        private void OnOkButtonClick(object sender, RoutedEventArgs e)
        {
            FinishInteraction();
        }
    }
}
