using Core.Entities;
using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    
    public DbSet<Character> Characters { get; set; }
    public DbSet<Enemy> Enemies { get; set; }
    
    /*
    public DbSet<Bank> Banks { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemShop> ItemsShop { get; set; }
    public DbSet<Quest> Quests { get; set; }
    public DbSet<QuestTask> QuestTasks { get; set; }
    public DbSet<Reward> Rewards { get; set; }
    public DbSet<Shop> Shops { get; set; }
    */

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CharacterConfig());
        builder.ApplyConfiguration(new EnemyConfig());

        //builder.ApplyConfiguration(new BankConfig());
        //builder.ApplyConfiguration(new ItemConfig());

        //!como hago si todos estos tienen clases auxiliares. Tablas intermedias?
        //builder.ApplyConfiguration(new InventoryConfig());
        //builder.ApplyConfiguration(new ItemShopConfig());
        //builder.ApplyConfiguration(new QuestConfig());
        //builder.ApplyConfiguration(new QuestTaskConfig());
        //builder.ApplyConfiguration(new RewardConfig());
        //builder.ApplyConfiguration(new ShopConfig());
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");
}
