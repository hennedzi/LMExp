using LMExp.Domain;
using LMExp.Domain.Entitys;
using LMExp.Infrastructure.Extensions;

namespace LMExp.Infrastructure.Contexts;

public class TreeContext : IContext
{
    private Tree Tree { get; set; }

    public TreeContext(Tree tree)
    {
        Tree = tree;
    }

    public Task<Node> GET(string key)
    {
        if (string.IsNullOrEmpty(key)) throw new InvalidOperationException("empty key");

        return Task.FromResult(Tree.Get(key));
    }

    public Task<bool> POST(string key)
    {
        return Task.FromResult(Tree.Insert(key));
    }

    public Task<bool> PUT(string key, string newKey)
    {
        return Task.FromResult<bool>(Tree.Update(key, newKey));
    }

    public Task<bool> DELETE(string key)
    {
        return Task.FromResult(Tree.Delete(key));
    }
}