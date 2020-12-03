using Bookworm_Desktop.UI.Dialogs;
using Bookworm_Desktop.UI.MainPages.Views.Funcionarios;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Bookworm_Desktop.UI.MainPages.Views.Acervo
{
    /// <summary>
    /// Interação lógica para AcervoDetailsView.xam
    /// </summary>
    public partial class AcervoDetailsView : UserControl
    {
        private Image imgBorrow;
        private tblProduto _produtoAtual;
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

            imgBorrow = (Image)btnBorrow.Content;

            if (db.tblEmprestimo.Count(e => e.IDProduto == _produtoAtual.IDProduto) > 0)
                imgBorrow.Source = (BitmapImage)FindResource("XImage");
            
            txtCount.Text = $"Exemplares Disponíveis: {db.tblProduto.Where(p => p.tblEmprestimo.Count == 0).Count(p => p.ISBN == _produtoAtual.ISBN)}";

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


        public void Borrow(object sender, RoutedEventArgs e)
        {
            using var db = new TCCFEntities();

            if (db.tblEmprestimo.Count(e => e.IDProduto == _produtoAtual.IDProduto) == 0)
            {

                var _3DaysAgo = DateTime.Now.AddDays(-3);


                var dialog = new ChooseUserDialog("Empréstimo", "Selecione um usuário da lista")
                {
                    Leitores = (
                        from r in db.tblReserva
                        where r.DataReserva > _3DaysAgo
                              && r.IDProduto == _produtoAtual.IDProduto
                        select r.tblLeitor
                    ).ToList()
                };


                dialog.ShowDialog();

                if (dialog.Result == null)
                    return;

                db.tblEmprestimo.Add(new tblEmprestimo()
                {
                    DataRetirada = DateTime.Now,
                    Renovacao = 0,
                    IDProduto = _produtoAtual.IDProduto,
                    IDLeitor = dialog.Result.IDLeitor,
                    IDFuncionario = StateRepository.loggedInUser.Get().IDFuncionario,
                    DataEntrega = DateTime.Now.AddDays(7)
                });

                db.SaveChanges();

                StateRepository.currentView.Set(new AcervoDetailsView(db.tblProduto.First(p => p.IDProduto == _produtoAtual.IDProduto)));

            }
            else
            {
                db.tblEmprestimo.Remove(db.tblEmprestimo.First(e => e.IDProduto == _produtoAtual.IDProduto));
                db.SaveChanges();
                StateRepository.currentView.Set(new AcervoDetailsView(db.tblProduto.First(p => p.IDProduto == _produtoAtual.IDProduto)));

            }
        }
    }
}
