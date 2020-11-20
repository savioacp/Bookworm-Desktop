using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bookworm_Desktop.UI
{
    public class PageHistory
    {
        private readonly Stack<UIElement> historyStack;

        public PageHistory()
        {
            historyStack = new Stack<UIElement>();
        }

        public UIElement Navigate(UIElement to)
        {
            historyStack.Push(to);
            return to;
        }

        public UIElement GoBack()
        {
            return historyStack.Pop();
        }

        public Stack<UIElement> GetStack()
        {
            return historyStack;
        }
    }
}
