namespace Package.Utilities.Net
{
    public class ReponseMessageApi
    {
        public string Code { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string MessageFormat { get; set; } = string.Empty;
        public string[] Parameters { get; set; }
        public string TypeMessage { get; set; }
        public string TitleTag { get; set; }



    }
}
