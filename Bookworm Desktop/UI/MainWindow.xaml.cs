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
			public int Id { get; set; }
            public string Name { get; set; }
        }

        private List<User> itemSource;
        public MainWindow()
        {
            InitializeComponent();
            itemSource = new List<User>
            {
                new User {Id = 0, Name = "Juliana Craveiro Fusco"},
                new User {Id = 1, Name = "Beatriz Silvério"},
                new User {Id = 2, Name = "Guilherme Panza"},
                new User {Id = 3, Name = "Khadeeja Ferraz Baloch"},
                new User {Id = 4, Name = "Lucas Kenzo Cyra"},
                new User {Id = 5, Name = "Lara Souza Ishibashi"},
                new User {Id = 6, Name = "Sávio Alves Cabelo Pereira"}
            };

            LoginButtonsContainer.ItemsSource = itemSource;

        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Button button = (Button) sender;
			User clickedUser = (User) button.DataContext;
			txtUsername.Text = clickedUser.Name;
		}
	}
}
