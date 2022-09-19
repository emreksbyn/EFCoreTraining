using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

ECommerceDbContext context = new();

#region Veri Nasil Silinir

//Product product = await context.Products.FirstOrDefaultAsync(x => x.Id == 1);
//context.Products.Remove(product);
//context.SaveChanges();

#endregion

#region Silme Isleminde ChangeTracker' in Rolu
// ChangeTracker, context uzerinden gelen verilerin takibinden sorumlu bir mekanizmadir. Bu takip mekanizmasi sayesinde context uzerinden gelen verilerle ilgili islemler neticesinde update yahut delete sorgularinin olusturulacagi anlasilir.
#endregion

#region Takip Edilmeyen Nesneler Nasil Silinir

//Product product = new()
//{
//    Id = 2
//};
//context.Products.Remove(product);
//context.SaveChanges();

#region EntityState Ile Silme Islemi

//Product product = new()
//{
//    Id = 3
//};
//context.Entry(product).State = EntityState.Deleted;
//context.SaveChanges();

#endregion

#endregion

#region Birden Fazla Veri Silinirken Dikkat Edilmesi Gerekenler

#region SaveChanges' i Verimli Kullanalim
// Tek tek SaveChanges() fonksiyonunu cagirmaktansa tek seferde cagirmaliyiz..
#endregion

#region RemoveRange

//var products = await context.Products.Where(x => x.Id <= 6).ToListAsync();
//context.Products.RemoveRange(products);
//await context.SaveChangesAsync();

#endregion

#region EntityState Ile Birden Fazla Veri Silme
// Silme isleminde boyle bir yetenegi yoktur !
#endregion

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