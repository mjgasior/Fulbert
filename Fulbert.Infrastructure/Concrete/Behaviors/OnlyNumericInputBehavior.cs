using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Fulbert.Infrastructure.Concrete.Behaviors
{
    public class OnlyNumericInputBehavior : Behavior<TextBoxBase>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PreviewTextInput += OnPreviewTextInput;
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
            base.OnDetaching();
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+");
            return !regex.IsMatch(text);
        }
    }
}
