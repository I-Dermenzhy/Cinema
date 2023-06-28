using Domain.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DbContexts.Configuration;

internal sealed class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("Sessions");

        builder.HasKey(e => e.Id);

        builder.OwnsOne(e => e.Duration, dur =>
        {
            dur.Property(d => d.Start).IsRequired();
            dur.Property(d => d.End).IsRequired();
        });

        builder.HasOne(e => e.Movie)
           .WithMany()
           .IsRequired()
           .HasForeignKey("MovieId");
    }
}
