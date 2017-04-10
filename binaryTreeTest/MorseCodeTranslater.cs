using System;
using System.Linq;

namespace binaryTreeTest {
    public class MorseCodeTranslater {
        private readonly BinaryTree<string> _tree;
        public string this[string value] => _tree.Root.Search(value.ToUpper(), null).ToString();

        public MorseCodeTranslater() {
            _tree = new BinaryTree<string>(FileManager.ReadTranslationFile("EN.Txt"));
            _tree.Root.Value = "";
        }
        public MorseCodeTranslater(string translationFilePath) {
            _tree = new BinaryTree<string>(FileManager.ReadTranslationFile(translationFilePath));
        }

        private static bool CharToBit(char value) {
            return value != '0';
        }
        private string ConvertCharToMorse(string value) {
            BitArray morse = _tree[value.ToUpper()];
            if (morse == null) return "\nerror, unknown character translation: " + value;
            return morse.ToString();
        }
        public string ConvertFromMorse(string binary) {
            string[] words = binary.Split(new[] {"       "}, StringSplitOptions.None);
            string result = words.Aggregate("", (current, w) => current + ConvertFromMorseWord(w) + " ");
            return result;
        }
        private string ConvertFromMorseWord(string binary) {
            string[] chars = binary.Split(new[] {"   "}, StringSplitOptions.None);
            return chars.Aggregate("", (current, t) => current + _tree[ConvertMorseTextToBitArray(t)]);
        }
        private static BitArray ConvertMorseTextToBitArray(string morse) {
            BitArray bits = new BitArray(morse.Length);
            for (int i = 0; i < morse.Length; i++) bits[i] = CharToBit(morse[i]);
            return bits;
        }
        public string ConvertToMorse(string text) {
            string[] words = text.Split(new[] {" "}, StringSplitOptions.None);
            string morseCode = words.Aggregate("",
                (current, word) => current + ConvertWordToMorse(word) + DefaultValues.WordSpacing);
            return morseCode.Remove(morseCode.Length - DefaultValues.WordSpacing.Length);
        }
        private string ConvertWordToMorse(string word) {
            string result = word.Aggregate("",
                (current, s) => current + ConvertCharToMorse(s.ToString()) + DefaultValues.CharacterSpacing);
            return result.Length > 0 ? result.Remove(result.Length - DefaultValues.CharacterSpacing.Length) : result;
        }

        public static class DefaultValues {
            public const string CharacterSpacing = "   ";
            public static readonly string[] DefaultTranslations = {
                "A01", "B1000", "C1010", "D100", "E0", "F0010",
                "G110", "H0000", "I00", "J0111", "K101", "L0100",
                "M11", "N10", "O111", "P0110", "Q1101", "R010", "S000",
                "T1", "U001", "V0001", "W011", "X1001", "Y1011", "Z1100"
            };
            public const string WordSpacing = "       ";
        }
    }
}