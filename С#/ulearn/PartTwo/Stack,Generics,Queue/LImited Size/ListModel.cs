using System.Collections.Generic;

namespace LimitedSizeStack
{
	public class Action<TItem>
	{
        public bool action;
		public int Index;
		public TItem Value;
        public Action(bool action, TItem value, int index = 0)
		{
			this.action = action;
			Value = value;
			Index = index;
		}
	}

    public class ListModel<TItem>
	{
		public List<TItem> Items { get; }
		public int UndoLimit;
		public LimitedSizeStack<Action<TItem>> Stack;

		public ListModel(int undoLimit) : this(new List<TItem>(), undoLimit) { }

		public ListModel(List<TItem> items, int undoLimit)
		{
            Stack = new LimitedSizeStack<Action<TItem>>(undoLimit);
            Items = items;
			UndoLimit = undoLimit;
		}

		public void AddItem(TItem item)
		{
			Items.Add(item);
			Stack.Push(new Action<TItem>(true, item));
		}

		public void RemoveItem(int index)
		{
            Stack.Push(new Action<TItem>(false, Items[index], index));
			Items.RemoveAt(index);
		}

		public bool CanUndo()
		{
			return Stack.Count != 0;
		}

		public void Undo()
		{
			var element = Stack.Pop();
			if (element.action) Items.RemoveAt(Items.Count-1);
			else Items.Insert(element.Index, element.Value);
		}
	}
}