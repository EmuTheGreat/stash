using System;
/* Кварталы города Фишбурга имеют квадратную форму. 
 * Их ограничивают N авеню, идущих с юга на север, и M улиц, идущих с востока на запад. 
 * Вертолёт взлетел на самом юго-западном перекрёстке и пролетел по прямой до самого северо-восточного перекрёстка. 
 * Над сколькими кварталами он пролетел?*/

namespace Lesson5.Arr6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Test(4, 3);
            Test(3, 3);
        }

        static void Test(int n , int m)
        {
            Console.WriteLine(Arr6(n - 1, m - 1));
        }

        static int Arr6(int n, int m)
        {
            if (n == m) return n;

            return m + n - 1 * GetLargestDivisor(n , m);
        }

        static int GetLargestDivisor(int n, int m)
        {
            while (n != m)
            {
                if (n > m)
                    n -= m;
                else
                    m -= n;
            }
            return n;
        }
    }
}
