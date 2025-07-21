using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Domain.Common;
using FluentValidation;
using FluentValidation.Results;

namespace Application.Common.Validators
{
    public static class FluentValidationExtensions
    {

        public static IRuleBuilderOptions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
                this IRuleBuilder<T, TElement> ruleBuilder,
                Func<TElement, Result<TValueObject, DomainModelExceptions>> factoryMethod)
                where TValueObject : ValueObject
        {
            return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
            {
                Result<TValueObject, DomainModelExceptions> result = factoryMethod(value);

                if (result.IsFailure)
                {
                    context.AddFailure(new ValidationFailure(result.Error.PropName, result.Error.Message) { ErrorCode = result.Error.Code });
                }
            });
        }

        public static IRuleBuilderOptions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject, List<DomainModelExceptions>>> factoryMethod)
        where TValueObject : ValueObject
        {
            return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
            {
                Result<TValueObject, List<DomainModelExceptions>> result = factoryMethod(value);

                if (result.IsFailure)
                {
                    foreach (DomainModelExceptions exception in result.Error)
                    {
                        context.AddFailure(new ValidationFailure(exception.PropName, exception.Message) { ErrorCode = exception.Code });
                    }
                }
            });
        }

    }
}
