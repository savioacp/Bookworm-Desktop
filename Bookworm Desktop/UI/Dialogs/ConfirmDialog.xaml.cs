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
using System.Windows.Shapes;

namespace Bookworm_Desktop.UI.Dialogs
{
    /// <summary>
    /// Lógica interna para ConfirmDialog.xaml
    /// </summary>
    public partial class ConfirmDialog : Window
    {
        public static bool Show(string caption = "Confirmação.", string text = "Esta ação não tem mais volta.")
        {
            return new ConfirmDialog(caption, text).ShowDialog() == true;
        }

        public ConfirmDialog(string caption, string text)
        {
            InitializeComponent();
            txtTitle.Text = caption;
            txtDescription.Text = text;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        public void OnConfirm(object sender, RoutedEventArgs e)
        {
            if (RadioButtonSim.IsChecked == true)
                DialogResult = true;
            Close();
        }

        public void OnHeaderDrag(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

    }
}
