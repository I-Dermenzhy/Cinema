using Domain.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DbContexts.Configuration;

internal sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Email)
            .IsRequired();

        builder.HasMany(e => e.Tickets)
            .WithOne()
            .HasForeignKey("ClientId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
