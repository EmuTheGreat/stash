using System;


/*Решите задачу 1517 Свобода выбора за o(N^2) .Даны две строки длины N. Найдите их наибольшую общую подстроку.*/


namespace Lesson3.LD6
{
    class Program
    {
        // Значения Prime и Mod взяты так, чтобы нод был равен 1 для наименьшей вероятности коллизии
        const int Prime = 31;
        const int Mod = 1000000007;
        static long[] Primes, pref1, pref2;

        static void Main(string[] args)
        {
            var s1 = "VOTEFORTHEGREATALBANIAFORYOU";
            var s2 = "CHOOSETHEGREATALBANIANFUTURE";
            var n = s1.Length;

            // Способ с полиномиальными хэшами
            Console.WriteLine(LongestCommonSubstring1(s1, s2, n));

            // Способ динамического программирования
            Console.WriteLine(LongestCommonSubstring2(s1, s2, n));
        }

        public static void GetHashes(string s1, string s2, int n)
        {
            Primes = new long[n + 1];
            Primes[0] = 1;
            pref1 = new long[n + 1];
            pref2 = new long[n + 1];

            long hash1 = 0;
            long hash2 = 0;

            // Считаем значения степеней Prime по модулю Mod
            for (int i = 1; i <= n; i++)
            {
                Primes[i] = Primes[i - 1] * Prime % Mod;
            }

            // Считаем полиномиальный хэш для каждого префикса обеих строк по модулю
            for (int i = 0; i < n; i++)
            {
                hash1 = (hash1 + (s1[i] - 'A' + 1) * Primes[i]) % Mod;
                hash2 = (hash2 + (s2[i] - 'A' + 1) * Primes[i]) % Mod;
                pref1[i + 1] = hash1;
                pref2[i + 1] = hash2;
            }
        }

        public static string LongestCommonSubstring1(string s1, string s2, int n)
        {
            GetHashes(s1, s2, n);
            
            for (int len = n; len > 0; len--)
            {
                for (int i = 0; i <= n - len; i++)
                {
                    for (int j = 0; j <= n - len; j++)
                    {
                        if (IsEquals(i, j, len - 1))
                        {
                            if (s1.Substring(i, len) == s2.Substring(j, len)) return s1.Substring(i, len);
                        }
                    }
                }
            }
            return String.Empty;
        }

        // Проверка хэшей преффиксов
        public static bool IsEquals(int i, int j, int len)
        {
            return (pref1[i + len] - pref1[i]) * Primes[j] % Mod == (pref2[j + len] - pref2[j]) * Primes[i] % Mod;
        }

        // Динамическое программирование
        static string LongestCommonSubstring2(string s1, string s2, int n)
        {
            int[,] lcs = new int[n + 1, n + 1];
            int maxLength = 0;
            int endIndex = 0;

            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        lcs[i, j] = 0;
                    }
                    else if (s1[i - 1] == s2[j - 1])
                    {
                        lcs[i, j] = lcs[i - 1, j - 1] + 1;
                        if (lcs[i, j] > maxLength)
                        {
                            maxLength = lcs[i, j];
                            endIndex = i - 1;
                        }
                    }
                    else
                    {
                        lcs[i, j] = 0;
                    }
                }
            }

            return s1.Substring(endIndex - maxLength + 1, maxLength);
        }
    }
}