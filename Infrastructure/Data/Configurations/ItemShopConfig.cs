using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class ItemShopConfig : IEntityTypeConfiguration<ItemShop>
{
    public void Configure(EntityTypeBuilder<ItemShop> builder)
    {
        builder
            .HasKey(x => x.ID);

        builder
            .Property(x => x.ID)
            .UseIdentityColumn();

        builder.Property(p => p.Item).IsRequired();
        builder.Property(p => p.Stock).IsRequired();
        builder.Property(p => p.Price).IsRequired();

        builder.ToTable("ItemShop");
    }
}