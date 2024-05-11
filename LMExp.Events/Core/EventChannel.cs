using System.Threading.Channels;
using LMExp.Domain.Entitys;

namespace LMExp.Events.Events;

public class EventChannel : IDisposable
{
    public Task Completion => _channel.Reader.Completion;

    public event Action<Request> Subscribers = null!;

    private readonly Channel<Request> _channel = Channel.CreateUnbounded<Request>();

    internal class EventSubscription : IDisposable
    {
        private readonly Action<Request> _subscribe;
        private readonly Action _unsubscribe;

        private readonly EventChannel _channel;

        public EventSubscription(
            EventChannel channel,
            Action<Request> subscribe,
            Action unsubscribe)
        {
            _subscribe = subscribe;
            _unsubscribe = unsubscribe;

            _channel = channel;
            _channel.Subscribers += _subscribe;
        }

        public void Dispose()
        {
            _channel.Subscribers -= _subscribe;
            _unsubscribe();
        }
    }

    public IDisposable Subscribe(Action<Request> subscribe, Action unsubscribe)
    {
        return new EventSubscription(this, subscribe, unsubscribe);
    }

    public bool Post(Request item)
    {
        Subscribers?.Invoke(item);
        return _channel.Writer.TryWrite(item);
    }

    public IAsyncEnumerable<Request> ToAsyncEnumerable(CancellationToken token)
    {
        return _channel.Reader.ReadAllAsync(token);
    }

    public void Dispose()
    {
        _channel.Writer.Complete();
    }
}