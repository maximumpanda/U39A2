using System.Collections.Generic;

namespace binaryTreeTest {
    public class BinaryTree<T> where T : class {
        public readonly BinaryNode<T> Root;
        public BitArray this[T value] => Root.Search(value, null);
        public T this[BitArray value] => Root.Search(value);

        public BinaryTree(Dictionary<T, BitArray> translations) {
            Root = BuildTree(translations);
        }

        private static BinaryNode<T> BuildTree(Dictionary<T, BitArray> translations) {
            BinaryNode<T> root = new BinaryNode<T>(null);
            foreach (KeyValuePair<T, BitArray> translation in translations) {
                BinaryNode<T> current = root;
                for (int i = 0; i < translation.Value.Count; i++) current = current[translation.Value[i]];
                current.Value = translation.Key;
            }
            return root;
        }
    }
}