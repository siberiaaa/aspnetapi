using Core.Entities;
using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    
    public DbSet<Character> Characters { get; set; }
    public DbSet<Enemy> Enemies { get; set; }
    public DbSet<CharacterType> CharacterTypes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CharacterConfig());
        builder.ApplyConfiguration(new EnemyConfig());
        builder.ApplyConfiguration(new CharacterTypeConfig());

    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");
}
