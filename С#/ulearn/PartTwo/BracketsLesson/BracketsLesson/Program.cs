using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BracketsLesson
{
    public class Program
    {
        public static void Main()
        {
            IsCorrectBrackets("()()[][]");
        }
        
        public static bool IsCorrectBrackets(string str)
        {
            Stack<char> stack = new Stack<char>();
            var brackets = new Dictionary<char, char>();
            brackets.Add('(', ')');
            brackets.Add('[', ']');
            foreach (var e in str)
            {
                if (brackets.ContainsKey(e)) stack.Push(e);
                else if (brackets.ContainsValue(e))
                {
                    if (stack.Count == 0 || brackets[stack.Pop()] != e) return false;
                }
                else return false;
            }
            return stack.Count == 0;
        }
    }
}
