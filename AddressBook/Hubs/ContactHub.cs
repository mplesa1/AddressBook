using AddressBook.Shared.DataTransferObjects.Contact;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBook.Api.Hubs
{
    public class ContactHub : Hub
    {
        public async Task SendMessage(string message, ICollection<ContactDto> contacts)
        {
            await Clients.All.SendAsync("ReceiveMessage", message,  contacts);
        }
    }
}
