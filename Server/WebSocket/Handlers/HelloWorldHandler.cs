using System.Net.WebSockets;
using System.Text;

namespace Server
{
    public class HelloWorldHandler : WebSocketHandler
    {
        public HelloWorldHandler(ConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
        }

        public override async Task ReceiveAsync(System.Net.WebSockets.WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var message = $"Ezt küldte a kliens: {Encoding.UTF8.GetString(buffer, 0, result.Count)}";

            await SendMessageToAllAsync(message);//A kliens által küldött üzenetet visszaküldjük az összes csatlakozott kliensnek
        }
    }
}
