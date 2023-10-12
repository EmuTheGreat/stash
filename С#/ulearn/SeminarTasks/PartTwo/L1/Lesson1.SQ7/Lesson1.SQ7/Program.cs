/*Версионный стек. 
 * Поддерживаются операции Push, Pop, Rollback. Состояния стека после выполнения этих операций нумеруются. 
 * С помощью Rollback можно откатиться на любое состояние, указав его номер. Rollback тоже можно откатить. 
 * Помимо этого, существует операция Forget, позволяющая забыть всю историю изменений. 
 * После Forget нумерация операций начинается с начала, Forget нельзя откатить. 
 * Все 4 операции должны работать за O(1).*/

using System;
using System.Collections.Generic;

namespace Lesson1.SQ7
{
    public class StackItem<T>
    {
        public T Value;
        public StackItem<T> Previous;

        public StackItem(T value)
        {
            Value = value;
        }
    }
    
    public class Stack<T>
    {
        public int NumberOfOperation { get; set; }
        public StackItem<T> Last { get; set; }
        public int Count { get; set; }
        public Dictionary<int, Tuple<int, StackItem<T>>> HistoryList { get; set; }

        public Stack()
        {
            Forget();
        }

        public void Push(T value)
        {
            if (Last == null) Last = new StackItem<T>(value);
            else
            {
                var item = new StackItem<T>(value);
                item.Previous = Last;
                Last = item;
            }
            HistoryList.Add(++NumberOfOperation, new Tuple<int, StackItem<T>>(Count, Last.Previous)); // Записывает предыдущий элемент для отката.
            Count++;
        }

        public T Pop()
        {
            if (Last == null) throw new InvalidOperationException("Stack is empty.");
            HistoryList.Add(++NumberOfOperation, new Tuple<int, StackItem<T>>(Count, Last)); // Записывает нынешний элемент для отката.
            var result = Last.Value;
            Last = Last.Previous;
            Count--;
            return result;
        }

        // Откатывает значения на момент выполнения операции.
        public void RollBack(int indexOfOperation)
        {
            if (HistoryList.Count == 0) throw new InvalidOperationException("History is empty.");
            HistoryList.Add(++NumberOfOperation, new Tuple<int, StackItem<T>>(Count, Last));
            var operation = HistoryList[indexOfOperation];
            Last = operation.Item2;
            Count = operation.Item1;
        }

        // Создаёт новый словарь для истории откатов. Обнуляет количество операций.
        public void Forget()
        {
            HistoryList = new Dictionary<int, Tuple<int, StackItem<T>>>();
            NumberOfOperation = 0;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var stack = new Stack<string>();

            stack.Push("1");
            stack.Push("2");
            stack.Push("3");
            stack.Pop(); // Удаляет "3" количество значений = 2              --- Операция #4

            stack.RollBack(4); //Отменяет Операцию #4, значений снова 3      --- Операция #5
            stack.RollBack(5); //Отменяет Операцию #5, занчений становится 2 --- Операция #6

            stack.Forget();
            
            stack.Push("3");
            stack.Push("4");
            stack.RollBack(2);

            Console.WriteLine(stack.Last.Value);
            Console.WriteLine(stack.Count);
        }
    }
}
