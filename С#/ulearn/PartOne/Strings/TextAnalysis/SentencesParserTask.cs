using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        /*private const string Delimiters = @".!?;:()";

        public static List<List<string>> ParseSentences(string text)
        {
            var ss = new List<List<string>>();
            var s = new List<string>();
            int i = 0, j = 0;
            for (; j < text.Length; j++)
                if (!char.IsLetter(text[j]) && text[j] != '\'') // конец предложения или слова
                {
                    AddWord();
                    if (Delimiters.IndexOf(text[j]) >= 0) // конец предложения
                        AddSentence();
                }
            AddWord();
            AddSentence();
            return ss;

            void AddWord() { if (i < j) s.Add(text.Substring(i, j - i).ToLower()); i = j + 1; }
            void AddSentence() { if (s.Count > 0) { ss.Add(s); s = new List<string>(); }; }
        }*/
        public static List<List<string>> ParseSentences(string text)
        {
            var sentenceList = new List<List<string>>();
            var sentenceBuilder = new StringBuilder();
            var sentenceSeparator = @".!?;:()".ToArray();
            var separator = " ".ToArray();

            foreach (var chr in text.ToLower())
            {
                if (char.IsLetter(chr) || chr == '\'') sentenceBuilder.Append(chr);
                else sentenceBuilder.Append(' ');

                if (sentenceSeparator.Contains(chr) && TextSplit(sentenceBuilder.ToString(), separator).Length != 0)
                {
                    sentenceList.Add(TextSplit(sentenceBuilder.ToString(), separator).ToList());
                    sentenceBuilder.Clear();
                }
            }
            if (TextSplit(sentenceBuilder.ToString(), separator).Length != 0)
            {
                sentenceList.Add(TextSplit(sentenceBuilder.ToString(), separator).ToList());
                sentenceBuilder.Clear();
            }
            return sentenceList;
        }

        static string[] TextSplit(string text, char[] separator)
        {
            return text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
