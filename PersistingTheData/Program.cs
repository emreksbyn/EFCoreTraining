using Microsoft.EntityFrameworkCore;

Console.WriteLine();


#region Add Data
//ECommerceDbContext context = new();

//Product product = new()
//{
//    ProductName = "A Product",
//    Price = 1000
//};

#region 1- context.AddAsync()
//await context.AddAsync(product);
#endregion
#region 2- context.DbSet.AddAsync()
// Type safe
//await context.Products.AddAsync(product);
#endregion

//await context.SaveChangesAsync();

#endregion

#region SaveChanges() ??

// SaveChanges; It is a function that generate insert, update and delete queries and sends them to the database with a transaction and executes them.

// If any of the generated queries fail, it rolls back all process (rollback)

#endregion

#region How Does EFCore Know When Data Needs To Be Add?

//ECommerceDbContext context = new();
//Product product = new()
//{
//    ProductName = "B Product",
//    Price = 2000
//};

//// Before added --> Detached
//Console.WriteLine(context.Entry(product).State);

//// After added --> Added
//context.Products.Add(product);
//Console.WriteLine(context.Entry(product).State);

//// After SaveChanges --> Unchanged
//context.SaveChanges();
//Console.WriteLine(context.Entry(product).State); 

#endregion

#region Attention To When There Is More Than One Data !

#region 1- Efficient use of SaveChanges()
// Don not use SaveChanges() every process end.
// If we use; Open the transaction for every process. It is bad 

//ECommerceDbContext context = new();
//Product product1 = new()
//{
//    ProductName = "C Product",
//    Price = 2000
//};

//Product product2 = new()
//{
//    ProductName = "D Product",
//    Price = 2000
//};

//Product product3 = new()
//{
//    ProductName = "E Product",
//    Price = 2000
//};


// Wrong Example 
//await context.Products.AddAsync(product1);
//await context.SaveChangesAsync();

//await context.Products.AddAsync(product2);
//await context.SaveChangesAsync();

//await context.Products.AddAsync(product3);
//await context.SaveChangesAsync();

// Better Example
//await context.Products.AddAsync(product1);
//await context.Products.AddAsync(product2);
//await context.Products.AddAsync(product3);
//await context.SaveChangesAsync();
#endregion
#region 2- AddRange()
//ECommerceDbContext context = new();
//Product product1 = new()
//{
//    ProductName = "F Product",
//    Price = 2000
//};

//Product product2 = new()
//{
//    ProductName = "G Product",
//    Price = 2000
//};

//Product product3 = new()
//{
//    ProductName = "H Product",
//    Price = 2000
//};

//await context.Products.AddRangeAsync(product1,product2,product3);
//await context.SaveChangesAsync();

#endregion

#endregion

#region I Want to Learn Generated Entity's Id
//ECommerceDbContext context = new();
//Product product1 = new()
//{
//    ProductName = "AA Product",
//    Price = 2000
//};
//await context.Products.AddAsync(product1);
//await context.SaveChangesAsync();
//Console.WriteLine(product1.Id);

#endregion

public class ECommerceDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.; Database=ECommerceDb; Integrated Security=True");
    }
}



public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public float Price { get; set; }
}