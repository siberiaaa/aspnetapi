using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class BankConfig : IEntityTypeConfiguration<Bank>
{
    public void Configure(EntityTypeBuilder<Bank> builder)
    {
        builder
            .HasKey(x => x.ID);

        builder
            .Property(x => x.ID)
            .UseIdentityColumn();

        builder.Property(p => p.PlayerBalance).IsRequired();
        builder.Property(p => p.Interest).IsRequired();
        builder.Property(p => p.Loan).IsRequired();
        builder.Property(p => p.SecurityLevel).IsRequired();

        builder.ToTable("Bank");
    }
}