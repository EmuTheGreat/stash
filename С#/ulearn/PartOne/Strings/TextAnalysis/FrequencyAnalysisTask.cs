using System.Collections.Generic;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            FillDictionary(GetNGrams(text, 1), result);
            FillDictionary(GetNGrams(text, 2), result);
            return result;
        }

        static void FillDictionary(Dictionary<string, Dictionary<string, int>> nGramm, Dictionary<string, string> result)
        {
            int maxCount;
            string maxElement;     
            foreach (var pr in nGramm)
            {
                maxCount = 1;
                maxElement = "zz";
                foreach (var e in pr.Value)
                {
                    if (e.Value > maxCount)
                    {
                        maxCount = e.Value;
                        maxElement = e.Key;
                    }
                    else if (e.Value == maxCount) if (string.CompareOrdinal(e.Key, maxElement) < 0) maxElement = e.Key;
                }
                result.Add(pr.Key, maxElement);
            }
        }

        static Dictionary<string, Dictionary<string, int>> GetNGrams(List<List<string>> text, int diff)
        {
            var dict = new Dictionary<string, Dictionary<string, int>>();
            string key, nextKey;

            for (int i = 0; i < text.Count; i++)
                for (int j = 0; j < text[i].Count - diff; j++)
                {
                    if (diff == 2) key = $"{text[i][j]} {text[i][j + 1]}";
                    else key = text[i][j];
                    nextKey = text[i][j + diff];
                    if (!dict.ContainsKey(key)) dict[key] = new Dictionary<string, int>();
                    if (!dict[key].ContainsKey(nextKey)) dict[key][nextKey] = 0;
                    dict[key][nextKey]++;
                }
            return dict;
        }
    }
}
