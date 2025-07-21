using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Nico.Assistant.Socket;
using System.Net;


namespace DemoIA.Rag.Bot.Controllers
{
    // This ASP Controller is created to handle a request. Dependency Injection will provide the Adapter and IBot
    // implementation at runtime. Multiple different IBot implementations running at different endpoints can be
    // achieved by specifying a more specific type for the bot constructor argument.
    [Route("api/[controller]")]
    [ApiController]
    public class NotifyServicesController : ControllerBase
    {
        private readonly IHubContext<ChatHubNico, IHubClient> _chatHubBase;

        public NotifyServicesController(IHubContext<ChatHubNico, IHubClient> chatHubBase)
        {
            _chatHubBase = chatHubBase;
        }

        [HttpPost]
        public async Task<IActionResult> Post(NotifyServicesModel responseNotify)
        {
            try
            {
                await _chatHubBase.Clients.Client(responseNotify.IdUser).ReceiveMessage(new()
                {
                    UserId = responseNotify.IdUser,
                    Message = new()
                    {
                        HtmlContent = responseNotify.Response,
                        AuthorName = responseNotify.IdUser,
                        MessageFromId = responseNotify.IdUser
                    }
                });
                // Let the caller know proactive messages have been sent
                return new ContentResult()
                {
                    Content = "<html><body><h1>Proactive messages have been sent.</h1></body></html>",
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new ContentResult()
                {
                    Content = "<html><body><h1>Proactive messages not have been sent."+ex.Message+"</h1></body></html>",
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                };
            }
        }
       
    }

    public class NotifyServicesModel
    {
        public string IdUser { get; set; }
        public string Response { get; set; }
    }
}
