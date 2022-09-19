
using Microsoft.EntityFrameworkCore;

Console.WriteLine("");


//// If I want to migrate without Microsoft.EntityFrameworkCore.Tools
//ECommerceDbContext context = new();
//await context.Database.MigrateAsync();


// 1- Generate DbContext class
public class ECommerceDbContext : DbContext
{
    // 3- Add Entities in DbContext class with DbSet<T>
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }

    // 4- Override the OnConfiguring method. Give the database connection string
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // 5- Download "Microsoft.EntityFrameworkCore.SqlServer" if I use MS Sql Server. Than give the database connection string.
        optionsBuilder.UseSqlServer("Server=.; Database=ECommerceDb; Integrated Security=True");
    }
}



// 2- Generate Entities
// Entities
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public float Price { get; set; }
}
public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}