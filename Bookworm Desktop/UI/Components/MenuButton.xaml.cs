using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Bookworm_Desktop.UI.Components
{
    /// <summary>
    /// Interação lógica para MenuButton.xam
    /// </summary>
    public partial class MenuButton : UserControl
    {
        private bool _selected = false;
        public bool Selected
        {
            get => _selected;
            set
            {
                if (value == _selected) return;
                MainContainer.Background.Opacity = value ? 1 : 0;
                _selected = value;
            }
        }

        public string Text
        {
            get => (string)MainBtn.Content;
            set => MainBtn.Content = value;
        }

        public UIElement AssociatedView { get; set; }
        public MenuButton()
        {
            InitializeComponent();

            StateRepository.currentView.Listen(view =>
            {
                if (!view.Equals(AssociatedView))
                    Selected = false;
            });

            MainBtn.Click += (sender, e) =>
            {
                if (Selected) return;
                Selected = true;
                StateRepository.currentView.Set(AssociatedView);
            };
        }
    }
}
