using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TestTask.Services;
using TestTask.Services.Common;

namespace TestTask.Controllers;

[Route("ws")]
[ApiController]
public class UserController(IServiceProvider serviceProvider) : ControllerBase
{
    [HttpGet]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await Subscribe(webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = 400;
        }
    }

    private async Task Subscribe(WebSocket socket)
    {
        var buffer = new byte[1024 * 4];

        while (socket.State == WebSocketState.Open)
        {
            var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client", CancellationToken.None);
            }
            else
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                string responseMessage;
                try
                {
                    responseMessage = await HandleMessageAsync(message);

                }
                catch (Exception validationException)
                {
                    responseMessage = validationException.Message;
                }
                
                var response = Encoding.UTF8.GetBytes($"Result: {responseMessage}");

                await socket.SendAsync(new ArraySegment<byte>(response), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }

    private async Task<string> HandleMessageAsync(string message)
    {
        var dto = message.ParseToUpdateUserDto();
        using var scope = serviceProvider.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<UserService>();
        var result = await userService.UpdateUser(dto);
        
        return JsonSerializer.Serialize(result);
    }
}