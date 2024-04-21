namespace LMExp.Domain;

public class NodeData(string value)
{
    public string Value { get; set; } = value;
}

public class Node(string value)
{
    public NodeData Data = new(value);
    public List<Node> Nodes { get; set; } = [];

    public void AddNode(string value)
        => Nodes.Add(new(value));

    public Node? GetNode(string value)
        => Nodes.FirstOrDefault(node => node.Data.Value == value);

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
            foreach (var n in node.Nodes)
            {
                nodes.Push(n);
            }
        }

        return result;
    }
}