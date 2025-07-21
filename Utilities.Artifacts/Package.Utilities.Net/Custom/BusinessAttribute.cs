namespace Package.Utilities.Net
{
    using System;
    /// <summary>
    /// Attribute Extend Custom para representar los repositorios de negocio
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class BusinessAttribute : Attribute
    {
        public string NameBase { get; set; }
    }
}
