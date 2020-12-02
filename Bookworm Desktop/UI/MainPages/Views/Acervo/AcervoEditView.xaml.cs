using Bookworm_Desktop.UI.MainPages.Views.Funcionarios;
using Microsoft.Win32;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Bookworm_Desktop.UI.MainPages.Views.Acervo
{
    /// <summary>
    /// Interação lógica para AcervoEditView.xam
    /// </summary>
    public partial class AcervoEditView : UserControl
    {
        private tblProduto _produtoAtual;
        private FuncionarioEditView.EditContext _context;
        public AcervoEditView(tblProduto produto, FuncionarioEditView.EditContext ctx)
        {
            _produtoAtual = produto;
            _context = ctx;

            InitializeComponent();

            if (ctx == FuncionarioEditView.EditContext.Creating)
            {

            }
            else
            {
                txtID.Text = $"ID {_produtoAtual.IDProduto}";
                txtEditora.Text = _produtoAtual.Editora;
                txtAno.Text = $"{_produtoAtual.AnoEdicao?.Year}";
                txtGêneros.Text = string.Join(",", _produtoAtual.tblGeneroProduto.Select(g => g.tblGenero.NomeGenero));
                txtSinopse.Text = _produtoAtual.DescricaoProd;
                txtAutor.Text = _produtoAtual.AutoresLivro;
                txtTítulo.Text = _produtoAtual.NomeLivro;
                txtSetor.Text = $"{_produtoAtual.Setor}";

                var converter = new ByteToImageConverter();

                imgProduto.Source = (ImageSource)converter.Convert(_produtoAtual.ImagemProd, typeof(ImageSource), null, null);

            }

        }

        public void GoBack(object sender, RoutedEventArgs e)
        {
            StateRepository.currentView.Set(new AcervoView());
        }

        public void ChooseImage(object sender, RoutedEventArgs e)
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
                imgProduto.Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }

        public void Confirm(object sender, RoutedEventArgs e)
        {
            using var db = new TCCFEntities();

            if (_context == FuncionarioEditView.EditContext.Editing)
            {

                var toEdit = db.tblProduto.First(p => p.IDProduto == _produtoAtual.IDProduto);

                toEdit.Editora = txtEditora.Text;
                toEdit.AnoEdicao = new DateTime(int.Parse(txtAno.Text), 1, 1);
                toEdit.DescricaoProd = txtSinopse.Text;
                toEdit.AutoresLivro = txtAutor.Text;
                toEdit.NomeLivro = txtTítulo.Text;
                toEdit.Setor = int.Parse(txtSetor.Text);
                toEdit.Fileira = int.Parse(txtFileira.Text);
                toEdit.Prateleira = int.Parse(txtPrateleira.Text);

                toEdit.ImagemProd = (byte[])new ByteToImageConverter().ConvertBack(imgProduto.Source, typeof(byte[]),
                    null, CultureInfo.CurrentCulture);

                var genStrings = txtGêneros.Text.Split(new[] { "," }, 0).Select(s => s.Trim()).ToArray();

                db.tblGeneroProduto.RemoveRange(db.tblGeneroProduto.Where(gp => gp.IDProduto == _produtoAtual.IDProduto));

                foreach (var genString in genStrings)
                {
                    tblGenero toBind;
                    if (!db.tblGenero.Any(g => g.NomeGenero == genString))
                        toBind = db.tblGenero.Add(new tblGenero()
                        {
                            NomeGenero = genString
                        });
                    else
                        toBind = db.tblGenero.First(g => g.NomeGenero == genString);
                    db.tblGeneroProduto.Add(new tblGeneroProduto()
                    {
                        tblGenero = toBind,
                        tblProduto = toEdit
                    });
                }

                db.SaveChanges();
                StateRepository.currentView.Set(new AcervoDetailsView(toEdit));
            }
            else
            {
                var toAdd = new tblProduto()
                {
                    AnoEdicao = new DateTime(int.Parse(txtAno.Text), 1, 1),
                    DescricaoProd = txtSinopse.Text,
                    AutoresLivro = txtAutor.Text,
                    NomeLivro = txtTítulo.Text,
                    Setor = int.Parse(txtSetor.Text),
                    Fileira = int.Parse(txtFileira.Text),
                    Prateleira = int.Parse(txtPrateleira.Text),
                    Editora = txtEditora.Text,
                    ImagemProd = (byte[])new ByteToImageConverter().ConvertBack(imgProduto.Source, typeof(byte[]), null, CultureInfo.CurrentCulture)
                };

                db.tblProduto.Add(toAdd);

                var genStrings = txtGêneros.Text.Split(new[] { ", " }, 0);

                foreach (var genString in genStrings)
                {
                    tblGenero toBind;
                    if (!db.tblGenero.Any(g => g.NomeGenero == genString))
                        toBind = db.tblGenero.Add(new tblGenero()
                        {
                            NomeGenero = genString
                        });
                    else
                        toBind = db.tblGenero.First(g => g.NomeGenero == genString);
                    db.tblGeneroProduto.Add(new tblGeneroProduto()
                    {
                        tblGenero = toBind,
                        tblProduto = toAdd
                    });
                }
                db.SaveChanges();
                StateRepository.currentView.Set(new AcervoDetailsView(toAdd));
            }

        }
    }
}
