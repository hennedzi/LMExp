namespace LMExp.Domain.Entitys;

public class Request
{
    public Guid Id;
    public RequestType Type { get; }
    public string Key;
    public string? ComputeKey = null;

    public override bool Equals(object? obj)
    {
        if (obj is Request request)
        {
            return Id == request.Id;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}