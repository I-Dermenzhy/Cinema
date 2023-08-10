using Contracts.Exceptions.NotFound;
using Contracts.Repositories;

using Domain.Filters;
using Domain.Models.Discounts;

using Microsoft.EntityFrameworkCore;

using Persistence.DbContexts;
using Persistence.Extensions.Linq;

namespace Persistence.Repositories;

public sealed class DiscountRepository : IDiscountRepository
{
    private readonly CinemaDbContext _dbContext;

    public DiscountRepository(CinemaDbContext dbContext) =>
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task<IEnumerable<Discount>> GetAllAsync() =>
        await _dbContext.Discounts.ToListAsync();

    public async Task<Discount> GetByIdAsync(Guid id)
    {
        return await _dbContext.Discounts.FindAsync(id) ??
               throw new ModelNotFoundException<Discount>(id);
    }

    public async Task<IEnumerable<Discount>> GetByFiltersAsync(DiscountFilters filters)
    {
        var query = _dbContext.Discounts.AsQueryable();
        query = query.ApplyFilters(filters);
        return await query.ToListAsync();
    }

    public async Task<Guid> InsertAsync(Discount discount)
    {
        ArgumentNullException.ThrowIfNull(discount, nameof(discount));

        _dbContext.Discounts.Add(discount);
        await _dbContext.SaveChangesAsync();

        return discount.Id;
    }

    public async Task RemoveAsync(Guid id)
    {
        var discount = await _dbContext.Discounts.FindAsync(id);

        if (discount is not null)
            await RemoveAsync(discount);
    }

    public async Task RemoveAsync(Discount discount)
    {
        ArgumentNullException.ThrowIfNull(discount, nameof(discount));

        _dbContext.Discounts.Remove(discount);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Discount discount)
    {
        ArgumentNullException.ThrowIfNull(discount, nameof(discount));

        if (!_dbContext.Discounts.Contains(discount))
            throw new ModelNotFoundException<Discount>(discount);

        await _dbContext.SaveChangesAsync();
    }
}
