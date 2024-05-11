namespace LMExp.Domain.Entitys;

public class Node
{
    public NodeData Data { get; set; }
    public List<Node> Nodes { get; set; } = [];

    public Node(string value)
    {
        Data = new NodeData(value);
    }

    public bool Exist(string data)
    {
        if (Data.Value.Equals(data)) return true;
        return Nodes.Exists(n => n.Exist(data));
    }

    public Node? GetNode(string value)
        => Nodes.Find(n => n.Data.Value == value);

    public void AddNode(string value)
        => Nodes.Add(new(value));

    public List<NodeData> GetAllNodes()
    {
        if (Nodes.Count == 0)
            return Enumerable.Empty<Node>() as List<NodeData>;

        List<NodeData> result = [];

        Stack<Node> nodes = new();
        nodes.Push(Nodes[0]);
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