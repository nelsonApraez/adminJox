
using Microsoft.AspNetCore.SignalR;
using JOX.Assistant.Interface;
using JOX.Assistant.Models;

namespace JOX.Assistant.Socket
{
    public abstract class ChatHubBase : Hub<IHubClient>
    {
        private readonly IJOXService _JOXService;

        protected ChatHubBase(IJOXService JOXService)
        {
            this._JOXService = JOXService;     
        }
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            //cuando se conecta se envia mensaje de bienvenida
            await _JOXService.ProcessMessageStart(async(response)=> 
                {
                    await SendMensage(Context.ConnectionId, new(response,"start") );
               }
                );        
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }


        /// <summary>
        /// se reciben los mensajes del cliente
        /// </summary>
        /// <param name="callbackmg"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task OnMessageCli(ReceiveMessagePayload callbackmg)
        {
            Conversation conversation = new Conversation
            {
                id = Context.ConnectionId
            };
            conversation.PersonId = conversation.id;
            conversation.SessionId = conversation.id;
            conversation.From.Id = conversation.id;
            conversation.From.Text = Context.User.Identity.Name??"";
            conversation.TypeChannel = "hub";
            conversation.UserText = callbackmg.Message.HtmlContent;
            await _JOXService.ProcessAndValidateConvesation(conversation, async (response) =>
            {
                await SendMensage(Context.ConnectionId,response );
            }
            );
        }

        public async Task SendMensage(string idConecction, ResponseJOX message)
        {
            await Clients.Client(idConecction).ReceiveMessage(new() { UserId = idConecction, Message = new() {HtmlContent= message.Message,
                AuthorName=message.Mode.ToString(),MessageFromId=idConecction } });
        }
    }
}
