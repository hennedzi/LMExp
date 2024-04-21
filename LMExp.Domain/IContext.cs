namespace LMExp.Domain;

public interface IContext
{
    public Task<Node> GET(string key);
    public Task<bool> POST(string key);
    public Task<bool> PUT(string key, string newKey);
    public Task<bool> DELETE(string key);
}