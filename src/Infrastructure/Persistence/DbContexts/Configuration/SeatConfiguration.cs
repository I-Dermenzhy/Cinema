using Domain.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DbContexts.Configuration;

internal sealed class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.ToTable("Seats");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Row).IsRequired();
        builder.Property(e => e.Place).IsRequired();
        builder.Property(e => e.Category).IsRequired();
    }
}
