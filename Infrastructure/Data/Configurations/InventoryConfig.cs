using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class InventoryConfig : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder
            .HasKey(x => x.ID);

        builder
            .Property(x => x.ID)
            .UseIdentityColumn();

        builder.Property(p => p.Capacity).IsRequired();
        builder.Property(p => p.Items).IsRequired();
        builder.Property(p => p.Weight).IsRequired();

        builder.ToTable("Inventory");
    }
}