using Domain.Models;
using Domain.Models.Discounts;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DbContexts.Configuration;

internal sealed class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.ToTable("Discounts");

        builder.HasKey(e => e.Id);

        builder.Property<Guid>("TicketId");

        builder.Property(e => e.Description)
            .IsRequired();

        builder.Property(e => e.Value)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.HasOne<Ticket>()
            .WithMany(t => t.Discounts)
            .HasForeignKey("TicketId");
    }
}
