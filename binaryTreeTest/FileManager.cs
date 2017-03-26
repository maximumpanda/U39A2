using System;
using System.Collections.Generic;
using System.IO;

namespace binaryTreeTest
{
    public static class FileManager
    {
        public static Dictionary<string, BitArray> ReadTranslationFile(string language) {
            Dictionary<string, BitArray> translations = new Dictionary<string, BitArray>();
            if (!File.Exists("EN.txt")) {
                File.WriteAllLines("EN.txt", MorseCodeValues.DefaultTranslations);
            }
            using (StreamReader reader = new StreamReader(File.Open(language + ".Txt", FileMode.Open))) {
                while (!reader.EndOfStream) {
                    Tuple<string, BitArray> res = ReadTranslation(reader);
                    translations.Add(res.Item1, res.Item2);
                }
            }
            return translations;
        }

        private static Tuple<string, BitArray> ReadTranslation(StreamReader reader) {
            char val = (char) reader.Read();
            string read = reader.ReadLine();
            Tuple<string, BitArray> newTranslation = new Tuple<string, BitArray>(val.ToString(), new BitArray(read.Length));
            for (int i = 0; i < read.Length; i++) {
                switch (read[i]) {
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
