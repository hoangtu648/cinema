using System.Collections.Concurrent;
using System.Net.WebSockets;

public static class WebSocketManager
{
    private static ConcurrentBag<WebSocket> _webSockets = new ConcurrentBag<WebSocket>();

    public static void AddClient(WebSocket webSocket)
    {
        _webSockets.Add(webSocket);
    }

    public static void RemoveClient(WebSocket webSocket)
    {
        _webSockets = new ConcurrentBag<WebSocket>(_webSockets.Except(new[] { webSocket }));
    }

    public static ConcurrentBag<WebSocket> GetAllClients()
    {
        return _webSockets;
    }
}
