using LMExp.Domain;

namespace LMExp.Infrastructure;

public class TreeContext : IContext
{
    public Tree Tree { get; set; }

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