using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

#region How We Can Update The Data ?

//ECommerceDbContext context = new();
//Product product = await context.Products.FirstOrDefaultAsync(x => x.Id == 5);
//product.ProductName = "ZZ";
//product.Price = 67;
//await context.SaveChangesAsync();
//Console.WriteLine(product.ProductName);

#endregion

#region What is the ChangeTracker ? Shortly

// ChangeTracker, context uzerinden gelen verilerin takibinden sorumlu bir mekanizmadir. Bu takip mekanizmasi sayesinde context uzerinden gelen verilerle ilgili islemler neticesinde update yahut delete sorgularinin olusturulacagi anlasilir.
// Entity must come with context.

#endregion

#region How We Can Update The Data that Untracked ?

//ECommerceDbContext context = new();

//// "product" is not tracked with ChangeTracker() because it did not come with context object.

//Product product = new()
//{
//    Id = 5,
//    ProductName = "New",
//    Price = 111
//};

//// When entity didn't come from context, We use Update() method for entity update.

//context.Products.Update(product);
//context.SaveChanges();
//Console.WriteLine(product.ProductName);
#endregion

#region What is the EntityState ?
// Bir entity instance' inin durumunu ifade eden bir referanstir.
//ECommerceDbContext context = new();
//Product product = new Product();

//// State of product is Detached now.
//Console.WriteLine(context.Entry(product).State); 
#endregion

#region How Does EFCore Know When Data Must To Be Update ?

// With EntityState.

//ECommerceDbContext context = new();
//Product product = await context.Products.FirstOrDefaultAsync(x => x.Id == 1);
//Console.WriteLine(context.Entry(product).State);

//product.ProductName = "Apple";
//Console.WriteLine(context.Entry(product).State);

//await context.SaveChangesAsync();
//Console.WriteLine(context.Entry(product).State);


#endregion

#region Attention To When There Is More Than One Data !

//ECommerceDbContext context = new();
//var products = await context.Products.ToListAsync();
//foreach (var product in products)
//{
//    product.ProductName += "*";
//    // Bad Example.
//    //await context.SaveChangesAsync();
//}
//// Good Example
//await context.SaveChangesAsync();

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