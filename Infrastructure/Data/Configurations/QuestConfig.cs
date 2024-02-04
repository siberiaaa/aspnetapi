using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class QuestConfig : IEntityTypeConfiguration<Quest>
{
    public void Configure(EntityTypeBuilder<Quest> builder)
    {
        builder
            .HasKey(x => x.ID);

        builder
            .Property(x => x.ID)
            .UseIdentityColumn();

        builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
        builder.Property(p => p.Tasks).IsRequired();
        builder.Property(p => p.Reward).IsRequired();
        builder.Property(p => p.State).IsRequired();

        builder.ToTable("Quest");
    }
}