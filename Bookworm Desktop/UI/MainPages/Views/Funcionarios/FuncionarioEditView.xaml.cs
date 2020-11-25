using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Bookworm_Desktop.Services;
using Bookworm_Desktop.UI.Extensions;
using Microsoft.Win32;

namespace Bookworm_Desktop.UI.MainPages.Views.Funcionarios
{
    /// <summary>
    /// Interação lógica para FuncionarioEditView.xam
    /// </summary>
    public partial class FuncionarioEditView : UserControl
    {
        public enum EditContext
        {
            Creating,
            Editing
        }

        private readonly EditContext _context;
        private readonly tblFuncionario _currentFuncionario;
        public FuncionarioEditView(tblFuncionario funcionario, EditContext ctx)
        {
            _currentFuncionario = funcionario;
            _context = ctx;

            InitializeComponent();


            using var db = new TCCFEntities();
            var cargos = db.tblCargo.Select(c => c.NomeCargo).ToList();

            foreach (var cargo in cargos)
                cbxCargo.Items.Add(cargo);

            if (_context == EditContext.Creating)
                txtHeader.Text = "Vamos adcionar este novo membro na nossa equipe!";
            else
            {
                txtNome.Text = _currentFuncionario.Nome;
                txtEmail.Text = _currentFuncionario.Email;
                txtCPF.Text = _currentFuncionario.CPF;
                txtEndereço.Text = _currentFuncionario.Endereco;
                txtRG.Text = _currentFuncionario.RG;
                txtTel.Text = _currentFuncionario.Telefone;
                txtID.Text = $"ID: {_currentFuncionario.IDFuncionario}";

                cbxCargo.Text = _currentFuncionario.tblCargo.NomeCargo;

                if (_currentFuncionario.IDFuncionario != StateRepository.loggedInUser.Get().IDFuncionario)
                {
                    StackPanel panelSenha = (StackPanel)txtSenha.Parent;
                    panelSenha.Visibility = Visibility.Collapsed;

                    StackPanel panelConfirmarSenha = (StackPanel)txtConfirmarSenha.Parent;
                    panelConfirmarSenha.Visibility = Visibility.Collapsed;
                }


            }
            var converter = new ByteToImageConverter();
            if (_currentFuncionario.ImagemFunc != null)
                if(_currentFuncionario.ImagemFunc != new byte[] { 0x00 })
                    imgFuncionario.Source = (ImageSource)converter.Convert(_currentFuncionario.ImagemFunc, typeof(ImageSource), null, null);

        }

        public void GoBack_Click(object sender, RoutedEventArgs e) => GoBack(sender, e);
        public void GoBack(object _, RoutedEventArgs e, tblFuncionario func = null)
        {
            if (_context == EditContext.Editing)
                StateRepository.currentView.Set(new FuncionarioDetailsView(func ?? _currentFuncionario, _context));
            else
                StateRepository.currentView.Set(new FuncionariosView());
        }

        public void ChooseImage(object _, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Imagens |*.png;*.jpg",
                CheckFileExists = true,
                Title = "Escolha uma imagem",
                Multiselect = false,
            };

            if (ofd.ShowDialog() == true)
            {
                imgFuncionario.Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }

        private void ConfirmCreate()
        {
            var textContainer = (WrapPanel)txtNome.FindCommonVisualAncestor(txtCPF);

            foreach (var _panel in textContainer.Children)
                if (_panel is StackPanel panel)
                    foreach (var child in panel.Children)
                        if (child is TextBox tbox)
                        {
                            if (tbox.Text == "")
                            {
                                MessageBox.Show("Todos os campos devem estar corretamente preenchidos.");
                                return;
                            }
                        }
                        else if (child is PasswordBox pbox)
                        {
                            if (pbox.Password == "")
                            {
                                MessageBox.Show("Todos os campos devem estar corretamente preenchidos.");
                                return;
                            }
                        }

            if (txtSenha.Password != txtConfirmarSenha.Password)
            {
                WarnText(txtConfirmarSenha);
                return;
            }


            using var db = new TCCFEntities();

            var (senha, salt) = Authentication.RegisterUser(txtSenha.Password);

            var func = new tblFuncionario()
            {
                Nome = txtNome.Text,
                CPF = txtCPF.StripMask(),
                Email = txtEmail.Text,
                Endereco = txtEndereço.Text,
                RG = txtRG.StripMask(),
                Telefone = txtTel.StripMask(),
                Senha = senha,
                Salt = salt,
                tblCargo = db.tblCargo.First(f => f.NomeCargo == cbxCargo.Text),
                ImagemFunc = (byte[])new ByteToImageConverter().ConvertBack(imgFuncionario.Source, typeof(byte[]), null, CultureInfo.CurrentCulture)
            };
            db.tblFuncionario.Add(func);
            db.SaveChanges();

            func = db.tblFuncionario.Find(func.IDFuncionario);

            StateRepository.currentView.Set(new FuncionarioDetailsView(func, _context));
        }

        private void ConfirmEdit(object sender, RoutedEventArgs e)
        {
            if (txtSenha.Password != "")
                if (txtSenha.Password != txtConfirmarSenha.Password)
                {
                    WarnText(txtConfirmarSenha);
                    return;
                }

            using var db = new TCCFEntities();

            var func = db.tblFuncionario.Include("tblCargo").First(f => f.IDFuncionario == _currentFuncionario.IDFuncionario);

            func.Nome = txtNome.Text;
            func.RG = txtRG.StripMask();
            func.CPF = txtCPF.StripMask();
            func.Email = txtEmail.Text;
            func.Endereco = txtEndereço.Text;
            func.Telefone = txtTel.StripMask();
            func.ImagemFunc = (byte[]) new ByteToImageConverter().ConvertBack(imgFuncionario.Source, typeof(byte[]),
                null, CultureInfo.CurrentCulture);

            db.SaveChanges();

            GoBack(sender, e, func);
        }
        private void Confirm(object sender, RoutedEventArgs e)
        {
            if (_context == EditContext.Editing)
                ConfirmEdit(sender, e);
            else
                ConfirmCreate();
        }

        private void WarnText(IInputElement element)
        {
            var backgroundBefore = ((Style)FindResource("BaseTextStyle"))
                                .Setters.OfType<Setter>()
                                .First(s => s.Property == BackgroundProperty)
                                .Value as Brush;

            void Handler(object sender, EventArgs e)
            {
                var senderControl = (Control)sender;

                senderControl.Background = backgroundBefore;
                senderControl.GotFocus -= Handler;

                txtPasswordError.BeginAnimation(HeightProperty, new DoubleAnimation(0, new Duration(new TimeSpan(0, 0, 0, 0, 200))));
            }

            element.KeyDown += Handler;

            txtPasswordError.BeginAnimation(HeightProperty, new DoubleAnimation(15, new Duration(new TimeSpan(0, 0, 0, 0, 200))));
        }
    }
}
