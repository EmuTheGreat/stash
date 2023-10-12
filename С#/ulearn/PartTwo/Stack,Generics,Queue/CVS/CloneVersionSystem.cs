using System.Collections.Generic;

namespace Clones;

//public class StackItem<T>
//{
//    public T Value;
//    public StackItem<T> Previous;
//    public StackItem(T value)
//    {
//        Value = value;
//    }
//}

//public class Stack<T>
//{
//    public StackItem<T> Last;
//    public int Count;

//    public Stack() { }

//    public Stack(Stack<T> stack)
//    {
//        Last = stack.Last;
//        Count = stack.Count;
//    }

//    public void Push(T value)
//    {
//        if (Last == null) Last = new StackItem<T>(value);
//        else
//        {
//            var item = new StackItem<T>(value);
//            item.Previous = Last;
//            Last = item;
//        }
//        Count++;
//    }

//    public T Pop()
//    {
//        var result = Last.Value;
//        Last = Last.Previous;
//        Count--;
//        return result;
//    }
//    public T Peek() => Last.Value;
//}

public class Clone
{
    public Stack<string> Programs;
    public Stack<string> RemoveList;

    public Clone()
    {
        Programs = new Stack<string>();
        RemoveList = new Stack<string>();
    }

    public Clone(Clone clone)
    {
        Programs = new Stack<string>(clone.Programs);
        RemoveList = new Stack<string>(clone.RemoveList);
    }

    public void Learn(string value)
    {
        Programs.Push(value);
    }

    public void RollBack()
    {
        RemoveList.Push(Programs.Pop());
    }

    public void ReLearn()
    {
        Programs.Push(RemoveList.Pop());
    }

    public string Check()
    {
        if (Programs.Count == 0) return "basic";
        return Programs.Peek();
    }
}

public class CloneVersionSystem : ICloneVersionSystem
{
    List<Clone> CloneList;

    public CloneVersionSystem()
    {
        CloneList = new List<Clone>() { new Clone() };
    }

    public string Execute(string query)
    {
        var values = query.Split(' ');
        var clone = CloneList[int.Parse(values[1]) - 1];
        switch (values[0])
        {
            case "learn":
                clone.Learn(values[2]);
                break;
            case "rollback":
                clone.RollBack();
                break;
            case "relearn":
                clone.ReLearn();
                break;
            case "check":
                return clone.Check();
            case "clone":
                CloneList.Add(new Clone(clone));
                break;
            default:
                return null;
        }
        return null;
    }
}
//using System.Collections.Generic;

//namespace Clones
//{
//    public class Clone
//    {
//        private Stack<string> _programs = new Stack<string>();
//        private Stack<string> _removeList = new Stack<string>();

//        public void Learn(string value) => _programs.Push(value);
//        public void RollBack() => _removeList.Push(_programs.Pop());
//        public void ReLearn() => _programs.Push(_removeList.Pop());
//        public string Check() => _programs.Count > 0 ? _programs.Peek() : "basic";
//        public Clone CloneMe() => new Clone { _programs = new Stack<string>(_programs), _removeList = new Stack<string>(_removeList) };
//    }

//    public class CloneVersionSystem : ICloneVersionSystem
//    {
//        private readonly List<Clone> _clones = new List<Clone> { new Clone() };
//        public string Execute(string query)
//        {
//            var queryArgs = query.Split(' ');
//            var cloneIndex = int.Parse(queryArgs[1]) - 1;
//            var clone = _clones[cloneIndex];
//            switch (queryArgs[0])
//            {
//                case "learn":
//                    clone.Learn(queryArgs[2]);
//                    break;
//                case "rollback":
//                    clone.RollBack();
//                    break;
//                case "relearn":
//                    clone.ReLearn();
//                    break;
//                case "check":
//                    return clone.Check();
//                case "clone":
//                    _clones.Add(clone.CloneMe());
//                    break;
//                default:
//                    return null;
//            }
//            return null;
//        }
//    }
//}