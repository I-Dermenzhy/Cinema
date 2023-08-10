using Domain.Models;
using Domain.Models.Discounts;

using Microsoft.EntityFrameworkCore;

namespace Persistence.DbContexts;

public sealed class CinemaDbContext : DbContext
{
    public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
    {
        ArgumentNullException.ThrowIfNull(options, nameof(options));
    }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Discount> Discounts => Set<Discount>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Seat> Seats => Set<Seat>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<Ticket> Tickets => Set<Ticket>();

    public DbSet<SessionSeat> SessionSeats => Set<SessionSeat>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CinemaDbContext).Assembly);
}
