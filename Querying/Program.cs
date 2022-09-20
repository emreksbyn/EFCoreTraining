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

// Yapilan sorguda sadece TEK bir verinin gelmesi amaclaniyorsa Single yada SingleOrDefault fonksiyonalari kullanilir.
// Birden fazla veri geldiginde hata versin uyarsin istiyorsak kullanislidir.
#region Single

// Egerki sorgu neticesinde birden fazla veri geliyorsa yada hic gelmiyorsa exception firlatir !.

// Tek veri geldi exception yok
//var product = context.Products.Single(x => x.Id == 1);

// Hic veri gelmedi excepiton firlatti --> System.InvalidOperationException: 'Sequence contains no elements'
//var product = context.Products.Single(x => x.Id == 67);

// Birden fazla veri geldi exception firlatti --> System.InvalidOperationException: 'Sequence contains more than one element'
//var product = context.Products.Single(x => x.Id > 1);

#endregion

#region SingleOrDefault

// Default u null' dir.
// Egerki sorgu neticesinde birden fazla veri geliyorsa exception firlatir, hic veri gelmiyorsa null doner.

// Tek veri geldi exception yok
//var product = context.Products.SingleOrDefault(x => x.Id == 1);

// Hic gelmedi default olarak product null dondu.
//var product = context.Products.SingleOrDefault(x => x.Id == 67);

// Birden fazla veri geldi exception firlatti --> System.InvalidOperationException: 'Sequence contains more than one element'
//var product = context.Products.SingleOrDefault(x => x.Id > 1);

#endregion


// Sorguda tek bir veri gelsin istiyorsak First yada FirstOrDefault kullanilabilir.
// Birden fazla veri geldiginde hata vermesini istemiyorsak kullanislidir.
#region First

// Sorgu neticesinde elde edilen verilerden ilkini getirir. Veri yoksa hata firlatir.

// product da 1 tane urun vardir hata firlatmaz
//var product = context.Products.First(x => x.Id == 1);

// Products da id si 1 den buyuk baska productlarda olmasina ragmen bunlarin ilki product dadir. Hata firlatmaz
//var product = context.Products.First(x => x.Id > 1);

// Hic kayit gelmediginde exception firlatir --> System.InvalidOperationException: 'Sequence contains no elements'
//var product = context.Products.First(x => x.Id > 67);

#endregion

// Hata firlatmaz
#region FirstOrDefault

// Sorgu neticesinde elde edilen verilerden ilkini getirir. Veri yoksa null degerini doner

// product da 1 tane urun vardir hata firlatmaz
//var product = context.Products.FirstOrDefault(x => x.Id == 1);

// Products da id si 1 den buyuk baska productlarda olmasina ragmen bunlarin ilki product dadir. Hata firlatmaz
//var product1 = context.Products.FirstOrDefault(x => x.Id > 1);

// Hic kayit gelmediginde product2 nesnesi null degerinin alir
//var product2 = context.Products.FirstOrDefault(x => x.Id > 67);

#endregion


#region Find

// Find fonk. primary key kolonuna hizli bir sekilde sorgu yapmamizi saglar.
// Sadece primary key alanlarini sorulayabilir.
// Kayit bulunamazsa null doner.
// Once context icerisini kontrol eder, kaydi bulamazsa veritabanina gonderir.

// PRIMARY KEY DURUMU
//var product = context.Products.Find(1);

// COMPOSITE PRIMARY KEY DURUMU
//var product = context.ProductPieces.Find(2,2);

#endregion

#region Last

// First fonksiyonunun tam tersi davranis gosterir. OrderBy ile kullanilmasi zorunludur.
// 10 tane urun varsa 10.urun gelir.
// Hic veri gelmiyorsa hata firlatir.
//var product = context.Products.OrderBy(x => x.Id).Last(x => x.Id > 3);

#endregion

#region LastOrDefault

// FirstOrDefault fonk.nun tam tersi davranis gosterir.
// Hic veri gelmiyorsa null doner.

#endregion

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
    public DbSet<ProductPiece> ProductPieces { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.; Database=ECommerceDb; Integrated Security=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductPiece>().HasKey(pc => new { pc.ProductId, pc.PieceId });
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

public class ProductPiece
{
    public int ProductId { get; set; }
    public int PieceId { get; set; }

    public Product Product { get; set; }
    public Piece Piece { get; set; }
}