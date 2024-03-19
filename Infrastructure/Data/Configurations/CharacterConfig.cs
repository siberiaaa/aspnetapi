using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Data.Configurations;

public class CharacterConfig : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
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

        builder.Property(p => p.Level).IsRequired();
        builder.Property(p => p.UrlImage);
        builder.Property(p => p.CharacterTypeId).IsRequired();
        builder.Property(p => p.HP).IsRequired();
        builder.Property(p => p.MP).IsRequired();
        builder.Property(p => p.IQ).IsRequired();
        builder.Property(p => p.Strenght).IsRequired();
        builder.Property(p => p.Agility).IsRequired();
        builder.Property(p => p.Exp).IsRequired();

        builder
            .HasOne(x => x.CharacterType)
            .WithMany()
            .HasForeignKey(x => x.CharacterTypeId);

        builder.ToTable("Character");
    }
}