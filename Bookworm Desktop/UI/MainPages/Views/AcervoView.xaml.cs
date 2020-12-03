using System;
using Bookworm_Desktop.UI.MainPages.Views.Acervo;
using Bookworm_Desktop.UI.MainPages.Views.Funcionarios;
using SugmaState;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Bookworm_Desktop.UI.Components;

namespace Bookworm_Desktop.UI.MainPages.Views
{
    /// <summary>
    /// Interação lógica para AcervoView.xam
    /// </summary>
    public partial class AcervoView : UserControl, IReloadable
    {
        public class GeneroItem : DependencyObject
        {
            public tblGenero Genero { get; set; }

            public GeneroItem()
            {

            }
            public GeneroItem(tblGenero Genero)
            {
                this.Genero = Genero;
            }
        }

        public class ResultItem : DependencyObject
        {
            public int ID { get; set; }
            public byte[] ImagemProd { get; set; }
            public string NomeLivro { get; set; }
            public string AutoresLivro { get; set; }
            public string GeneroString => string.Join(", ", Generos);
            public IEnumerable<string> Generos { get; set; }
            public string Editora { get; set; }
            public string AnoEdicao { get; set; }
        }

        private State<IEnumerable<GeneroItem>> generos = new State<IEnumerable<GeneroItem>>(new GeneroItem[] { });
        private State<IEnumerable<ResultItem>> results = new State<IEnumerable<ResultItem>>(new ResultItem[] { });

        public AcervoView()
        {
            InitializeComponent();

            using var db = new TCCFEntities();

            generos.Listen(g => AcervoContainer.ItemsSource = g);

            results.Listen(r =>
            {
                AcervoContainer.Visibility = Visibility.Collapsed;
                ResultsContainer.ItemsSource = r;
            });

            var query = (
                    from g in db.tblGenero
                    where g.tblGeneroProduto.Any()
                    select g
                    ).Include("tblGeneroProduto")
                    .Include("tblGeneroProduto.tblProduto").ToList();

            generos.Set(query.Select(g => new GeneroItem(g)));
        }

        private void Add(object _, RoutedEventArgs e)
        {
            StateRepository.currentView.Set(new AcervoEditView(new tblProduto(), FuncionarioEditView.EditContext.Creating));
        }

        private void ToggleChecked(object sender, RoutedEventArgs e)
        {

        }

        private void OpenDetails(object sender, RoutedEventArgs e)
        {
            var context = (ResultItem) ((Clickable) sender).DataContext;

            using var db = new TCCFEntities();

            StateRepository.currentView.Set(new AcervoDetailsView(db.tblProduto.First(p => p.IDProduto == context.ID)));
        }
        private void DoSearch(object sender, RoutedEventArgs e)
        {
            using var db = new TCCFEntities();
            results.Set(db.tblProduto.Where(p => p.NomeLivro.Contains(txtSearch.Text)
            || p.Editora.Contains(txtSearch.Text)
            || p.AutoresLivro.Contains(txtSearch.Text)
            ).ToList().Select(p => new ResultItem
            {
                AnoEdicao = p.AnoEdicao.GetValueOrDefault(DateTime.Now).Year.ToString(),
                NomeLivro = p.NomeLivro,
                Editora = p.Editora,
                ImagemProd = p.ImagemProd,
                AutoresLivro = p.AutoresLivro,
                Generos = p.tblGeneroProduto.Select(g => g.tblGenero.NomeGenero).ToList(),
                ID = p.IDProduto
            }).ToList());
        }

        public void OnReload()
        {
            using var db = new TCCFEntities();

            var query = (
                    from g in db.tblGenero
                    where g.tblGeneroProduto.Any()
                    select g
                ).Include("tblGeneroProduto")
                .Include("tblGeneroProduto.tblProduto").ToList();

            generos.Set(query.Select(g => new GeneroItem(g)));
        }
    }
}
