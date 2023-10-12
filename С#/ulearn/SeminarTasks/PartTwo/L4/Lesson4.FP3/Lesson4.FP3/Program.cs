using System;
using System.Collections.Generic;
using System.Linq;

/*Реализуйте методы. С их использованием реализуйте чтение строк с клавиатуры до тех пор, 
 * пока не будет введена пустая строка. И то же самое, но условие завершения — двойная пустая строка. 
 * Используйте замыкание для хранения флага "в прошлый раз была введена пустая строка".*/

namespace Lesson4.FP3
{
    internal class Program
    {
        public static void Main()
        {
            //Здесь используется замыкание, чтобы хранить флаг wasEmpty, который указывает, была ли предыдущая строка пустой
            var wasEmpty = false;
            Func<string, bool> shouldStop = s =>
            {
                var result = wasEmpty && string.IsNullOrEmpty(s);
                wasEmpty = string.IsNullOrEmpty(s);
                return result;
            };

            EnumerableExtensions.Generate(Console.ReadLine)
                .TakeUntil(shouldStop)
                .DoAction(x => Console.WriteLine($"c = {x}"));
        }
    }

    public static class EnumerableExtensions
    {
        //Создает бесконечную последовательность элементов, вызывая переданную ему функцию
        public static IEnumerable<T> Generate<T>(Func<T> makeNext)
        {
            while (true) yield return makeNext();
        }

        //Метод TakeUntil берет исходную последовательность и возвращает элементы из нее до тех пор,
        //пока для текущего элемента условие, заданное функцией shouldStop, не вернет true
        public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, Func<T, bool> shouldStop)
        {
            foreach (T item in source)
            {
                yield return item;
                if (shouldStop(item))
                {
                    yield break;
                }
            }
        }

        public static void DoAction<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach(var e in source) action(e);
        }
    }
}
