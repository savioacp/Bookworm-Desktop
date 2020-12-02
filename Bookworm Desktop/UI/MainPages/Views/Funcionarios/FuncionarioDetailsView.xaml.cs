using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Bookworm_Desktop.UI.MainPages.Views.Funcionarios
{
    /// <summary>
    /// Interação lógica para FuncionarioDetailsView.xam
    /// </summary>
    public partial class FuncionarioDetailsView : UserControl
    {
        private tblFuncionario _currentFuncionario;
        private FuncionarioEditView.EditContext _context;
        public FuncionarioDetailsView(tblFuncionario funcionario, FuncionarioEditView.EditContext ctx)
        {
            _context = ctx;

            InitializeComponent();

            txtNome.Text = funcionario.Nome;
            txtEmail.Text = funcionario.Email;
            txtCPF.Text = funcionario.CPF.ApplyCPFMask();
            txtCargo.Text = funcionario.tblCargo.NomeCargo;
            txtEndereço.Text = funcionario.Endereco;
            txtRG.Text = funcionario.RG.ApplyRGMask();
            txtTel.Text = funcionario.Telefone.ApplyTelMask();
            txtID.Text = $"ID: {funcionario.IDFuncionario}";

            _currentFuncionario = funcionario;
            var converter = new ByteToImageConverter();

            imgFuncionario.Source = (ImageSource)converter.Convert(funcionario.ImagemFunc, typeof(ImageSource), null, null);

            if (_context == FuncionarioEditView.EditContext.Creating)
                txtHeader.Text = "Vamos adcionar este novo membro na nossa equipe! Essas informações estão corretas?";
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            StateRepository.currentView.Set(new FuncionariosView());
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
            StateRepository.currentView.Set(new FuncionarioEditView(_currentFuncionario, FuncionarioEditView.EditContext.Editing));
        }
    }
}
