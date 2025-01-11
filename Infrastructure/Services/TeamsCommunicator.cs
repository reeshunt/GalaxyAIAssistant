using Azure.Identity;
using Infrastructure.Interfaces;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TeamsCommunicator : ITeamsCommunicator
    {

        private readonly GraphServiceClient _graphClient;

        public TeamsCommunicator()
        {
            //string clientId= "", 
            //tenantId= "", 
            //clientSecret = "";

            // Create a credential using Azure.Identity
            //var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

            //// Initialize the GraphServiceClient
            //_graphClient = new GraphServiceClient(credential);
        }

        // Get messages from a specific channel
        public async Task<IEnumerable<ChatMessage>> GetAllMessagesAsync(string teamId, string channelId)
        {
            var allMessages = new List<ChatMessage>();
            var messages = await _graphClient.Teams[teamId]
                .Channels[channelId]
                .Messages
                .GetAsync();

            allMessages.AddRange(messages.Value);

            // Handle pagination
            while (messages.OdataNextLink != null)
            {
                messages = await _graphClient.Teams[teamId]
                    .Channels[channelId]
                    .Messages
                    .GetAsync(requestConfig =>
                    {
                        requestConfig.QueryParameters.Top = 50; // Retrieve 50 messages at a time
                        requestConfig.QueryParameters.Skip = 0; // Start from the beginning
                    });

                allMessages.AddRange(messages.Value);
            }

            return allMessages;
        }

        public async Task SendMessageToUserAsync(string userId, string message)
        {
            // Create a chat thread with the user
            var chat = new Chat
            {
                ChatType = ChatType.OneOnOne,
                Members = new List<ConversationMember>
                {
                    new AadUserConversationMember
                    {
                        Roles = new List<string>(),
                        AdditionalData = new Dictionary<string, object>
                        {
                            {"user@odata.bind", $"https://graph.microsoft.com/v1.0/users/{userId}"}
                        }
                    }
                }
            };

            // Create the chat
            var createdChat = await _graphClient.Chats.PostAsync(chat);

            // Send a message to the chat
            //var chatMessage = new Microsoft.Graph.Chats.Item.Messages;

            //await _graphClient.Chats[createdChat.Id]
            //    .Messages
            //    .PostAsync(chatMessage);
        }
    }
}
