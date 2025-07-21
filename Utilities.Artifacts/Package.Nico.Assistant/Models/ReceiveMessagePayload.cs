namespace Nico.Assistant.Models
{
    public class ReceiveMessagePayload
    {
        public string UserId { get; set; }
        public ChatMessageResource Message { get; set; }
    }
}
