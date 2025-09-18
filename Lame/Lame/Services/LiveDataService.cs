using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lame.Services;

public class LiveDataService : ILiveDataService
{
    private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(2);
    private string _data = "Initial Data";
    private CancellationTokenSource? _cancellationTokenSource;
    
    public event Action<string>? LiveDataReceived;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        while (!_cancellationTokenSource.Token.IsCancellationRequested)
        {
            await Task.Delay(_updateInterval, _cancellationTokenSource.Token);

            _data = $"Updated Data: {DateTime.Now:HH:mm:ss}";
            LiveDataReceived?.Invoke(_data);
        }
    }

    public void Stop()
    {
        _cancellationTokenSource?.Cancel();
    }

}