using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace Moodle_server2._0.WebSockets
{
    public class WebSocketManager
    {
        private readonly List<WebSocket> sockets=new List<WebSocket>();

        public void AddSocket(WebSocket socket)=>sockets.Add(socket);

        public async Task RemoveSocket(WebSocket socket)
        {
            sockets.Remove(socket);
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by WebSocketManager", CancellationToken.None);
        }

        public async Task SendMessageToAllAsync(string msg)
        {
            var buffer=Encoding.UTF8.GetBytes(msg);
            var segment= new ArraySegment<byte>(buffer);

            foreach (var s in sockets.ToList())
                if (s.State == WebSocketState.Open)
                    await s.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);

        }

        public async Task Echo(WebSocket socket)
        {
            var buffer= new byte[4096];

            while(socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer),CancellationToken.None);
                
                if(result.MessageType== WebSocketMessageType.Close)
                    await RemoveSocket(socket);
                else 
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    await SendMessageToAllAsync(message);
                }
            }
        }

    }
}
