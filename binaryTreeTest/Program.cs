using System;

namespace binaryTreeTest {
    public class Program {
        public static void Main(string[] args) {
            MorseCodeTranslater translator = new MorseCodeTranslater();

            string test = translator["T"] + MorseCodeTranslater.DefaultValues.CharacterSpacing + translator["E"] +
                          MorseCodeTranslater.DefaultValues.CharacterSpacing + translator["S"] +
                          MorseCodeTranslater.DefaultValues.CharacterSpacing +
                          translator["T"];
            string testtest = test + MorseCodeTranslater.DefaultValues.WordSpacing + test;

            Console.WriteLine(translator.ConvertFromMorse(test));
            Console.WriteLine(translator.ConvertFromMorse(testtest));

            string result = translator.ConvertToMorse("Test Test");
            Console.WriteLine(result);
            Console.WriteLine(translator.ConvertFromMorse(result));
            Console.WriteLine("enter any text to convert to morse");

            while (true) {
                result = Console.ReadLine();
                if (result != null && result.ToLower() == "exit") break;
                result = translator.ConvertToMorse(result);
                Console.WriteLine(result);
                Console.WriteLine(translator.ConvertFromMorse(result));
            }
        }
    }
}