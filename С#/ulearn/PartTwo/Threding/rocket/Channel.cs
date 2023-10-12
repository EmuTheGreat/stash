using System.Collections.Generic;

namespace rocket_bot;

public class Channel<T> where T : class
{
    private readonly List<T> items = new();

    public T this[int index]
    {
        get
        {
            lock (items)
            {
                if (index >= 0 && index < items.Count)
                    return items[index];
                else return null;
            }
        }
        set
        {
            lock (items)
            {
                if (index == items.Count)
                    items.Add(value);
                else if (index >= 0 && index < items.Count)
                {
                    items.RemoveRange(index, items.Count - index);
                    items.Add(value);
                }
            }
        }
    }

    public T LastItem()
    {
        lock (items)
        {
            if (items.Count > 0) return items[^1];
            else return null;
        }
    }

    public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
    {
        lock (items)
        {
            if (LastItem() == knownLastItem)
                items.Add(item);
        }
    }

    public int Count
    {
        get { lock (items) return items.Count; }
    }
}
