﻿using SugmaState;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bookworm_Desktop.Annotations;
using Bookworm_Desktop.UI.Dialogs;
using Bookworm_Desktop.UI.MainPages.Views.Funcionarios;

namespace Bookworm_Desktop.UI.MainPages.Views
{
    /// <summary>
    /// Interação lógica para FuncionariosView.xam
    /// </summary>
    public partial class FuncionariosView : UserControl
    {
        public class ItemFuncionario : INotifyPropertyChanged
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
            public byte[] ImagemFunc { get; set; }
            public string Nome { get; set; }
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        State<IEnumerable<ItemFuncionario>> matchingFuncs = new State<IEnumerable<ItemFuncionario>>(new ItemFuncionario[] { });
        public FuncionariosView()
        {
            InitializeComponent();
            matchingFuncs.Listen(fs =>
            {
                FuncsContainer.ItemsSource = fs;
            });

            using (var db = new TCCFEntities())
                matchingFuncs.Set(db.tblFuncionario.OrderBy(f => f.Nome).Take(10).ToList().Select(f => new ItemFuncionario()
                {
                    ImagemFunc = f.ImagemFunc,
                    IsChecked = false,
                    Nome = f.Nome,
                    Id = f.IDFuncionario
                }));
        }

        public void DoSearch(object sender, RoutedEventArgs e)
        {
            using (var db = new TCCFEntities())
                matchingFuncs.Set(db.tblFuncionario.Where(f => f.Nome.Contains(txtSearch.Text)).OrderBy(f => f.Nome).ToList().Select(f => new ItemFuncionario()
                {
                    ImagemFunc = f.ImagemFunc,
                    IsChecked = false,
                    Nome = f.Nome,
                    Id = f.IDFuncionario
                }));
        }

        public void ToggleCheched(object sender, RoutedEventArgs e)
        {
            var context = (ItemFuncionario)((FrameworkElement)sender).DataContext;
            matchingFuncs.Set(matchingFuncs.Get().Select(f =>
            {
                if (f.Id != context.Id) return f;

                f.IsChecked = !context.IsChecked;
                return f;
            }));
        }

        public void DeleteSelected(object sender, RoutedEventArgs e)
        {
            if (ConfirmDialog.Show("Deletar funcionários"))
            {
                using (var db = new TCCFEntities())
                    foreach (var funcItem in matchingFuncs.Get().Where(f => f.IsChecked))
                    {
                        var toDelete = db.tblFuncionario.First(f => f.IDFuncionario == funcItem.Id);
                        db.tblFuncionario.Remove(toDelete);

                        db.SaveChangesAsync();

                        DoSearch(sender, e);
                    }
            }
        }

        public void OpenDetails(object sender, RoutedEventArgs e)
        {
            var button = (FrameworkElement)sender;

            var clickedItemFunc = (ItemFuncionario)button.DataContext;

            using (var db = new TCCFEntities())
            {
                var clickedFunc = db.tblFuncionario.Include("tblCargo").First(f => f.IDFuncionario == clickedItemFunc.Id);

                StateRepository.currentView.Set(new FuncionarioDetailsView(clickedFunc));
            }
        }

    }
}