/*Построить полиномиальный хэш на битовых операциях <<, >>, |, &, ^ вместо умножения и сложения.
*/

using System;

namespace Lesson3.LD8
{
    class Program
    {
        static void Main()
        {
            var s = "ABCDEabcde";
            Console.WriteLine(PolynomialHash.GetPolynomialHash(s));
        }
    }

    class PolynomialHash
    {
        public static uint GetPolynomialHash(string s)
        {
            uint hash = 0;
            for (int i = 0; i < s.Length; i++)
            {
                uint t = s[i];
                hash ^= (((t << (i & 15)) | (t >> (16 - (i & 15)))) & 255) ^ (hash << 1) ^ (hash >> 31);
            }
            return hash;
        }
    }
}
