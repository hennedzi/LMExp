namespace LMExp.Domain.Entitys;

public class Node
{
    public NodeData Data { get; set; }
    public List<Node> Nodes { get; set; } = [];

    public Node(string value)
    {
        Data = new NodeData(value);
    }
}