using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Moodle_server2._0.WebSockets
{
    public class WebSocketService
    {
        private readonly WebSocketManager wsManager;

        public WebSocketService(WebSocketManager wsm) => wsManager = wsm;

        public async Task HandleWebSocketConnection(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                var socket = await context.WebSockets.AcceptWebSocketAsync();
                wsManager.AddSocket(socket);
                await wsManager.Echo(socket);
            }
            else
                context.Response.StatusCode = 400;
        }
    }
}
