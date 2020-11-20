using SugmaState;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            path.BeginStoryboard((Storyboard) FindResource("StoryboardLoading"));
        }
    }
}
