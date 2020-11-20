using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
