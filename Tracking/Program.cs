using Microsoft.EntityFrameworkCore;

ECommerceDbContext context = new();

#region AsNoTracking Method
// Context uzerinden gelen tum datalar Change Tracker ile takip edilir.
// Gelen verinin sayisi ile dogru orantili bir sekilde maliyeti artar. Bu yuzden uzerinde islem yapilmayacak verilerin Change Tracker ile takip edilmesi gereksizdir. Ama islem yapilacaksa Change Tracker aktif olmalidir.
// AsNoTracking metodu context uzerinden gelen datalarin takip edilmesini engeller. Maliyeti dusurur.
// AsNoTacking ile sorgu yaptigimiz verileri kullanabiliriz (View) fakat uzerinde islem (update) yapamayiz.

//var users = await context.Users.AsNoTracking().ToListAsync();

// AsNoTracking devrede oldugu icin SaveChanges metodu bir ise yaramayacaktir.
//foreach (var user in users)
//{
//    Console.WriteLine(user.Name);
//    user.Name = $"new -> {user.Name}";
//}
//await context.SaveChangesAsync();

// context nesnesi uzerinden Update() Remove() metotlarini cagirirsak bu sefer SaveChanges() degisiklikleri kayit eder.
//foreach (var user in users)
//{
//    Console.WriteLine(user.Name);
//    user.Name = $"new -> {user.Name}";
//    context.Users.Update(user);
//    //context.Users.Remove(user);
//}
//await context.SaveChangesAsync();
#endregion

#region AsNoTrackingWithIdentityResolution
// Change Tracker(CT) mekanizmasi sayesinde yinelenen datalar ayni instance' lari kullanirlar. Performans' i arttirir.
// AsNoTracking metodu ile yapilan sorgularda ise yinelenen datalar farkli instance' ler ile karsilanirlar cunku bir takip soz konusu degildir. Bu da maliyete sebep olur. Ozellikle iliskisel tablolarda bu duruma dikkat etmeliyiz.

// Boyle durumlarda hem takip maliyetini hemde yinelenen datalari ayni instance ile karsilayip buradaki maliyetten kurtulmak istiyorsak AsNoTrackingWithIdentityResolution fonksiyonunu kullanmaliyiz.

//var books = await context.Books.Include(b => b.Authors).AsNoTrackingWithIdentityResolution().ToListAsync();

// AsNoTrackingWithIdentityResolution fonksiyonu AsNoTracking fonk a nazaran gorece daha yavastir/maliyetlidir. Lakin CT ye nazaran daha performanlidir ve daha az maliyetlidir.
#endregion


#region AsTracking
// Context uzerinden gelen datalarin CT tarafindan takip edilmesini iradeli bir sekilde ifade etmemizi saglar. 
// Peki neden kullanalim ?
// UseQueryTrackingBehavior metodunun davranisi geregi uygulama seviyesinde CT' nin default olarak devrede olup olmamasini ayarliyor olacagiz. Eger ki default olarak pasif hale getirilirse boyle durumlarda takip mekanizmasinin ihtiyac oldugu sorgularda AsTracking() fonk kullanilabilir ve boylece takip mekanizmasi iradeli bir sekilde devreye sokmus oluruz.

//var books = await context.Books.AsTracking().ToListAsync();
#endregion

#region UseQueryTrackingBehavior
// EF Core seviyesinde / uygulama seviyesinde ilgili context ten gelen verilerin uzerinde CT mekanizmasinin davranisi temel seviyede belirlememizi saglayan fonksiyondur. Yani konfigurasyon fonksiyonudur.
#endregion

Console.WriteLine();

#region DbContext and Entities

public class ECommerceDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.; Database=ECommerceDb; Integrated Security=True");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public ICollection<Role> Roles { get; set; }
}

public class Role
{
    public int Id { get; set; }
    public string RoleName { get; set; }

    public ICollection<User> Users { get; set; }
}

public class Book
{
    public Book() => Console.WriteLine("Book Created.");
    public int Id { get; set; }
    public string BookName { get; set; }
    public int PageNumber { get; set; }

    public ICollection<Author> Authors { get; set; }
}

public class Author
{
    public Author() => Console.WriteLine("Author Created.");
    public int Id { get; set; }
    public string AuthorName { get; set; }

    public ICollection<Book> Books { get; set; }
}
#endregion