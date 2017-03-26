using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace binaryTreeTest
{
    public static class FileManager
    {
        public static Dictionary<char, BitArray> ReadTranslationFile(string language) {
            Dictionary<char, BitArray> translations = new Dictionary<char, BitArray>();
            if (!File.Exists("EN.txt")) {
                File.WriteAllLines("EN.txt", MorseCodeValues.DefaultTranslations);
            }
            using (StreamReader reader = new StreamReader(File.Open(language + ".Txt", FileMode.Open))) {
                while (!reader.EndOfStream) {
                    Tuple<char, BitArray> res = ReadTranslation(reader);
                    translations.Add(res.Item1, res.Item2);
                }
            }
            return translations;
        }

        private static Tuple<char, BitArray> ReadTranslation(StreamReader reader) {
            char val = (char) reader.Read();
            string read = reader.ReadLine();
            Tuple<char, BitArray> newTranslation = new Tuple<char, BitArray>(val, new BitArray(read.Length));
            for (int i = 0; i < read.Length; i++) {
                switch (read[i])
                {
                    case '0':
                        newTranslation.Item2[i] = false;
                        break;
                    case '1':
                        newTranslation.Item2[i] = true;
                        break;
                }
            }
            return newTranslation;
        }
    }
}
