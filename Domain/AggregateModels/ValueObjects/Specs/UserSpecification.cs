namespace Domain.AggregateModels.Specification
{
    using System;
    using System.Globalization;
    using System.Linq.Expressions;
    using Domain.Specification;

    public class UserSpecification : SpecificationBase<User>
    {
        public static Expression<Func<User, bool>> GetRecordByUsername(string username)
        => x => x.Username.Equals(DateTime.Parse(username, new CultureInfo("en-US", true)));
    }
}
