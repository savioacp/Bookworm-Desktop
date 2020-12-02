using Bookworm_Desktop.UI.MainPages.Views.Funcionarios;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Bookworm_Desktop.UI.MainPages.Views.Clientes
{
    /// <summary>
    /// Interação lógica para FuncionarioDetailsView.xam
    /// </summary>
    public partial class ClienteDetailsView : UserControl
    {
        private tblLeitor _currentLeitor;
        private FuncionarioEditView.EditContext _context;
        public ClienteDetailsView(tblLeitor leitor, FuncionarioEditView.EditContext ctx)
        {
            _context = ctx;

            InitializeComponent();

            txtNome.Text = leitor.Nome;
            txtEmail.Text = leitor.Email;
            txtCPF.Text = leitor.CPF.ApplyCPFMask();
            txtCargo.Text = leitor.tblTipoLeitor.TipoLeitor;
            txtEndereço.Text = leitor.Endereco;
            txtRG.Text = leitor.RG.ApplyRGMask();
            txtTel.Text = leitor.Telefone.ApplyTelMask();
            txtID.Text = $"ID: {leitor.IDLeitor}";

            _currentLeitor = leitor;
            var converter = new ByteToImageConverter();

            imgFuncionario.Source = (ImageSource)converter.Convert(leitor.ImagemLeitor, typeof(ImageSource), null, null);

            if (_context == FuncionarioEditView.EditContext.Creating)
                txtHeader.Text = "Vamos adcionar este novo membro na nossa equipe! Essas informações estão corretas?";
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            StateRepository.currentView.Set(new ClientesView());
        }
        private void GoBack(object sender, RoutedEventArgs e)
        {
            if (_context == FuncionarioEditView.EditContext.Editing)
                Confirm(sender, e);
            else
                EditClick(sender, e);
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            StateRepository.currentView.Set(new ClienteEditView(_currentLeitor, FuncionarioEditView.EditContext.Editing));
        }
    }
}
