using Bookworm_Desktop.Services;
using Bookworm_Desktop.UI.Extensions;
using Bookworm_Desktop.UI.MainPages.Views.Funcionarios;
using Microsoft.Win32;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Bookworm_Desktop.UI.MainPages.Views.Clientes
{
    /// <summary>
    /// Interação lógica para ClienteEditView.xam
    /// </summary>
    public partial class ClienteEditView : UserControl
    {
        private readonly FuncionarioEditView.EditContext _context;
        private readonly tblLeitor _currentLeitor;
        public ClienteEditView(tblLeitor leitor, FuncionarioEditView.EditContext ctx)
        {
            _currentLeitor = leitor;
            _context = ctx;

            InitializeComponent();

            using var db = new TCCFEntities();
            var tiposLeitor = db.tblTipoLeitor.Select(t => t.TipoLeitor).ToList();

            foreach (var tipo in tiposLeitor)
                cbxCargo.Items.Add(tipo);

            if (_context == FuncionarioEditView.EditContext.Creating)
                txtHeader.Text = "Vamos adicionar este leitor na nossa equipe";
            else
            {
                txtNome.Text = _currentLeitor.Nome;
                txtEmail.Text = _currentLeitor.Email;
                txtCPF.Text = _currentLeitor.CPF;
                txtEndereço.Text = _currentLeitor.Endereco;
                txtRG.Text = _currentLeitor.RG;
                txtTel.Text = _currentLeitor.Telefone;
                txtID.Text = $"ID: {_currentLeitor.IDLeitor}";

                cbxCargo.Text = _currentLeitor.tblTipoLeitor.TipoLeitor;

                if (_currentLeitor.IDLeitor != StateRepository.loggedInUser.Get().IDFuncionario)
                {
                    StackPanel panelSenha = (StackPanel)txtSenha.Parent;
                    panelSenha.Visibility = Visibility.Collapsed;

                    StackPanel panelConfirmarSenha = (StackPanel)txtConfirmarSenha.Parent;
                    panelConfirmarSenha.Visibility = Visibility.Collapsed;
                }

                var converter = new ByteToImageConverter();

                if (_currentLeitor.ImagemLeitor != null)
                    if (_currentLeitor.ImagemLeitor != new byte[] { 0x00 })
                        imgFuncionario.Source =
                            (ImageSource)converter.Convert(_currentLeitor.ImagemLeitor, typeof(ImageSource), null,
                                null);
            }

        }

        public void GoBack_Click(object sender, RoutedEventArgs e) => GoBack(sender, e);
        public void GoBack(object _, RoutedEventArgs e, tblLeitor leitor = null)
        {
            if (_context == FuncionarioEditView.EditContext.Editing)
                StateRepository.currentView.Set(new ClienteDetailsView(leitor ?? _currentLeitor, _context));
            else
                StateRepository.currentView.Set(new ClientesView());
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

            var leitor = new tblLeitor()
            {
                Nome = txtNome.Text,
                CPF = txtCPF.StripMask(),
                Email = txtEmail.Text,
                Endereco = txtEndereço.Text,
                RG = txtRG.StripMask(),
                Telefone = txtTel.StripMask(),
                Senha = senha,
                Salt = salt,
                tblTipoLeitor = db.tblTipoLeitor.First(f => f.TipoLeitor == cbxCargo.Text),
                ImagemLeitor = (byte[])new ByteToImageConverter().ConvertBack(imgFuncionario.Source, typeof(byte[]), null, CultureInfo.CurrentCulture)
            };
            db.tblLeitor.Add(leitor);
            db.SaveChanges();

            leitor = db.tblLeitor.Find(leitor.IDLeitor);

            StateRepository.currentView.Set(new ClienteDetailsView(leitor, _context));
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

            var leitor = db.tblLeitor.Include("tblTipoLeitor").First(l => l.IDLeitor == _currentLeitor.IDLeitor);

            leitor.Nome = txtNome.Text;
            leitor.RG = txtRG.StripMask();
            leitor.CPF = txtCPF.StripMask();
            leitor.Email = txtEmail.Text;
            leitor.Endereco = txtEndereço.Text;
            leitor.Telefone = txtTel.StripMask();
            leitor.ImagemLeitor = (byte[])new ByteToImageConverter().ConvertBack(imgFuncionario.Source, typeof(byte[]),
                null, CultureInfo.CurrentCulture);

            db.SaveChanges();

            GoBack(sender, e, leitor);
        }
        private void Confirm(object sender, RoutedEventArgs e)
        {
            if (_context == FuncionarioEditView.EditContext.Editing)
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

        private void ChooseImage(object _, RoutedEventArgs e)
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
    }
}
