using System;
using System.Collections.Generic;
using System.Linq;

namespace DiskTree;

public class DiskTreeTask
{
    public class Root
    {
        public Dictionary<string, Root> Roots = new();
        public string Name;

        public Root(string name) => Name = name;

        public Root GetRoot(string root)
        {
            if (Roots.TryGetValue(root, out Root node)) return node;

            var newRoot = new Root(root);
            Roots[root] = newRoot;
            return newRoot;
        }

        public List<string> GetConclusion(int i, List<string> list)
        {
            if (i >= 0) list.Add(new string(' ', i) + Name);
            i++;

            foreach (var value in Roots.Values
                .OrderBy(root => root.Name, StringComparer.Ordinal))
            {
                list = value.GetConclusion(i, list);
            }

            return list;
        }
    }

    public static List<string> Solve(List<string> input)
    {
        var root = new Root(string.Empty);

        foreach (var name in input)
        {
            var node = root;
            var path = name.Split('\\');

            foreach (var s in path)
            {
                node = node.GetRoot(s);
            }
        }

        return root.GetConclusion(-1, new List<string>());
    }
}