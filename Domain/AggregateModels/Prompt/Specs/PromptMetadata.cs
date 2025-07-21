using Domain.Common;
namespace Domain.AggregateModels
{
    /// <summary>
    /// Esta clase representa la Implementacion la metadata para la Entidad (Prompt)
    /// </summary>
    public static class PromptMetadata 
    { 
       public static Domain.Common.Metadata Nombre => new(nameof(Prompt.Nombre), nameof(Prompt.Nombre), 100);
       public static Domain.Common.Metadata Promtuser => new(nameof(Prompt.Promtuser), nameof(Prompt.Promtuser), 10000);
       public static Domain.Common.Metadata Promtsystem => new(nameof(Prompt.Promtsystem), nameof(Prompt.Promtsystem), 10000);
       public static Domain.Common.Metadata Tags => new(nameof(Prompt.Tags), nameof(Prompt.Tags), 100);
       public static Domain.Common.Metadata Folder => new(nameof(Prompt.Folder), nameof(Prompt.Folder), 100);
    }
}
