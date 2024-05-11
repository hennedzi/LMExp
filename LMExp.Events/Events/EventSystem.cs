using LMExp.Domain;
using LMExp.Domain.Entitys;

namespace LMExp.Events.Events;

public class EventSystem
{
    public readonly EventChannel Channel = new();

    private readonly IContext _context;
    
    private Dictionary<Request, object> _results = [];
    
    private readonly SemaphoreSlim _sync = new(1, 1);

    public EventSystem(IContext context)
    {
        _context = context;
    }
    
    private async Task HandleResult(Request request, object result)
    {
        try
        {
            await _sync.WaitAsync();
            _results.TryAdd(request, result);
        }
        catch (Exception e)
        {
            _sync.Release();
        }
    }

    public object? TryGetResult(in Request signal)
    {
        if (_results.TryGetValue(signal, out object result))
            return result;
        return null;
    }

    public async Task<bool> DispatchEvents()
    {
        await foreach (var request in Channel.ToAsyncEnumerable(CancellationToken.None))
        {
            switch (request.Type)
            {
                case RequestType.Get:
                    if (!_results.TryGetValue(request, out _))
                    {
                        var result = await _context.GET(request.Key);
                        await HandleResult(request, result);
                    }
                    break;

                case RequestType.Put:
                    await _context.PUT(request.Key, request.ComputeKey);
                    break;

                case RequestType.Post:
                    await _context.POST(request.Key);
                    break;

                case RequestType.Delete:
                    await _context.DELETE(request.Key);
                    break;

                default:
                    break;
            }
        }

        return Channel.Completion.IsCompletedSuccessfully;
    }
}