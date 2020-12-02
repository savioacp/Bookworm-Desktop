using SugmaState;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Bookworm_Desktop.UI.Components
{
    /// <summary>
    /// Interação lógica para Spinner.xam
    /// </summary>
    public partial class Spinner : UserControl
    {
        public State<bool> IsLoading;
        public State<Color> Color;

        public Spinner()
        {
            InitializeComponent();
            IsLoading = new State<bool>(true);
            Color = new State<Color>(Colors.Transparent);

            Color.Listen(c =>
            {
                path.Stroke = new SolidColorBrush(c);
            });

            path.BeginStoryboard((Storyboard)FindResource("StoryboardLoading"));
        }
    }
}
