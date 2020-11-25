using System.Linq;
using Bookworm_Desktop.Services;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Bookworm_Desktop.UI.LoginPages
{
    /// <summary>
    /// Interação lógica para Login2.xam
    /// </summary>
    public partial class Login2 : UserControl
    {

        public Login2()
        {
            InitializeComponent();
            StateRepository.loggedInUser.Listen(currentUser =>
            {
                txtUsername.Text = currentUser.Nome;
                imgEmerson.Fill = new ImageBrush((ImageSource)new ByteToImageConverter().Convert(currentUser.ImagemFunc, null, null, null))
                {
                    Stretch = Stretch.UniformToFill,
                    TileMode = TileMode.Tile
                };
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            button.IsEnabled = false;
            button.Cursor = Cursors.No;

            if (StateRepository.loggedInUser.Get().Senha.Equals(Authentication.GetHash(txtSenha.Password, StateRepository.loggedInUser.Get().Salt)))
                ((Storyboard)FindResource("AfterLoginStoryboard")).Begin();
            else
            {
                MessageBox.Show("Verifique suas credenciais e tente novamente.", "Senha inválida.", MessageBoxButton.OK, MessageBoxImage.Error);

                txtSenha.Password = "";
                button.IsEnabled = true;
                button.Cursor = Cursors.Hand;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Task.Run(async () =>
            {
                await Task.Delay(600);
                Dispatcher.Invoke(() => StateRepository.loggedInUser.Set(new tblFuncionario { Nome = "none" }));
            });
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            txtPlaceholder.Visibility = txtSenha.Password == "" ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
