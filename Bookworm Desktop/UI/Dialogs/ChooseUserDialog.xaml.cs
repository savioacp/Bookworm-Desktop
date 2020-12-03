using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Bookworm_Desktop.UI.Components;
using Xceed.Wpf.AvalonDock.Layout;

namespace Bookworm_Desktop.UI.Dialogs
{
    /// <summary>
    /// Lógica interna para ChooseUserDialog.xaml
    /// </summary>
    public partial class ChooseUserDialog : Window
    {
        public class Selectable<T> : DependencyObject
        {
            public bool IsChecked { get; set; }

            public T Object { get; }

            public Selectable(T obj) : this(obj, false) { }

            public Selectable(T obj, bool isChecked)
            {
                Object = obj;
                this.IsChecked = false;
            }
        }

        public IEnumerable<tblLeitor> Leitores
        {
            get => (IEnumerable<tblLeitor>)itemsControl.ItemsSource;
            set => itemsControl.ItemsSource = value;
        }


        public tblLeitor Result;

        public static readonly DependencyProperty LeitoresProperty = DependencyProperty.Register("Leitores", typeof(ObservableCollection<tblLeitor>), typeof(ChooseUserDialog), new PropertyMetadata(null));

        public static tblLeitor ShowDialog(string caption, string text)
        {
            return new ChooseUserDialog(caption, text).Result;
        }

        public ChooseUserDialog(string caption, string text)
        {
            InitializeComponent();
            txtTitle.Text = caption;
            txtDescription.Text = text;
        }

        public void OnHeaderDrag(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        public void OnConfirm(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void OnUserClick(object sender, RoutedEventArgs e)
        {
            var context = (tblLeitor) ((Clickable) sender).DataContext;

            Result = context;
        }

        public void OnSearch(object sender, RoutedEventArgs e)
        {
            using var db = new TCCFEntities();
            Leitores = (
                from l in db.tblLeitor
                where l.Nome.Contains(srchBox.Text)
                    || l.CPF.Contains(srchBox.Text)
                    || l.RG.Contains(srchBox.Text)
                select l
                    ).ToList();
        }
    }
}
