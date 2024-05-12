using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            if (Encoding.UTF8.GetString(buffer, 0, result.Count).Substring(0, 11) != "AddFavTopic") {
                var message = $"Ezt küldte a kliens: {Encoding.UTF8.GetString(buffer, 0, result.Count)}";

                await SendMessageToAllAsync(Encoding.UTF8.GetString(buffer, 0, result.Count).Substring(0, 10));//A kliens által küldött üzenetet visszaküldjük az összes csatlakozott kliensnek
            } else if (Encoding.UTF8.GetString(buffer, 0, result.Count).Substring(0, 11) == "AddFavTopic")
            {
                var message = $"A(z){Encoding.UTF8.GetString(buffer, 0, result.Count).Substring(11,1)} topicot kedvencekbe tette a(z) {Encoding.UTF8.GetString(buffer, 0, result.Count).Substring(13, 1)}-es felhasználó";

                await SendMessageToAllAsync(message);//A kliens által küldött üzenetet visszaküldjük az összes csatlakozott kliensnek
            }
        }
    }
}
