using System;

namespace DataStructures.StackWithMax
{
	public class StackWithMax<T> where T : IComparable<T>
	{
		private class StackElement<TElement> where TElement : IComparable<TElement>
		{
			public TElement Value { get; }
			public StackElement<TElement> CurrentMaximum { set; get; }
			public StackElement<TElement> Previous { set; get; }

			public StackElement(TElement value)
			{
				Value = value;
			}
		}

		private StackElement<T> _top;
		private StackElement<T> _max;

		private int _count = 0;

		public int Length => _count;

		public T Pop()
		{
			if (_top == null)
			{
				return default;
			}

			var ret = _top.Value;
			_count--;

			if (_top.Previous == null)
			{
				// means we are popping last element
				_top = null;
				_max = null;

				return ret;
			}
			else
			{
				var prev = _top.Previous;
				_top = prev;

				if (ret.CompareTo(_max.Value) == 0)
				{
					// means we have popped current maximum
					_max = _top.CurrentMaximum;
				}
			}

			return ret;
		}

		public void Push(T element)
		{
			if (_top == null)
			{
				var newElt = new StackElement<T>(element);
				_top = newElt;
				_count++;
				_max = newElt;
				_top.CurrentMaximum = newElt;
			}
			else
			{
				var newEelement = new StackElement<T>(element)
				{
					Previous = _top
				};

				var comparisonResult = element.CompareTo(_max.Value); // compare with current maximum

				if (comparisonResult < 0)
				{
					// elt < max
					newEelement.CurrentMaximum = _max;

				}
				else
				{
					// elt >= max
					_max = newEelement;
					newEelement.CurrentMaximum = _max;
				}

				_top = newEelement;
				_count++;
			}
		}

		public T GetMaxElement()
		{
			return _top.CurrentMaximum.Value;
		}
	}
}
