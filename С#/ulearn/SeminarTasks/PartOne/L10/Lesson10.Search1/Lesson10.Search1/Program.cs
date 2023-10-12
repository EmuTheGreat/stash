using System;

/*Дан массив длины n. Можно потратить o(n^2) на предобработку так, чтобы потом уметь быстро, за O(log n) 
 * отвечать на запросы Find(X) — найти индекс первого вхождения элемента X в массив или -1, если такого элемента в массиве нет.*/

namespace Lesson10.Search1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var array = new int[] { 14, 64, 23, 97, 23, 43, 17, 43, 75, 34, 10}; // 10, 14, 17, 23, 23, 34, 43, 43, 64, 75, 97

            Console.WriteLine(BinSearch(BubbleSort(array), 23, -1, array.Length));
        }

        static int[] BubbleSort(int[] array)
        {
            for (int i = array.Length; i > 0; i--)
                for (int j = 0; j < array.Length - 1; j++)
                    if (array[j] > array[j+1]) (array[j], array[j+1]) = (array[j+1], array[j]);
            return array;
        }
        
        public static int BinSearch(int[] array, int value, int left, int right)
        {
            if (right - left == 0) return left;
            var m = left + (right - left) / 2;
            if (array[m] < value) return BinSearch(array, value, m + 1, right);
            return BinSearch(array, value, left, m);
        }
    }
}
