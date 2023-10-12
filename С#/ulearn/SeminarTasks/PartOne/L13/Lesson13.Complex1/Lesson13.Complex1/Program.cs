using System;

/*Дан массив чисел длины N. Нужно за o-малое(N) научиться отвечать на 
* запросы вида «содержится ли элемент X в массиве где-то в диапазоне с L-го элемента по R-й включительно?». 
* Можно потратить o-малое(N^2) времени на подготовку.*/

namespace Lesson13.Complex1
{  
    class Element
    {
        public int Value;
        public int Index;

        public override string ToString()
        {
            return $"({Value}) ({Index})";
        }
    }

    internal class Program
    {
        static void Main()
        {
            var mas = new[] { 1, 4, 2, 7, 9, 9, 9, 7234, 12, 323, 12, 8, 8, 8, 8 };
            Element[] newMas = new Element[mas.Length];

            // Преобразование массива чисел в массив элементов
            for (int i = 0; i < mas.Length; i++)
                newMas[i] = new Element { Value = mas[i], Index = i };

            QuickSort(newMas, 0, newMas.Length - 1);

            Console.WriteLine(Complex1(newMas, 9, 3, 7));
            Console.WriteLine(Complex1(newMas, 8, 11, 14));

            foreach (var e in newMas) Console.WriteLine(e);
        }

        // Если искомое число есть в массиве и его индекс находится в отрезке,
        // то метод возвращает индекс первого вхожденияю, иначе -1
        static int Complex1(Element[] array, int valueToFind, int L, int R)
        {
            int left = 0;
            int right = array.Length - 1;
            while (left < right)
            {
                var middle = (right + left) / 2;
                if (valueToFind <= array[middle].Value)
                    right = middle;
                else left = middle + 1;
            }
            if (array[right].Value == valueToFind && array[right].Index >= L && array[right].Index <= R)
                return array[right].Index;
            return -1;
        }

        static void QuickSort(Element[] array, int start, int end)
        {
            if (end == start) return;
            var pivot = array[end];
            var storeIndex = start;
            for (int i = start; i <= end - 1; i++)
                if (array[i].Value <= pivot.Value)
                {
                    (array[i], array[storeIndex]) = (array[storeIndex], array[i]);
                    storeIndex++;
                }
            (array[end], array[storeIndex]) = (array[storeIndex], array[end]);
            if (storeIndex > start) QuickSort(array, start, storeIndex - 1);
            if (storeIndex < end) QuickSort(array, storeIndex + 1, end);
        }
    }
}

