using Bookworm_Desktop.UI.Components;
using Bookworm_Desktop.UI.Dialogs;
using Bookworm_Desktop.UI.MainPages.Views.Clientes;
using Bookworm_Desktop.UI.MainPages.Views.Funcionarios;
using SugmaState;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Bookworm_Desktop.UI.MainPages.Views
{
    /// <summary>
    /// Interação lógica para ClientesView.xam
    /// </summary>
    public partial class ClientesView : UserControl, IReloadable
    {
        public class ItemLeitor : INotifyPropertyChanged
        {
            private bool _isChecked;

            public bool IsChecked
            {
                get => _isChecked;
                set
                {
                    _isChecked = value;
                    OnPropertyChanged(nameof(IsChecked));
                }
            }

            public int Id { get; set; }
            public byte[] ImagemLeitor { get; set; }
            public string Nome { get; set; }
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        State<IEnumerable<ItemLeitor>> matchingClients = new State<IEnumerable<ItemLeitor>>(new ItemLeitor[] { });

        public ClientesView()
        {
            InitializeComponent();

            matchingClients.Listen(cs =>
            {
                GridDefaultView.Visibility = Visibility.Collapsed;
                ClientsContainer.ItemsSource = cs;
            });
        }

        public void DoSearch(object sender, RoutedEventArgs e)
        {
            using var db = new TCCFEntities();

            matchingClients.Set(db.tblLeitor.Where(c => c.Nome.Contains(txtSearch.Text)).OrderBy(c => c.Nome).ToList().Select(c => new ItemLeitor()
            {
                Id = c.IDLeitor,
                ImagemLeitor = c.ImagemLeitor,
                IsChecked = false,
                Nome = c.Nome
            }));
        }

        public void ToggleCheched(object sender, RoutedEventArgs e)
        {
            var context = (ItemLeitor)((FrameworkElement)sender).DataContext;
            matchingClients.Set(matchingClients.Get().Select(c =>
            {
                if (c.Id != context.Id) return c;

                c.IsChecked = !context.IsChecked;
                return c;
            }));
        }

        public void DeleteSelected(object sender, RoutedEventArgs e)
        {

            using var db = new TCCFEntities();

            var selectedClients = matchingClients.Get().Where(c => c.IsChecked).ToList();

            if (!selectedClients.Any()) return;

            bool plural = selectedClients.Count > 1;

            if (ConfirmDialog.Show($"Tem certeza que deseja deletar este{(plural ? "s" : "")} leitor{(plural ? "es" : "")}?"))
            {
                var toDeleteIds = selectedClients.Select(c => c.Id).ToArray();
                var toDelete = db.tblLeitor.Where(c => toDeleteIds.Contains(c.IDLeitor));

                db.tblLeitor.RemoveRange(toDelete);

                db.SaveChanges();

                DoSearch(sender, e);
            }
        }

        public void OpenDetails(object sender, RoutedEventArgs e)
        {
            // TODO: Open Client Details
            var clickedContext = (ItemLeitor)((Clickable)sender).DataContext;

            using var db = new TCCFEntities();

            var clickedClient = db.tblLeitor.Include("tblTipoLeitor")
                .First(c => c.IDLeitor == clickedContext.Id);

            StateRepository.currentView.Set(new ClienteDetailsView(clickedClient, FuncionarioEditView.EditContext.Editing));
        }

        public void Add(object sender, RoutedEventArgs e)
        {
            // TODO: Add Client

        }

        public void OnReload()
        {

        }
    }
}
