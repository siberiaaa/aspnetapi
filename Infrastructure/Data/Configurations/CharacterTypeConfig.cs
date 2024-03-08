using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Data.Configurations;

public class CharacterTypeConfig : IEntityTypeConfiguration<CharacterType>
{
    public void Configure(EntityTypeBuilder<CharacterType> builder)
    {
        builder
            .HasKey(x => x.ID);
        //Has key Devuelve key builder

        //Esta configuracion devuelve property builder 
        //Y necesitamos ambos retornos, pero juntos no funcionan
        //así que toca separao.
        builder
            .Property(x => x.ID)
            .UseIdentityColumn();

        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.Name).IsRequired();

        builder
            .HasMany(x => x.Characters)
            .WithOne(x => x.CharacterType).HasForeignKey(x => x.CharacterTypeId);
   

        builder.ToTable("CharacterType");
    }
}