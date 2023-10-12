using System.Collections.Generic;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        readonly char[] delimiters = { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };
        readonly Dictionary<string, Dictionary<int, List<int>>> wordsIdAndPos =
            new Dictionary<string, Dictionary<int, List<int>>>();

        public void Add(int id, string documentText)
        {
            var words = documentText.Split(delimiters);
            var pos = 0;
            foreach (var word in words)
            {
                if (!wordsIdAndPos.ContainsKey(word))
                {
                    wordsIdAndPos.Add(word, new Dictionary<int, List<int>>());
                    wordsIdAndPos[word].Add(id, new List<int>());
                }

                else if (!wordsIdAndPos[word].ContainsKey(id)) wordsIdAndPos[word].Add(id, new List<int>());

                wordsIdAndPos[word][id].Add(pos);
                pos += word.Length + 1;
            }
        }

        public List<int> GetIds(string word)
        {
            if (wordsIdAndPos.ContainsKey(word)) return new List<int>(wordsIdAndPos[word].Keys);
            return new List<int>();
        }

        public List<int> GetPositions(int id, string word)
        {
            if (wordsIdAndPos.ContainsKey(word) && wordsIdAndPos[word].ContainsKey(id))
                return wordsIdAndPos[word][id];
            return new List<int>();
        }

        public void Remove(int id)
        {
            foreach (var word in wordsIdAndPos.Keys)
                if (wordsIdAndPos[word].ContainsKey(id)) wordsIdAndPos[word].Remove(id);
        }
    }
}
