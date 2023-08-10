using Domain.Models;

using Microsoft.Extensions.Logging;

namespace Services.Validation;

public interface IValidationChain<T, USelf>
    where T : IModel
    where USelf : IValidationChain<T, USelf>
{
    public USelf AddValidator(ChainValidator<T> validator);
    public ChainValidator<T> Build();

    public ChainValidator<T> BuildDefault();
    public ChainValidator<T> BuildDefault(ILogger logger);
}