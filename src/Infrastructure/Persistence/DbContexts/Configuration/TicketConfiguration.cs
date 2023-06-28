using Domain.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DbContexts.Configuration;
internal sealed class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");

        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.Client)
            .WithMany()
            .IsRequired()
            .HasForeignKey(e => e.ClientId);

        builder.HasOne(e => e.Session)
            .WithMany()
            .IsRequired()
            .HasForeignKey(e => e.SessionId);

        builder.HasOne(e => e.Seat)
            .WithMany()
            .IsRequired()
            .HasForeignKey(e => e.SeatId);

        builder.HasMany(e => e.Discounts)
            .WithOne()
            .HasForeignKey("TicketId");
    }
}
