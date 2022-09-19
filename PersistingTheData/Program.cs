using Microsoft.EntityFrameworkCore;

Console.WriteLine();

#region Add Data

#endregion

#region context.AddAsync()

#endregion

#region context.DbSet.AddAsync()

#endregion

#region SaveChanges() 

#endregion

#region How Does EFCore Know When Data Needs To Be Added?

#endregion

#region Attention To When There Is More Than One Data

#endregion

#region Efficient use of SaveChanges()

#endregion

#region AddRange()

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
    public string Name { get; set; }
    public int Quantity { get; set; }
    public float Price { get; set; }
}