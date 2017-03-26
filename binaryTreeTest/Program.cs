using System;

namespace binaryTreeTest
{
    public class Program
    {
        public static void Main(string[] args) {
            MorseCodeTranslater translator = new MorseCodeTranslater();

            string test = translator["T"] +  MorseCodeValues.CharacterSpacing + translator["E"] + MorseCodeValues.CharacterSpacing + translator["S"] + MorseCodeValues.CharacterSpacing + translator["T"];
            string testtest = test + MorseCodeValues.WordSpacing + test;

            translator.ConvertFromMorse(test);
            translator.ConvertFromMorse(testtest);
            Console.WriteLine(testtest);
            string result = translator.ConvertToMorse("Test Test");
            Console.WriteLine(result);
            translator.ConvertFromMorse(result);
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
