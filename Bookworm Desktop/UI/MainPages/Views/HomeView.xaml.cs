using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Bookworm_Desktop.UI.MainPages.Views
{
    /// <summary>
    /// Interação lógica para HomeView.xam
    /// </summary>
    public partial class HomeView : UserControl, IReloadable
    {
        public HomeView()
        {
            InitializeComponent();

            using var db = new TCCFEntities();

            itemsControl.ItemsSource = db.tblProduto.OrderByDescending(p => p.IDProduto).Take(15).ToList();
        }

        public void OnReload()
        {
            using var db = new TCCFEntities();

            itemsControl.ItemsSource = db.tblProduto.OrderByDescending(p => p.IDProduto).Take(15).ToList();

        }

        public void ImageClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
