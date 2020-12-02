using System.Windows;
using System.Windows.Controls;

namespace Bookworm_Desktop.UI.Components
{
    /// <summary>
    /// Interação lógica para SearchTextBox.xam
    /// </summary>
    public partial class SearchTextBox : UserControl
    {

        public string Text { get => txtTerm.Text; set => txtTerm.Text = value; }
        public string Placeholder { get => txtPlaceholder.Text; set => txtPlaceholder.Text = value; }

        public RoutedEventHandler DoSearch { get; set; } = (s, e) => { };

        public SearchTextBox()
        {
            InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            DoSearch(sender, e);
        }

    }
}
