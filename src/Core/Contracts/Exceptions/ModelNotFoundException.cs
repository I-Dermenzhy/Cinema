using Domain.Models;

namespace Contracts.Exceptions.NotFound;

public class ModelNotFoundException<T> : Exception where T : IModel
{
    public ModelNotFoundException(Guid id)
        : base($"The {typeof(T)} with the id '{id}' was not found.")
    {
    }

    public ModelNotFoundException(T model)
        : this(model.Id) => Model = model;

    public T? Model { get; init; }
}
