using System;
/*Как избавиться от переполнения int-а в бинарном поиске в строке var middle = (left+right) / 2;*/

namespace Lesson10.Search0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[] { 1, 2, 3, 4 };
            Console.WriteLine(BinSearchLeftBorder(arr, 4, -1, arr.Length));
        }

        public static int BinSearchLeftBorder(int[] array, int value, int left, int right)
        {
            if (right - left == 1) return left;
            var m = left + (right - left) / 2; //  <=
            if (array[m] < value) return BinSearchLeftBorder(array, value, m, right);
            return BinSearchLeftBorder(array, value, left, m);
        }
    }
}
