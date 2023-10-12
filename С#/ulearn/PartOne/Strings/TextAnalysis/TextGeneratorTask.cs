using System.Collections.Generic;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(Dictionary<string, string> nextWords, string phraseBeginning, int wordsCount)
        {
            var builder = new List<string>();
            var phrase = phraseBeginning.Split(' ');
            int indexOfKey;

            foreach (var e in phrase) builder.Add(e);

            while (wordsCount != 0)
            {
                indexOfKey = builder.Count - 1;
                string key = builder[indexOfKey];

                if (indexOfKey + 1 > 1 && nextWords.ContainsKey($"{builder[indexOfKey - 1]} {key}"))
                    wordsCount -= GetPhrase($"{builder[indexOfKey - 1]} {key}", nextWords, builder);
                else if (nextWords.ContainsKey(key))
                    wordsCount -= GetPhrase(key, nextWords, builder);
                else break;
            }
            phraseBeginning = string.Join(" ", builder);
            return phraseBeginning;
        }

        static int GetPhrase(string key, Dictionary<string, string> nextWords, List<string> builder)
        {
            var i = 0;
            var value = nextWords[key].Split(' ');

            foreach (var e in value)
            {
                builder.Add(e); 
                i++;
            }
            return i;
        }
    }
}