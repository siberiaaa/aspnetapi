using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Data.Configurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(x => x.ID);

        builder
            .Property(x => x.ID)
            .UseIdentityColumn();

        builder
            .Property(p => p.Username)
            .IsRequired()
            .HasMaxLength(255);
        
        builder
            .Property(p => p.Password)
            .IsRequired()
            .HasMaxLength(255);

        builder.ToTable("Users");
    }
}