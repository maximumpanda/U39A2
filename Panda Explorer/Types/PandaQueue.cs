using System;
using System.Collections.Generic;

namespace Panda_Explorer.Types {
    internal class PandaQueue<T> {
        private readonly Queue<T> _queue = new Queue<T>();

        public int Count => _queue.Count;

        public virtual T Dequeue() {
            return _queue.Dequeue();
        }

        public virtual void Enqueue(T obj) {
            _queue.Enqueue(obj);
            ItemAdded?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler ItemAdded;
    }
}