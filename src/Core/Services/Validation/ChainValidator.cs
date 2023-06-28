using Contracts.Validation;

using Domain.Models;

namespace Services.Validation;
public abstract class ChainValidator<T> : IValidator<T> where T : IModel
{
    public ChainValidator<T> SetNext(ChainValidator<T> validator)
    {
        NextValidator = validator;
        return this;
    }

    protected ChainValidator<T>? NextValidator { get; private set; }

    public virtual bool Validate(T entity) =>
        NextValidator is not null ? NextValidator.Validate(entity) : true;
}
