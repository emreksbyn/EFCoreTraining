using Microsoft.EntityFrameworkCore;

Console.WriteLine();

ECommerceDbContext context = new();

#region En Temel Basit Sorgulama

#region Method Syntax

// Linq methods // Sorguyu metotlar ile saglariz. Cogunlukla bu kullanilir.
//var products = await context.Products.ToListAsync();

#endregion

#region Query Syntax

// Linq queries
//var products2 = await (from product in context.Products
//select product).ToListAsync(); 

#endregion

#endregion


#region Sorguyu Execute Etmek Icin Ne Yapmaliyiz

#region ToListAsync()

// Method Syntax
//var products = await context.Products.ToListAsync();

// Query Syntax
//var products = await (from product in context.Products
//                      select product).ToListAsync();

#endregion

#region Foreach

//var products = from product in context.Products
//               select product;

//// products IQuerayble da olsa foreach icinde IEnumerable olur ve sorgu execute edilir.
//foreach (Product product in products)
//{
//    Console.WriteLine(product.ProductName);
//}

#endregion

#region Deferred Execution (Ertelenmis Calisma)

// Deferred Execution ( Ertelenmis Calisma)
// IQueryable calismalarinda ilgili kod yazildigi noktada tetiklenmez / calistirilmaz yani ilgili kod yazildigi noktada sorguyu generate etmez ! Nerede eder ? Calistirdigi / execute edildigi noktada tetiklenir ! Iste bu duruma Deferred Execution yani Ertelenmis Calisma denir.


//int productId = 5;

//// Query burada olustruldu
//var products = from product in context.Products
//               where product.Id < productId
//               select product;

//// Query den sonra productId degeri degistirilirse degistirilmis hali ile sorgu execute edilir !!
//productId = 67;

//// Query burada execute edildi. Oncesinde productId degistirildigi icin execute edilirken son hali ile edilir !!
//foreach (Product product in products)
//{
//    Console.WriteLine(product.ProductName);
//}

//await products.ToListAsync();

#endregion

#endregion


#region IQueryable ve IEnumerable Nedir? Basit Olarak

#region IQueryable
// IQueryable :
// Sorguya karsilik gelir. EfCore uzerinden yapilmis olan sorgunun execute edilmemis halini ifade eder.
// Elimde daha veri yok sorgusal hali ifade eder.

// Su hali ile products IQueryable halindedir.
//var products = from product in context.Products
//              select product; 
#endregion

#region IEnumerable
// IEnumerable :
// Sorgunun calistirilip / execute edilip verilerin in memorye (bellege) yuklenmis halini ifade eder.

// products ToListAsync() ile birlikte execute edilir ve List<> yani IEnumerable haline gelir.
//var products = await (from product in context.Products
//                      select product).ToListAsync(); 
#endregion

#endregion


#region Cogul Veri Getiren Sorgulama Fonksiyonlari

#region ToListAsync()

// Uretilen sorguyu execute etmemizi saglar.
//var products = await context.Products.ToListAsync();

//var products = await (from product in context.Products
//select product).ToListAsync();

#endregion

#region Where

//var products = context.Products.Where(x=>x.Id > 0).ToList();

//var products = (from product in context.Products
//                where product.Id > 0
//                select product).ToList();

#endregion

#region OrderBy

// Sıralama yaptirir. Default olarak Ascending siralar.
//var products = context.Products.Where(x => x.Id > 0 && x.ProductName.StartsWith("a")).OrderBy(x => x.Id).ToList();

//var products = context.Products.Where(x => x.Id > 0 && x.ProductName.StartsWith("a")).OrderByDescending(x => x.Id).ToList();

//var products = (from product in context.Products
//                where product.Id > 0 && product.ProductName.StartsWith("a")
//                orderby product.Id ascending
//                orderby product.ProductName descending
//                select product).ToList();

#endregion

#region ThenBy

// OrderBy uzerinden yapilan siralama islemini farkli kolonlarada uygulamamizi saglar. Default Ascending

//var products = context.Products.Where(x => x.Id > 0 && x.ProductName.StartsWith("a")).OrderBy(x => x.Id).ThenBy(x => x.ProductName).ThenBy(x => x.Price).ToList();

//var products = context.Products.Where(x => x.Id > 0 && x.ProductName.StartsWith("a")).OrderByDescending(x => x.Id).ThenByDescending(x => x.ProductName).ThenByDescending(x => x.Price).ToList();

#endregion

#endregion


#region Tekil Veri Getiren Sorgulama Fonksiyonlari









#endregion


#region Diger Sorgulama Fonksiyonlari











#endregion


#region Sorgu Sonucu Donusum Fonksiyonlari










#endregion


#region GroupBy()









#endregion


#region Foreach()










#endregion

public class ECommerceDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Piece> Pieces { get; set; }

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
    public ICollection<Piece> Pieces { get; set; }
}
public class Piece
{
    public int Id { get; set; }
    public string PieceName { get; set; }
}