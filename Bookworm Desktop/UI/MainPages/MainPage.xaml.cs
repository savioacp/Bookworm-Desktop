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
using Bookworm_Desktop.UI.Dialogs;

namespace Bookworm_Desktop.UI.MainPages
{
	/// <summary>
	/// Interação lógica para MainPage.xam
	/// </summary>
	public partial class MainPage : UserControl
	{

		public MainPage()
		{
			InitializeComponent();
            if (StateRepository.loggedInUser.Get().tblCargo?.NivelAcesso > 1)
                MenuButtonFuncionários.Visibility = Visibility.Collapsed;
			StateRepository.currentView.Listen(v =>
			{
				MainContent.Content = v;
			});
        }
	}
}
