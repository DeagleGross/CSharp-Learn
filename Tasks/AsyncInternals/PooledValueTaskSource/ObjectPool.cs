﻿using System;
using System.Threading;

namespace AsyncInternals.PooledValueTaskSource
{
    public class ObjectPool<T> where T : class
    {
        private T _firstItem;
        private readonly T[] _items;
        private readonly Func<T> _generator;

        public ObjectPool(Func<T> generator, int size)
        {
            _generator = generator ?? throw new ArgumentNullException($"{nameof(generator)}");
            _items = new T[size - 1];
        }

        public T Rent()
        {
            // PERF: Examine the first element. If that fails, RentSlow will look at the remaining elements.
            // Note that the initial read is optimistically not synchronized. That is intentional. 
            // We will interlock only when we have a candidate. in a worst case we may miss some
            // recently returned objects. Not a big deal.
            Console.WriteLine("R:");

            var inst = _firstItem;
            if (inst is null || inst != Interlocked.CompareExchange(ref _firstItem, null, inst))
            {
                inst = RentSlow();
            }

            return inst;
        }

        public void Return(T item)
        {
            Console.WriteLine("*");
            if (_firstItem is null)
            {
                // Intentionally not using interlocked here. 
                // In a worst case scenario two objects may be stored into same slot.
                // It is very unlikely to happen and will only mean that one of the objects will get collected.
                _firstItem = item;
            }
            else
            {
                ReturnSlow(item);
            }
        }

        private T RentSlow()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                // Note that the initial read is optimistically not synchronized. That is intentional. 
                // We will interlock only when we have a candidate. in a worst case we may miss some
                // recently returned objects. Not a big deal.
                var inst = _items[i];
                if (inst is not null)
                {
                    if (inst == Interlocked.CompareExchange(ref _items[i], null, inst))
                    {
                        Console.WriteLine("  -");
                        return inst;
                    }
                }
            }
            
            Console.WriteLine("  -");
            return _generator();
        }

        private void ReturnSlow(T obj)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] is null)
                {
                    // Intentionally not using interlocked here. 
                    // In a worst case scenario two objects may be stored into same slot.
                    // It is very unlikely to happen and will only mean that one of the objects will get collected.
                    _items[i] = obj;
                    break;
                }
            }
        }
    }
}
