using System.Collections.Generic;

namespace binaryTreeTest {
    public class BinaryNode<T> where T : class {
        private static int _currentIndex = -1;
        private BinaryNode<T> _falseNode;
        private BinaryNode<T> _trueNode;

        public BinaryNode<T> this[bool val] => val ? TrueNode() : FalseNode();
        public T Value { get; set; }

        public BinaryNode(T value) {
            Value = value;
        }
        public BinaryNode<T> FalseNode(T val = null) => InitOrReturnNode(val, false);
        private BinaryNode<T> InitOrReturnNode(T val, bool truefalse) {
            if (val == null) {
                if (truefalse) return _trueNode ?? (_trueNode = new BinaryNode<T>(null));
                return _falseNode ?? (_falseNode = new BinaryNode<T>(null));
            }
            BinaryNode<T> newNode = new BinaryNode<T>(val);
            if (truefalse) _trueNode = newNode;
            else _falseNode = newNode;
            return newNode;
        }

        public T Search(BitArray binary) {
            if (++_currentIndex != binary.Count)
                return binary[_currentIndex] ? _trueNode?.Search(binary) : _falseNode?.Search(binary);
            _currentIndex = -1;
            return Value;
        }
        public BitArray Search(T value, BitArray key = null) {
            if (EqualityComparer<T>.Default.Equals(Value, value)) return key;
            BitArray newKey = new BitArray(key);
            newKey.Add(true);
            BitArray res = _trueNode?.Search(value, newKey);
            if (res != null) return res;
            newKey[newKey.Count - 1] = false;
            res = _falseNode?.Search(value, newKey);
            return res;
        }

        public BinaryNode<T> TrueNode(T val = null) => InitOrReturnNode(val, true);
    }
}