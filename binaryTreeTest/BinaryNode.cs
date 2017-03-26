using System;
using System.Collections;

namespace binaryTreeTest
{
    public class BinaryNode {
        private static int _currentIndex = -1;
        public char Value { get; set; }

        public BinaryNode this[bool val] => val ? TrueNode() : FalseNode();

        public BinaryNode TrueNode(char val = '\n') => InitOrReturnNode(val, true);
        private BinaryNode _trueNode;
        public BinaryNode FalseNode(char val = '\n') => InitOrReturnNode(val, false);
        private BinaryNode _falseNode;

        public BinaryNode(char value) {
            Value = value;
        }

        public char Search(BitArray binary) {
            if (++_currentIndex != binary.Length)
                return binary[_currentIndex] ? _trueNode.Search(binary) : _falseNode.Search(binary);
            _currentIndex = -1;
            return Value;
        }
        private BinaryNode InitOrReturnNode(char val, bool truefalse) {
            if (val == '\n') {
                if (truefalse) {
                    return _trueNode ?? (_trueNode = new BinaryNode('\n'));
                }
                return _falseNode ?? (_falseNode = new BinaryNode('\n'));
            }
            BinaryNode newNode = new BinaryNode(val);
            if (truefalse) _trueNode = newNode;
            else _falseNode = newNode;
            return newNode;
        }

    }
}
