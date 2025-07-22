using JOX.Assistant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JOX.Assistant.Storage
{
    public interface ICosmosDbService
    {
        /// <summary>
        /// Creates a new conversation in the database.
        /// </summary>
        Task CreateConversationAsync(Conversation conversation);

        /// <summary>
        /// Updates the chats for an existing conversation.
        /// </summary>
        Task UpdateChatsAsync(Conversation conversation);

        /// <summary>
        /// Retrieves a conversation by SessionId and PersonId, including the last N chats.
        /// </summary>
        Task<Conversation> GetConversationAsync(string sessionId, string personId);        

        Task SaveConvesation(Conversation conversation, bool IsNew = false);
    }
}
