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

namespace Bookworm_Desktop.UI.MainPages.Views.Funcionarios
{
    /// <summary>
    /// Interação lógica para FuncionarioDetailsView.xam
    /// </summary>
    public partial class FuncionarioDetailsView : UserControl
    {
        public FuncionarioDetailsView(tblFuncionario funcionario)
        {
            InitializeComponent();

            txtNome.Text = funcionario.Nome;
            txtEmail.Text = funcionario.Email;
            txtCPF.Text = funcionario.CPF;
            txtCargo.Text = funcionario.tblCargo.NomeCargo;
            txtEndereço.Text = funcionario.Endereco;
            txtRG.Text = funcionario.RG;
            txtTel.Text = funcionario.Telefone;
            txtID.Text = $"ID: {funcionario.IDFuncionario}";

            var converter = new ByteToImageConverter();

            imgFuncionario.Source = (ImageSource) converter.Convert(funcionario.ImagemFunc, typeof(ImageSource), null,
                null);
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            StateRepository.currentView.Set(new FuncionariosView());
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
