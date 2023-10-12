using System;

namespace LimitedSizeStack
{
    //public class DoubleLinkedListItem<T>
    //{
    //    public T? Value { get; set; }
    //    public DoubleLinkedListItem<T>? NextItem { get; set; }
    //    public DoubleLinkedListItem<T>? PreviousItem { get; set; }
    //}

    //public class DoubleLinkList<T>
    //{
    //    public int Count { get; set; }
    //    public DoubleLinkedListItem<T>? Head { get; set; }
    //    public DoubleLinkedListItem<T>? Tail { get; set; }

    //    public T? RemoveFirst()
    //    {
    //        if (Head == null) throw new InvalidOperationException();
    //        var result = Head.Value;
    //        Head = Head.NextItem;
    //        Head.PreviousItem = null;

    //        if (Head == null)
    //            Tail = null;
    //        Count--;
    //        return result;
    //    }

    //    public T? RemoveLast()
    //    {
    //        if (Tail == null) throw new InvalidOperationException();
    //        var result = Tail.Value;
    //        Tail = Tail.PreviousItem;


    //        if (Tail == null)
    //            Head = null;
    //        Count--;
    //        return result;
    //    }

    //    public void Add(T value)
    //    {
    //        if (Head == null)
    //            Tail = Head = new DoubleLinkedListItem<T>() { Value = value };
    //        else
    //        {
    //            var item = new DoubleLinkedListItem<T>() { Value = value };
    //            item.PreviousItem = Tail;
    //            Tail.NextItem = item;
    //            Tail = item;
    //        }
    //        Count++;
    //    }
    //}

    public class LimitedSizeStack<T>
    {
        T[] stack;
        int count;
        int index;
        public int Count { get => count; }

        public LimitedSizeStack(int limit) 
        { 
            stack = new T[limit]; 
        }

        public void Push(T item)
        {
            var size = stack.Length;
            if (size == 0) return;
            if (count < size) count++;
            stack[index++] = item;
            index %= size;
        }

        public T Pop()
        {
            if (stack.Length == 0) throw new InvalidOperationException();
            if (index == 0) index = stack.Length;
            count--;
            return stack[--index];
        }
    }
}
