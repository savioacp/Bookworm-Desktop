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

namespace Bookworm_Desktop.UI
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public class User // só pra prototyping mesmo
        {
            public string Name { get; set; }
        }

        private List<User> itemSource;
        public MainWindow()
        {
            InitializeComponent();
            itemSource = new List<User>
            {
                new User {Name = "Juliana Craveiro Fusco"},
                new User {Name = "Beatriz Silvério"},
                new User {Name = "Guilherme Panza"},
                new User {Name = "Khadeeja Ferraz Baloch"},
                new User {Name = "Lucas Kenzo Cyra"},
                new User {Name = "Lara Souza Ishibashi"},
                new User {Name = "Sávio Alves Cabelo Pereira"}
            };

            LoginButtonsContainer.ItemsSource = itemSource;

        }
    }
}
