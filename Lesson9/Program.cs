using Microsoft.EntityFrameworkCore;

Console.WriteLine("");

ECommerceDbContext context = new();
await context.Database.MigrateAsync();

public class ECommerceDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Provider

        // ConnectionString        
        optionsBuilder.UseSqlServer("Server=.; Database=ECommerceDb; Integrated Security=True");

        // Lazy Loading ...
    }
}

public class Product
{
    public int Id { get; set; } // OR
    //public int ID { get; set; }
    //public int ProductId { get; set; }
    //public int ProductID { get; set; }
}

#region OnConfiguring() Method
// We use OnConfiguring method to manipulate the EfCore.Tool with
// It is an override method --> protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#endregion

#region Basic Entity Identification Rules
// Every Entity must have Primary Key in EfCore. --> public int Id { get; set; }
#endregion

#region Defination of Table Name
//  public DbSet<Product> Products { get; set; } --> Products is Table name in Database
#endregion