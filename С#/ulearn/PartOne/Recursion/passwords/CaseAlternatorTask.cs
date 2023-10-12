using System.Collections.Generic;

namespace Passwords
{
    public class CaseAlternatorTask
    {
        public static List<string> AlternateCharCases(string lowercaseWord)
        {
            var passwords = new List<string>();
            FindPassword(lowercaseWord.ToCharArray(), 0, passwords);
            return passwords;
        }

        static void FindPassword(char[] word, int position, List<string> passwords)
        {
            var wordLength = word.Length;
            if (position == wordLength) { passwords.Add(new string(word)); return; }

            if (word[position] != char.ToUpper(word[position]) && char.IsLetter(word[position]))
            {
                FindPassword(word, position + 1, passwords);
                word[position] = char.ToUpper(word[position]);
            }

            FindPassword(word, position + 1, passwords);
            word[position] = char.ToLower(word[position]);
        }
    }
}