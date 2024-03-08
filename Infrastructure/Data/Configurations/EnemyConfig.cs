using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class EnemyConfig : IEntityTypeConfiguration<Enemy>
{
    public void Configure(EntityTypeBuilder<Enemy> builder)
    {
        builder
            .HasKey(x => x.ID);

        builder
            .Property(x => x.ID)
            .UseIdentityColumn();

        builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
        builder.Property(p => p.UrlImage);
        builder.Property(p => p.HP).IsRequired();
        builder.Property(p => p.Level).IsRequired();
        builder.Property(p => p.Reward).IsRequired();
        builder.Property(p => p.Abilities).IsRequired();

        builder.ToTable("Enemy");
    }
}