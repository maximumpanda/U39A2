using System.Collections.Generic;

namespace binaryTreeTest {
    public class BitArray {
        private readonly List<bool> _bits = new List<bool>();

        public int Count => _bits.Count;

        public bool this[int id] {
            get { return _bits[id]; }
            set { _bits[id] = value; }
        }

        public BitArray() {
        }
        public BitArray(int size) {
            for (int i = 0; i < size; i++) _bits.Add(false);
        }

        public BitArray(BitArray original) {
            if (original == null) return;
            for (int i = 0; i < original.Count; i++) _bits.Add(original[i]);
        }

        public void Add(bool value) {
            _bits.Add(value);
        }

        public void Remove(int id) {
            _bits.RemoveAt(id);
        }

        public override string ToString() {
            string output = "";
            foreach (bool bit in _bits) output += bit ? "1" : "0";
            return output;
        }
    }
}