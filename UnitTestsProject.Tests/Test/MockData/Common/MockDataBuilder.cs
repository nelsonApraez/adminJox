using System;
using System.Linq.Expressions;

namespace UnitTestsProject.Tests.Test.MockData
{
    public class MockDataBuilder<T> where T : class, new()
    {
        private readonly T _entity;
        public MockDataBuilder()
        {
            _entity = new T();
        }
        public MockDataBuilder<T> With<TProperty>(Expression<Func<T, TProperty>> expresion, TProperty value)
        {
            _entity.GetType().GetProperty(((MemberExpression)expresion.Body).Member.Name).SetValue(_entity, value, null);

            return this;
        }
        public MockDataBuilder<T> With(string propertyName, dynamic value)
        {
            _entity.GetType().GetProperty(propertyName).SetValue(_entity, value, null);
            return this;
        }
        public T Build()
        {
            return _entity;
        }
    }
}
