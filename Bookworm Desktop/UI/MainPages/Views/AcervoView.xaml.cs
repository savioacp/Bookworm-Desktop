using Bookworm_Desktop.UI.MainPages.Views.Acervo;
using Bookworm_Desktop.UI.MainPages.Views.Funcionarios;
using SugmaState;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        private State<IEnumerable<GeneroItem>> generos = new State<IEnumerable<GeneroItem>>(new GeneroItem[] { });
        public AcervoView()
        {
            InitializeComponent();

            using var db = new TCCFEntities();

            generos.Listen(g => AcervoContainer.ItemsSource = g);


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

        }
        private void DoSearch(object sender, RoutedEventArgs e)
        {

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
