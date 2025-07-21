namespace Domain.AggregateModels.Specification
{
    using System;
    using System.Globalization;
    using System.Linq.Expressions;
    using Domain.Specification;

    public class MenuSpecification : SpecificationBase<Menu>
    {
        public static Expression<Func<Menu, bool>> GetRecordByTitle(string title)
        => x => x.Title.Equals(DateTime.Parse(title, new CultureInfo("en-US", true)));
    }
}
