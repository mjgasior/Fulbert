using Prism.Interactivity.InteractionRequest;
using System.Windows;
using System;
using System.Windows.Controls;

namespace Fulbert.Infrastructure.Concrete.Interactions
{
    public class NotificationContent : Control, IInteractionRequestAware
    {
        private const string CLOSE_BUTTON_NAME = "PART_OkButton";
        private const string CONTENT_LABEL_NAME = "PART_Content";
        private const string TITLE_LABEL_NAME = "PART_Title";

        public Action FinishInteraction { get; set; }
        public INotification Notification { get; set; }

        public NotificationContent()
        {
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            GetOkButton().Click += OnOkButtonClick;
            GetContentPresenter().Content = Notification.Content;
            GetTitlePresenter().Content = Notification.Title;
        }

        private Label GetContentPresenter()
        {
            return (Template.FindName(CONTENT_LABEL_NAME, this) as Label);
        }

        private Label GetTitlePresenter()
        {
            return (Template.FindName(TITLE_LABEL_NAME, this) as Label);
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
