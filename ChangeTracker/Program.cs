
using Microsoft.EntityFrameworkCore;
ECommerceDbContext context = new();

#region Change Tracking Neydi?
//Context nesnesi üzerinden gelen tüm nesneler/veriler otomatik olarak bir takip mekanizması tarafından izlenirler. İşte bu takip mekanizmasına Change Tracker denir. Change Traker ile nesneler üzerindeki değişiklikler/işlemler takip edilerek netice itibariyle bu işlemlerin fıtratına uygun sql sorgucukları generate edilir. İşte bu işleme de Change Tracking denir. 
#endregion

#region ChangeTracker Propertysi
//Takip edilen nesnelere erişebilmemizi sağlayan ve gerektiği taktirde işlemler gerçekşetirmemizi sağlayan bir propertydir.
//Context sınıfının base class'ı olan DbContext sınıfının bir member'ıdır.

//var products = await context.Products.ToListAsync();
//products[6].Price = 123; //Update
//context.Products.Remove(products[7]); //Delete
//products[8].ProductName = "asdasd"; //Update


//var datas = context.ChangeTracker.Entries();

//await context.SaveChangesAsync();
//Console.WriteLine();
#endregion

#region DetectChanges Metodu
//EF Core, context nesnesi tarafından izlenen tüm nesnelerdeki değişiklikleri Change Tracker sayesinde takip edebilmekte ve nesnelerde olan verisel değişiklikler yakalanarak bunların anlık görüntüleri(snapshot)'ini oluşturabilir.
//Yapılan değişikliklerin veritabanına gönderilmeden önce algılandığından emin olmak gerekir. SaveChanges fonksiyonu çağrıldığı anda nesneler EF Core tarafından otomatik kontrol edilirler. Normalde SaveChanges zaten DetectChanges i tetikliyor. Incelikli durumlar haricinde DetectChanges metodunu kullanmak maliyetlidir. 
//Ancak, yapılan operasyonlarda güncel tracking verilerinden emin olabilmek için değişişiklerin algıulanmasını opsiyonel olarak gerçekleştirmek isteyebiliriz. İşte bunun için DetectChanges fonksiyonu kullanılabilir ve her ne kadar EF Core değişikleri otomatik algılıyor olsa da siz yine de iradenizle kontrole zorlayabilirsiniz.

//var product = await context.Products.FirstOrDefaultAsync(u => u.Id == 3);
//product.Price = 123;

//context.ChangeTracker.DetectChanges();
//await context.SaveChangesAsync();

#endregion

#region AutoDetectChangesEnabled Property'si
//İlgili metotlar(SaveChanges, Entries) tarafından DetectChanges metodunun otomatik olarak tetiklenmesinin konfigürasyonunu yapmamızı sağlayan proeportydir.
//SaveChanges fonksiyonu tetiklendiğinde DetectChanges metodunu içerisinde default olarak çağırmaktadır. Bu durumda DetectChanges fonksiyonunun kullanımını irademizle yönetmek ve maliyet/performans optimizasyonu yapmak istediğimiz durumlarda AutoDetectChangesEnabled özelliğini kapatabiliriz.
#endregion

#region Entries Metodu
//Context'te ki Entry metodunun koleksiyonel versiyonudur.
//Change Tracker mekanizması tarafından izlenen her entity nesnesinin bigisini EntityEntry türünden elde etmemizi sağlar ve belirli işlemler yapabilmemize olanak tanır.
//Entries metodu, DetectChanges metodunu tetikler. Bu durum da tıpkı SaveChanges'da olduğu gibi bir maliyettir. Buradaki maliyetten kaçınmak için AuthoDetectChangesEnabled özelliğine false değeri verilebilir.

//var products = await context.Products.ToListAsync();
//products.FirstOrDefault(u => u.Id == 7).Price = 123; //Update
//context.Products.Remove(products.FirstOrDefault(u => u.Id == 8)); //Delete
//products.FirstOrDefault(u => u.Id == 9).ProductName = "asdasd"; //Update

//context.ChangeTracker.Entries().ToList().ForEach(e =>
//{

//    if (e.State == EntityState.Unchanged)
//    {
//        //:..
//    }
//    else if (e.State == EntityState.Deleted)
//    {
//        //...
//    }
//    //...
//});
#endregion

#region AcceptAllChanges Metodu
//SaveChanges() veya SaveChanges(true) tetiklendiğinde EF Core herşeyin yolunda olduğunu varsayarak track ettiği verilerin takibini keser yeni değişikliklerin takip edilmesini bekler. Böyle bir durumda beklenmeyen bir durum/olası bir hata söz konusu olursa eğer EF Core takip ettiği nesneleri brakacağı için bir düzeltme mevzu bahis olamayacaktır.

//Haliyle bu durumda devreye SaveChanges(false) ve AcceptAllChanges metotları girecektir.

//SaveChanges(False), EF Core'a gerekli veritabanı komutlarını yürütmesini söyler ancak gerektiğinde yeniden oynatılabilmesi için değişikleri beklemeye/nesneleri takip etmeye devam eder. Taa ki AcceptAllChanges metodunu irademizle çağırana kadar!

//SaveChanges(false) ile işlemin başarılı olduğundan emin olursanız AcceptAllChanges metodu ile nesnelerden takibi kesebilirsiniz.

//var products = await context.Products.ToListAsync();
//products.FirstOrDefault(u => u.Id == 7).Price = 123; //Update
//context.Products.Remove(products.FirstOrDefault(u => u.Id == 8)); //Delete
//products.FirstOrDefault(u => u.Id == 9).ProductName = "asdasd"; //Update

//await context.SaveChangesAsync(false);
//context.ChangeTracker.AcceptAllChanges();

#endregion

#region HasChanges Metodu
//Takip edilen nesneler arasından değişiklik yapılanların olup olmadığının bilgisini verir.
//Arkaplanda DetectChanges metodunu tetikler.
//var result = context.ChangeTracker.HasChanges();
#endregion

#region Entity States
//Entity nesnelerinin durumlarını ifade eder.

#region Detached
//Nesnenin change tracker mekanizması tarafıdnan takip edilmediğini ifade eder. context den gelmedigi icin !
//Product product = new();
//Console.WriteLine(context.Entry(product).State);
//product.ProductName = "asdasd";
//await context.SaveChangesAsync();
#endregion

#region Added
//Veritabanına eklenecek nesneyi ifade eder. Adeed henüz veritabanına işlenmeyen veriyi ifade eder. SaveChanges fonksiyonu çağrıldığında insert sorgusu oluşturucalşığı anlamını gelir.
//Product product = new() { Price = 123, ProductName = "Ürün 1001" };
//Console.WriteLine(context.Entry(product).State); // Detached
//await context.Products.AddAsync(product);
//Console.WriteLine(context.Entry(product).State); // Added
//await context.SaveChangesAsync();
//Console.WriteLine(context.Entry(product).State); // Unchanged
//product.Price = 321;
//Console.WriteLine(context.Entry(product).State); // Updated
//await context.SaveChangesAsync();
//Console.WriteLine(context.Entry(product).State); // Unchanged
#endregion

#region Unchanged
//Veritabanından sorgulandığından beri nesne üzerinde herhangi bir değişiklik yapılmadığını ifade eder. Sorgu neticesinde elde edilen tüm nesneler başlangıçta bu state değerindedir.
//var products = await context.Products.ToListAsync();

//var data = context.ChangeTracker.Entries();
//Console.WriteLine();
#endregion

#region Modified
//Nesne üzerinde değşiiklik/güncelleme yapıldığını ifade eder. SaveChanges fonksiyonu çağrıldığında update sorgusu oluşturulacağı anlamına gelir.
//var product = await context.Products.FirstOrDefaultAsync(u => u.Id == 3);
//Console.WriteLine(context.Entry(product).State);
//product.ProductName = "asdasdasdasdasd";
//Console.WriteLine(context.Entry(product).State);
//await context.SaveChangesAsync(false); // false dedigimiz icin modified olarak kalir cunku takibe devam ediliyor.
//Console.WriteLine(context.Entry(product).State);
#endregion

#region Deleted
//Nesnenin silindiğini ifade eder. SaveChanges fonksiyonu çağrıldığında delete sorgusu oluşturuculağı anlamına gelir.
//var product = await context.Products.FirstOrDefaultAsync(u => u.Id == 4);
//context.Products.Remove(product);
//Console.WriteLine(context.Entry(product).State);
//context.SaveChangesAsync();
#endregion
#endregion

#region Context Nesnesi Üzerinden Change Tracker
//var product = await context.Products.FirstOrDefaultAsync(u => u.Id == 6);
//product.Price = 123;
//product.ProductName = "Silgi"; //Modified | Update

#region Entry Metodu

#region OriginalValues Property'si
//var price = context.Entry(product).OriginalValues.GetValue<float>(nameof(Product.Price));
//var productName = context.Entry(product).OriginalValues.GetValue<string>(nameof(Product.ProductName));
//Console.WriteLine();
#endregion

#region CurrentValues Property'si
//var productName = context.Entry(product).CurrentValues.GetValue<string>(nameof(Product.ProductName));
#endregion

#region GetDatabaseValues Metodu
//var _product = await context.Entry(product).GetDatabaseValuesAsync();
#endregion
#endregion

#endregion

#region Change Tracker'ın Interceptor Olarak Kullanılması
// SaveChangesAsync metodu override edilebilir bir metottur. DbContext sinifinin icinde override edilerek ChangeTracker da Interceptor mantiginda kullanilabilir.

//public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
//{
//    var entries = ChangeTracker.Entries();
//    foreach (var entry in entries)
//    {
//        if (entry.State == EntityState.Added)
//        {

//        }
//    }
//    return base.SaveChangesAsync(cancellationToken);
//}

#endregion



public class ECommerceDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.; Database=ECommerceDb; Integrated Security=True");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {

            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public float Price { get; set; }
}