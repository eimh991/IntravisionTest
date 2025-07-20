using Microsoft.AspNetCore.SignalR;

namespace IntravisionTest.Hubs
{
    public class ShopHub: Hub
    {
        private static string? _currentConnectionId;

        public override async Task OnConnectedAsync()
        {
            if (_currentConnectionId == null)
            {
                _currentConnectionId = Context.ConnectionId;
                await Clients.Caller.SendAsync("AccessGranted");
            }
            else
            {
                await Clients.Caller.SendAsync("AccessDenied");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context.ConnectionId == _currentConnectionId)
            {
                _currentConnectionId = null;
                await Clients.All.SendAsync("AccessReleased");
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
