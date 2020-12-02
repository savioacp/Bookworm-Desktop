using Bookworm_Desktop.UI.Dialogs;
using Bookworm_Desktop.UI.MainPages.Views.Funcionarios;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Bookworm_Desktop.UI.MainPages.Views.Acervo
{
    /// <summary>
    /// Interação lógica para AcervoDetailsView.xam
    /// </summary>
    public partial class AcervoDetailsView : UserControl
    {
        private tblProduto _produtoAtual;

        private Image imgBorrow;
        public AcervoDetailsView(tblProduto produto)
        {
            InitializeComponent();

            using var db = new TCCFEntities();
            _produtoAtual = db.tblProduto.Include("tblGeneroProduto").Include("tblGeneroProduto.tblGenero").First(p => p.IDProduto == produto.IDProduto);

            txtTítulo.Text = _produtoAtual.NomeLivro;
            txtGêneros.Text = String.Join(", ", _produtoAtual.tblGeneroProduto.Select(g => g.tblGenero.NomeGenero).ToList());
            txtAutor.Text = _produtoAtual.AutoresLivro;
            txtSinopse.Text = _produtoAtual.DescricaoProd;
            txtID.Text = $"Código {_produtoAtual.IDProduto}";
            txtEditora.Text = _produtoAtual.Editora + " " + _produtoAtual.AnoEdicao?.Year;

            txtPrateleira.Text = $"{_produtoAtual.Prateleira}";
            txtFileira.Text = $"{_produtoAtual.Fileira}";
            txtSetor.Text = $"{_produtoAtual.Setor}";

            //imgBorrow = (Image)btnBorrow.Content;

            //if (db.tblEmprestimo.Any(e => e.IDProduto == _produtoAtual.IDProduto))
            //{
            //    btnBorrow.Background = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#152B33"));
            //    imgBorrow.RenderTransform= new ScaleTransform(0.8,0.8,28,28);
            //}

            txtCount.Text = $"Exemplares Disponíveis: {db.tblProduto.Count(p => p.ISBN == _produtoAtual.ISBN)}";

            var converter = new ByteToImageConverter();

            imgProduto.Source =
                (ImageSource)converter.Convert(_produtoAtual.ImagemProd, typeof(ImageSource), null, null);
        }

        public void GoBack(object sender, RoutedEventArgs e)
        {
            StateRepository.currentView.Set(new AcervoView());
        }

        public void EditClick(object sender, RoutedEventArgs e)
        {
            StateRepository.currentView.Set(new AcervoEditView(_produtoAtual, FuncionarioEditView.EditContext.Editing));
        }
        public void Delete(object sender, RoutedEventArgs e)
        {
            if (ConfirmDialog.Show("Você tem certeza que quer deletar " + _produtoAtual.NomeLivro))
            {
                using var db = new TCCFEntities();



                var prodToDelete = db.tblProduto.First(p => p.IDProduto == _produtoAtual.IDProduto);

                db.tblFavoritos.RemoveRange(prodToDelete.tblFavoritos);
                db.tblReserva.RemoveRange(prodToDelete.tblReserva);
                db.tblGeneroProduto.RemoveRange(prodToDelete.tblGeneroProduto);
                db.tblEmprestimo.RemoveRange(prodToDelete.tblEmprestimo);

                db.tblProduto.Remove(prodToDelete);

                db.SaveChanges();

                StateRepository.currentView.Set(new AcervoView());
            }
        }


        //public void Borrow(object sender, RoutedEventArgs e)
        //{
        //    using var db = new TCCFEntities();
        //    if (!db.tblEmprestimo.Any(e => e.IDProduto == _produtoAtual.IDProduto))
        //    {
        //        btnBorrow.Background = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#152B33"));
        //        imgBorrow.RenderTransform = new ScaleTransform(0.8, 0.8, 28, 28);

        //        db.tblEmprestimo.Add(new tblEmprestimo()
        //        {
        //            DataRetirada = DateTime.Now,
        //            IDFuncionario = StateRepository.loggedInUser.Get().IDFuncionario,
        //            IDProduto = _produtoAtual.IDProduto,
        //            Renovacao = 0,
        //            tblLeitor = db.tblLeitor.First(l => l.Nome.Contains("Sávio"))
        //        });
        //    }
        //    else
        //    {
        //        btnBorrow.Background = new SolidColorBrush(Colors.Transparent);
        //        imgBorrow.RenderTransform = Transform.Identity;

        //        db.tblEmprestimo.RemoveRange(db.tblEmprestimo.Where(e => e.IDProduto == _produtoAtual.IDProduto));
        //    }

        //    db.SaveChanges();
        //}
    }
}
