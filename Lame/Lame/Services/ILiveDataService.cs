using System;

namespace Lame.Services;

public interface ILiveDataService
{
    event Action<string> LiveDataReceived;
}