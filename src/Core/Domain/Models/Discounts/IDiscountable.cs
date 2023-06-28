namespace Domain.Models.Discounts;

public interface IDiscountable
{
    public IList<Discount> Discounts { get; }

    public void AddDiscount(Discount discount);
    public void RemoveDiscount(Discount discount);
}
