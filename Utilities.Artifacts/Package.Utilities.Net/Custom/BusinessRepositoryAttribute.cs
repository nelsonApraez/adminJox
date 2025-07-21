using System;

namespace Package.Utilities.Net
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class BusinessRepositoryAttribute : Attribute
    {
        public string NameBase { get; set; }
    }
}
