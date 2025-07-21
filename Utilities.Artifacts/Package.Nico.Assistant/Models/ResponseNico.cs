

namespace Nico.Assistant.Models
{
    public class ResponseNico
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public ModeNico Mode { get; set; }= ModeNico.Ok;
        public string Message { get; set; }=  string.Empty;
        public string Rule { get; set; } = string.Empty;

        public ResponseNico(string message,string rule="")
        {
            Message = message;
            Rule = rule;
        }
    }

    public enum ModeNico
    {
        Error,
        Warning,
        Ok,
        Continue
    }
}
