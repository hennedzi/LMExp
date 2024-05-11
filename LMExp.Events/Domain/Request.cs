using LMExp.Domain.Entitys;

namespace LMExp.Events.Domain;

public class Request
{
    public Guid Id { get; }
    public RequestType Type { get; }
    public string Key { get; }
    public string? ComputeKey { get; } = null;

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