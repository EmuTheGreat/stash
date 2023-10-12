using System;
/*Дана строка из символов '(' и ')'. Определить, является ли она корректным скобочным выражением. 
 * Определить максимальную глубину вложенности скобок.
*/

namespace Lesson4.Loops5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Loops5("()()()(");
            Loops5("((()))");
            Loops5("()()()(())");
        }
        static void Loops5(string line)
        {
            int maxDeep = 0;
            int deep = 0;
            int countSymbol = 0;
            bool flag = true;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '(')
                {
                    deep++;
                    countSymbol++;
                }
                
                else
                {
                    if (deep > maxDeep) maxDeep = deep;
                    countSymbol--;
                    deep = 0;
                }
                
                if (countSymbol < 0 || line[line.Length - 1] == '(')
                {
                    flag = false;
                    break;
                }
            }
            if (flag) Console.WriteLine($"Строка введена корректно. Максимальная глубина {maxDeep}.");
            else Console.WriteLine("Строка введена не корректно.");
        }
    }
}
