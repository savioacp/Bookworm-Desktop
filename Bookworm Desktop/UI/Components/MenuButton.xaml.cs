using System.Windows;
using System.Windows.Controls;

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

        public IReloadable AssociatedView { get; set; }
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
                StateRepository.currentView.Set((UIElement)AssociatedView);
                AssociatedView.OnReload();
            };
        }
    }
}
