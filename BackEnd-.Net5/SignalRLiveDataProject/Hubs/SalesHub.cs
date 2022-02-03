using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRLiveDataProject.Hubs
{
	public class SalesHub:Hub
	{
		public async Task SendMessageAsync()
        {
			await Clients.All.SendAsync("receiveMessage", "Hello SignalR Clients");
        }
	}
}

