using System;

namespace Package.Utilities.Net
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class BusinessDaoAttribute : Attribute
    {
        public string NameBase { get; set; }
    }
}
