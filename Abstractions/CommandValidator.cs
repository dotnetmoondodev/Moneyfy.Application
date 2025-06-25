using FluentValidation;

namespace Application.Abstractions;

public abstract class CommandValidator<T>: AbstractValidator<T> where T : BaseCommand
{
    protected CommandValidator()
    {
        RuleFor( x => x.Description )
            .NotEmpty()
            .MaximumLength( 128 )
            .WithMessage( "Description is required and must not exceed 128 characters." );

        RuleFor( x => x.Value )
            .GreaterThan( 0 )
            .WithMessage( "Value must be greater than $0." );
    }
}
