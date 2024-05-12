using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;

namespace Server
{
    public class WebSocketManagerMiddleware
    {
        private readonly RequestDelegate _next;
        private WebSocketHandler _webSocketHandler { get; set; }

        public WebSocketManagerMiddleware(RequestDelegate next,
                                            WebSocketHandler webSocketHandler)
        {
            _next = next;
            _webSocketHandler = webSocketHandler;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)//Ellenőrizzük, hogy a kérés WebSocket kérés-e
                return;

            var socket = await context.WebSockets.AcceptWebSocketAsync();//Elfogadjuk a WebSocket kérést
            await _webSocketHandler.OnConnected(socket);//A kliens csatlakozásakor meghívjuk az OnConnected metódust

            await Receive(socket, async (result, buffer) =>//A kliens által küldött üzenetek fogadására használjuk a Receive metódust
            {
                if (result.MessageType == WebSocketMessageType.Text)//Ha a kliens szöveges üzenetet küld
                {
                    await _webSocketHandler.ReceiveAsync(socket, result, buffer);
                    return;
                }

                else if (result.MessageType == WebSocketMessageType.Close)//Ha a kliens lecsatlakozik
                {
                    await _webSocketHandler.OnDisconnected(socket);
                    return;
                }

            });
        }

        private async Task Receive(System.Net.WebSockets.WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                                                        cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }
    }
}
