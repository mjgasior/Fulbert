using System.Windows;
using System.Windows.Controls;

namespace Fulbert.Presentation.Controls.Extended
{
    public class HeaderTextBox : TextBox
    {
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header",
            typeof(string), typeof(HeaderTextBox), new PropertyMetadata(string.Empty));
    }
}
