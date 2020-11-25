using SugmaState;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Bookworm_Desktop.UI.LoginPages
{
    public partial class Login1 : UserControl
    {
        public Login1()
        {
            InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Button.IsEnabled = false;
            Button.Cursor = Cursors.No;

            Task.Run(() =>
            {
                using (var db = new TCCFEntities())
                {
                    Dispatcher.Invoke(() =>
                    {
                        try
                        {
                            StateRepository.loggedInUser.Set(db.tblFuncionario.Include("tblCargo").First(f => f.Email == txtEmail.Text));
                            BeginStoryboard((Storyboard)FindResource("PanelChangeStoryboard"));
                        }
                        catch
                        {
                            MessageBox.Show("Ops! Não foi encontrado nenhum funcionário com este email... Por favor, verifique e tente novamente.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                            
                            Button.IsEnabled = true;
                            Button.Cursor = Cursors.Hand;
                        }
                    });
                }
            });
        }
    }
}
