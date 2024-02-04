using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class QuestTaskConfig : IEntityTypeConfiguration<QuestTask>
{
    public void Configure(EntityTypeBuilder<QuestTask> builder)
    {
        builder
            .HasKey(x => x.ID);

        builder
            .Property(x => x.ID)
            .UseIdentityColumn();

        builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
        builder.Property(p => p.Description).IsRequired();
        builder.Property(p => p.Done).IsRequired();

        builder.ToTable("QuestTask");
    }
}