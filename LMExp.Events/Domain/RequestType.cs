namespace LMExp.Events.Domain;

public enum RequestType : byte
{
    None = 0,
    Get,
    Put,
    Post,
    Delete
}