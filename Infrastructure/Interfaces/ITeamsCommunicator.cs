using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ITeamsCommunicator
    {
        Task<IEnumerable<ChatMessage>> GetAllMessagesAsync(string teamId, string channelId);
        Task SendMessageToUserAsync(string userId, string message);
        
    }
}
