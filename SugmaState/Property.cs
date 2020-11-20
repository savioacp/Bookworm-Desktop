using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SugmaState
{
	public class State<T>
	{
		private T Value { get; set; }

		private List<Action<T>> listeners;

		public State(T InitialValue)
		{
			listeners = new List<Action<T>>();
			Value = InitialValue;
		}

		public void Set(T newValue)
		{
			if (!Value.Equals(newValue))
			{
				Value = newValue;
				Notify(newValue);
			}
		}

		private void Notify(T newValue)
		{
			foreach (var listener in listeners)
			{
				listener.Invoke(newValue);
			}
		}

		public T Get()
		{
			return Value;
		}

		public Action Listen(Action<T> listener)
		{
			listeners.Add(listener);
			return () => { listeners.Remove(listener); };
		}
	}
}
