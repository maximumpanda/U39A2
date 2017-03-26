using System;
using System.Collections.Generic;
using System.Linq;

namespace binaryTreeTest
{
    public class MorseCodeTranslater {
        private readonly BinaryNode<string> _root;
        public string this[string value] => _root.Search(value.ToUpper()).ToString();

        public MorseCodeTranslater() {
            _root = BuildTranslationsTree(FileManager.ReadTranslationFile("EN"));
        }
        public string ConvertFromMorse(string binary) {
            string[] words = binary.Split(new[] { "       " }, StringSplitOptions.None);
            string result = words.Aggregate("", (current, w) => current + ConvertFromMorseWord(w) + " ");
            Console.WriteLine(result);
            return result;
        }

        public string ConvertToMorse(string text) {
            string[] words = text.Split(new[] {" "}, StringSplitOptions.None);
            string morseCode = words.Aggregate("", (current, word) => current + (ConvertWordToMorse(word) + MorseCodeValues.WordSpacing));   
            return morseCode.Remove(morseCode.Length - MorseCodeValues.WordSpacing.Length);
        }

        private string ConvertWordToMorse(string word) {
            string result = word.Aggregate("", (current, s) => current + (ConvertCharToMorse(s.ToString()) + MorseCodeValues.CharacterSpacing));
            return result.Length > 0 ? result.Remove(result.Length- MorseCodeValues.CharacterSpacing.Length) : result;
        }

        private string ConvertCharToMorse(string value) {
            BitArray morse = _root.Search(value.ToUpper());
            if (morse == null) return "error, unknown character translation: "  + value;
            return morse.ToString();
        }

        private string ConvertFromMorseWord(string binary) {
            string[] chars = binary.Split(new[] { "   " }, StringSplitOptions.None);
            return chars.Aggregate("", (current, t) => current + _root.Search(ConvertMorseTextToBitArray(t)));
        }

        private BitArray ConvertMorseTextToBitArray(string morse) {
            BitArray bits = new BitArray(morse.Length);
            for (int i = 0; i < morse.Length; i++) {
                bits[i] = CharToBit(morse[i]);
            }
            return bits;
        }

        private bool CharToBit(char value) {
            if (value == '0') return false;
            return true;
        }

        private static BinaryNode<string> BuildTranslationsTree(Dictionary<string, BitArray> translations) {
            BinaryNode<string> root = new BinaryNode<string>("");           
            foreach (KeyValuePair<string, BitArray> translation in translations) {
                BinaryNode<string> current = root;
                for (int i = 0; i < translation.Value.Count; i++) {
                    current = current[translation.Value[i]];
                }
                current.Value = translation.Key;
            }
            return root;
        }
    }

    public static class MorseCodeValues {

        public const string CharacterSpacing = "   ";
        public const string WordSpacing = "       ";

        public static readonly string[] DefaultTranslations = {
            "A01",
            "B1000",
            "C1010",
            "D100",
            "E0",
            "F0010",
            "G110",
            "H0000",
            "I00",
            "J0111",
            "K101",
            "L0100",
            "M11",
            "N10",
            "O111",
            "P0110",
            "Q1101",
            "R010",
            "S000",
            "T1",
            "U001",
            "V0001",
            "W011",
            "X1001",
            "Y1011",
            "Z1100"
        };
}
}
