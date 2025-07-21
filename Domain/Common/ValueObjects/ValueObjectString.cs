using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Domain.Common.ValueObjects
{
    public class ValueObjectString : ValueObject
    {
        public string Valor { get; private set; }

        protected int Longitud { get; set; } = 10;

        protected ValueObjectString()
        {

        }

        protected ValueObjectString(string valor)
        {
            Valor = valor;
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
