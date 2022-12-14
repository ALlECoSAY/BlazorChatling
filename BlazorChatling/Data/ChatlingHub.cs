using BlazorChatling.Data.DBContextFiles;
using Microsoft.AspNetCore.SignalR;

namespace BlazorChatling.Data
{
    public class ChatlingHub : Hub
    {
        public const string HubUrl = "/chatx";

        public async Task Broadcast(Users user, Messages message, TypeOfMessage typeOfMessage) {
            await Clients.All.SendAsync("Broadcast", user, message, typeOfMessage);
        }

        public override Task OnConnectedAsync() {
            Console.WriteLine($"{Context.ConnectionId} connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e) {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            await base.OnDisconnectedAsync(e);
        }

    }


    public enum TypeOfMessage {
        edit,
        delete,
        write
    }

}
