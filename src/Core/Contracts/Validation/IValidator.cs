using Domain.Models;

namespace Contracts.Validation;
public interface IValidator<T> where T : IModel
{
    public bool Validate(T model);
}
