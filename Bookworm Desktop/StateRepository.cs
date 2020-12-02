using Bookworm_Desktop.UI.MainPages.Views;
using SugmaState;
using System.Windows;

namespace Bookworm_Desktop
{
    public static class StateRepository
    {
        public static readonly State<tblFuncionario> loggedInUser = new State<tblFuncionario>(new tblFuncionario { Nome = "none" });
        public static readonly State<bool> isAuthenticated = new State<bool>(false);

        public static readonly State<UIElement> currentView = new State<UIElement>(new HomeView());
    }
}
