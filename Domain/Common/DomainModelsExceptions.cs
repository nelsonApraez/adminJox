using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Package.Utilities.Net;

namespace Domain.Common
{
    public sealed class DomainModelExceptions : ValueObject
    {

        public string Code { get; }
        public string Message { get; }
        public string PropName { get; }

        internal DomainModelExceptions(string code, string propName, string message)
        {
            Code = code;
            PropName = propName;
            Message = message;
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }

    public static class DomainExceptions
    {

        public static class General
        {

            public static DomainModelExceptions ValueIsInvalid(string propName, string code = null) =>
                new(string.IsNullOrEmpty(code) ? $"{EnumerationMessage.Message.Formato}" : code, propName, propName);

            public static DomainModelExceptions ValueIsRequired(string propName) =>
                 ValueIsRequired(propName, propName);


            public static DomainModelExceptions ValueIsRequired(string propName, string labelName) =>
                new($"{EnumerationMessage.Message.DatoRequerido}", propName, labelName);

            public static DomainModelExceptions InvalidLength(int length = 0, string propName = null, string labelName = "")
            {
                return new DomainModelExceptions($"{EnumerationMessage.Message.Longitud}", string.IsNullOrEmpty(labelName) ? propName : labelName, $"{propName}|{length} {Constants.CharMaxLength}");
            }

        }
    }
}
