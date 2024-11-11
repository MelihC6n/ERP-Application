using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace ERPServer.Application.Behaviors;
public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var errorDictionary = _validators
            .Select(s => s.Validate(context))
            .SelectMany(s => s.Errors)
            .Where(s => s != null)
            .GroupBy(
            s => s.PropertyName,
            s => s.ErrorMessage, (propertyName, errorMessage) =>
            new
            {
                Key = propertyName,
                values = errorMessage.Distinct().ToArray()
            }).ToDictionary(s => s.Key, s => s.values[0]);

        if (errorDictionary.Any())
        {
            var errors = errorDictionary.Select(s => new ValidationFailure(s.Key, s.Value));

            throw new ValidationException(errors);
        }

        return await next();
    }
}
