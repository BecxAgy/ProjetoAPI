 
namespace IWantApp.Infra.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Product> Products { get; set; } //Transformando product em uma tabela
    public DbSet<Category> Categories { get; set; } //abilitando o acesso a tabela

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    /// <summary>
    /// this method it's useful for modeling the builder in database (MaxLength, Is Required)
    /// </summary>
    /// <param name="builder"></param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); //the father base is being called

        builder.Ignore<Notification>();
        builder.Entity<Product>()
            .Property(p => p.Name).IsRequired();
        builder.Entity<Product>()
            .Property(p => p.Description).HasMaxLength(250).IsRequired(false);
        builder.Entity<Category>()
            .Property(C => C.Name).IsRequired();    
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
    {
        configuration.Properties<string>()
            .HaveMaxLength(100);
    }

}