using Bookworm_Desktop.UI.MainPages.Views.Acervo;
using System.Windows;
using System.Windows.Controls;

namespace Bookworm_Desktop.UI.Components
{
    /// <summary>
    /// Interação lógica para GeneroComponent.xam
    /// </summary>
    public partial class GeneroComponent : UserControl
    {
        public tblGenero Genero
        {
            get =>
                (tblGenero)GetValue(GeneroProperty);
            set
            {
                OnGeneroChanged(value);
                SetValue(GeneroProperty, value);
            }
        }

        public static readonly DependencyProperty GeneroProperty = DependencyProperty.Register("Genero", typeof(tblGenero), typeof(GeneroComponent), new PropertyMetadata());

        public GeneroComponent()
        {
            InitializeComponent();

        }

        private void OnGeneroChanged(tblGenero value)
        {

        }

        private void ImageClick(object sender, RoutedEventArgs e)
        {
            var produtoClicado = ((tblGeneroProduto)((Grid)sender).DataContext).tblProduto;

            StateRepository.currentView.Set(new AcervoDetailsView(produtoClicado));
        }
    }
}
