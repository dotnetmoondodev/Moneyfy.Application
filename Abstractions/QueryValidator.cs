using FluentValidation;

namespace Application.Abstractions;

public abstract class QueryValidator<T>: AbstractValidator<T> where T : BaseQuery
{
    protected QueryValidator()
    {
        RuleFor( x => x.Id )
            .NotEmpty()
            .WithMessage( "Id is required." );
    }
}
