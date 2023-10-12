using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTrees;

public class BinaryTree<T> : IEnumerable<T> where T : IComparable
{
    private TreeNode<T> root;
    public T this[int index]
    {
        get
        {
            if (root == null || index < 0 || index >= root.Size) throw new IndexOutOfRangeException();
            var curNode = root;
            while (true)
            {
                var sizeLeft = curNode.Left?.Size ?? 0;
                if (index == sizeLeft) return curNode.Value;
                if (index < sizeLeft) curNode = curNode.Left;
                else
                {
                    index -= 1 + sizeLeft;
                    curNode = curNode.Right;
                }
            }
        }
    }

    public void Add(T key)
    {
        var node = new TreeNode<T>(key);
        if (root == null) root = node;
        else
        {
            var curNode = root;
            while (true)
            {
                if (curNode.Value.CompareTo(key) > 0)
                {
                    if (curNode.Left == null)
                    {
                        curNode.Left = new TreeNode<T>(key);
                        break;
                    }
                    curNode = curNode.Left;
                }
                else
                {
                    if (curNode.Right == null)
                    {
                        curNode.Right = new TreeNode<T>(key);
                        break;
                    }
                    curNode = curNode.Right;
                }
            }
        }
    }

    public bool Contains(T key)
    {
        var curNode = root;
        while (curNode != null)
        {
            var с = curNode.Value.CompareTo(key);
            if (с == 0) return true;
            curNode = с < 0 ? curNode.Right : curNode.Left;
        }
        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        if (root == null) return Enumerable.Empty<T>().GetEnumerator();
        return root.GetValues().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}

public class TreeNode<T> where T : IComparable
{
    private TreeNode<T> parentNode;
    private TreeNode<T> right;
    private TreeNode<T> left;
    public T Value { get; }
    public int Size { get; private set; }

    public TreeNode(T value)
    {
        Value = value;
        Size = 1;
    }

    public TreeNode<T> Right
    {
        get => right;
        set
        {
            if (right != null) ChangeSize(-right.Size);
            if (value != null)
            {
                ChangeSize(value.Size);
                value.parentNode = this;
            }
            right = value;
        }
    }

    public TreeNode<T> Left
    {
        get => left;
        set
        {
            if (left != null) ChangeSize(-left.Size);
            if (value != null)
            {
                ChangeSize(value.Size);
                value.parentNode = this;
            }
            left = value;
        }
    }
    public void ChangeSize(int delta)
    {
        parentNode?.ChangeSize(delta);
        Size += delta;
    }

    public IEnumerable<T> GetValues()
    {
        if (Left != null)
        {
            foreach (var value in Left.GetValues())
                yield return value;
        }

        yield return Value;

        if (Right != null)
        {
            foreach (var value in Right.GetValues())
                yield return value;
        }
    }
}
