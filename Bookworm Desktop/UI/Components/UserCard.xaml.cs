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
	/// Interação lógica para UserCard.xam
	/// </summary>
	public partial class UserCard : UserControl
	{
		public UserCard()
		{
			InitializeComponent();
			StateRepository.loggedInUser.Listen(f =>
			{
				Username.Text = f.Nome;
				ProfileImage.ImageSource = (BitmapImage)(new ByteToImageConverter().Convert(f.ImagemFunc, null, null, null));
			});
		}
	}
}
