namespace JOX.Assistant.Models
{
    public class ResponseJOX
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public ModeJOX Mode { get; set; } = ModeJOX.Ok;
        public string Message { get; set; } = string.Empty;
        public string Rule { get; set; } = string.Empty;

        public ResponseJOX(string message, string rule = "")
        {
            Message = message;
            Rule = rule;
        }
    }

    public enum ModeJOX
    {
        Error,
        Warning,
        Ok,
        Continue
    }
}