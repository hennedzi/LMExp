using LMExp.Domain;
using LMExp.Domain.Entitys;

namespace LMExp.Infrastructure.Extensions;

public static class NodeExtensions
{
    public static bool Exist(this Node node, string data)
    {
        if (node.Data.Value.Equals(data)) return true;
        return node.Nodes.Any(n => n.Exist(data));
    }

    public static Node? GetNode(this Node node, string value)
        => node.Nodes.FirstOrDefault(n => n.Data.Value == value);

    public static void AddNode(this Node node, string value)
        => node.Nodes.Add(new(value));

    public static List<NodeData> GetAllNodes(this Node n)
    {
        if (n.Nodes.Count == 0)
            return Enumerable.Empty<Node>() as List<NodeData>;

        List<NodeData> result = [];

        Stack<Node> nodes = new();
        nodes.Push(n.Nodes[0]);
        while (nodes.Count > 0)
        {
            var node = nodes.Pop();
            result.Add(node.Data);
            foreach (var nf in node.Nodes)
            {
                nodes.Push(nf);
            }
        }

        return result;
    }
}