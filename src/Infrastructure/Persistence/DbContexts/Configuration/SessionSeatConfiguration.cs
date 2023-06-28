using Domain.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DbContexts.Configuration;

internal sealed class SessionSeatConfiguration : IEntityTypeConfiguration<SessionSeat>
{
    public void Configure(EntityTypeBuilder<SessionSeat> builder)
    {
        builder.ToTable("SessionSeats");

        builder.HasOne(e => e.Seat)
            .WithMany()
            .HasForeignKey("SeatId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Session)
            .WithMany()
            .HasForeignKey("SessionId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
