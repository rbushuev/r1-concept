using Microsoft.AspNetCore.SignalR;

namespace rbushuev.Hubs
{
    public class RevitHub : Hub
    {
        public async Task Send(string command) =>
             await Clients.All.SendAsync("Received", command);
    }
}
