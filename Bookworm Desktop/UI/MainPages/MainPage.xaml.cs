using System.Windows;
using System.Windows.Controls;

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
