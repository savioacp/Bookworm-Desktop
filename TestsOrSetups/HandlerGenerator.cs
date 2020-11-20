using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsOrSetups
{
    class HandlerGenerator<T>
    {
        private Dictionary<T, Action<object, EventArgs>> handlers;

        public Action<object, EventArgs> GetCustomHandler(Action<object, EventArgs, T> handler, T discriminator)
        {
            void _handler(object sender, EventArgs e) => handler(sender, e, discriminator);

            handlers.Add(discriminator, _handler);

            return _handler;
        }
    }
}
