namespace LMExp.Domain;

public class Tree
{
    public Node Root { get; set; }
    public Node Current { get; set; }

    public Tree(string value)
    {
        Root = new(value);
        Current = Root;
    }
}