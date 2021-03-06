﻿using System.Collections.Generic;

namespace binaryTreeTest {
    public class BinaryNode<T> where T : class {
        private BinaryNode<T> _falseNode;
        private BinaryNode<T> _trueNode;

        public BinaryNode<T> this[bool val] => val ? TrueNode() : FalseNode();
        public T Value { get; set; }

        public BinaryNode(T value) {
            Value = value;
        }

        public BinaryNode<T> FalseNode(T val = null) {
            return InitOrReturnNode(val, false);
        }

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
        public T Search(BitArray binary, int index = -1) {
            if (++index != binary.Count)
                return binary[index] ? _trueNode?.Search(binary, index) : _falseNode?.Search(binary, index);
            return Value;
        }
        public BitArray Search(T value, BitArray key) {
            if (EqualityComparer<T>.Default.Equals(Value, value)) return key;
            BitArray newKey = new BitArray(key);
            newKey.Add(true);
            BitArray res = _trueNode?.Search(value, newKey);
            if (res != null) return res;
            newKey[newKey.Count - 1] = false;
            res = _falseNode?.Search(value, newKey);
            return res;
        }
        public BinaryNode<T> TrueNode(T val = null) {
            return InitOrReturnNode(val, true);
        }
    }
}