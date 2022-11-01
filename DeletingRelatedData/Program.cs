using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();

#region One to One İlişkisel Senaryolarda Veri Silme

//Person? person = await context.Persons
//                .Include(p => p.Address)
//                .FirstOrDefaultAsync(p => p.Address.Id == 1);
//if (person != null)
//    context.Addresses.Remove(person.Address);
//await context.SaveChangesAsync();

#endregion

#region One to Many İlişkisel Senaryolarda Veri Silme

//Blog? blog = await context.Blogs
//            .Include(b => b.Posts)
//            .FirstOrDefaultAsync(b => b.Id == 1);

//Post? post = blog.Posts.FirstOrDefault(x => x.Id == 2);

//context.Posts.Remove(post);
//await context.SaveChangesAsync();

#endregion

#region Many to Many İlişkisel Senaryolarda Veri Silme

//Book? book = await context.Books
//            .Include(b => b.Authors)
//            .FirstOrDefaultAsync(b => b.Id == 1);
//Author? author = book.Authors.FirstOrDefault(a => a.Id == 2);

//// Bu gider principal table dan siler. Veri kaybi olabilir.
////context.Authors.Remove(author); 

//// Bu bizim istedigimiz gibi cross table dan siler.
//book.Authors.Remove(author);
//await context.SaveChangesAsync();

#endregion

#region Cascade Delete
// Bu davranis modelleri Fluent API ile konfigure edilebilmektedir.

#region Cascade
// Esas (Principal) tablodan veri silindiginde, bagimli (dependent) toblodaki iliskili verilerinin de silinmesini saglar.
// EF Core da default davranis seklidir.
// Many to Many iliskilerde zorunlu kullandigimiz davranistir.
#endregion

#region SetNull
// Esas (Principal) tablodan veri silindiginde, bagimli (dependent) toblodaki iliskili verilerine null atanmasini saglar.
// One to One senaryolarda eger ki Foreign Key ve Primary Key kolonlari ayni ise o zaman SetNull davranisini KULLANAMAYIZ.
// Kullanmak istiyorsak Primary Kolon ile Foreign Kolan lar ayri olmalidir.
// One to Many senaryolarda ise null olabilecek property - ? (nullable) - olarak isaretlenmelidir.
/* 
 
 Fluent API de ise   modelBuilder.Entity<Post>()
                    .HasOne(p => p.Blog)
                    .WithMany(b => b.Posts)
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired(false);

 --> isRequired(false) olarak bildirilmelidir.
*/
#endregion

#region Restrict
// Esas tablodan bir veri silinmek istendiginde, bagimli tablosunda iliskili verileri var ise buna izin vermez.
#endregion

//Blog? blog = await context.Blogs.FindAsync(1);
//context.Blogs.Remove(blog);
//await context.SaveChangesAsync();

#endregion

#region Saving Data
//Person person = new()
//{
//    Name = "Emre",
//    Address = new()
//    {
//        PersonAddress = "Istanbul"
//    }
//};

//Person person2 = new()
//{
//    Name = "Ali"
//};

//await context.AddAsync(person);
//await context.AddAsync(person2);

//Blog blog = new()
//{
//    Name = "emrekisaboyun.com Blog",
//    Posts = new List<Post>
//    {
//        new(){ Title = "1. Post" },
//        new(){ Title = "2. Post" },
//        new(){ Title = "3. Post" },
//    }
//};

//await context.Blogs.AddAsync(blog);

//Book book1 = new() { BookName = "1. Kitap" };
//Book book2 = new() { BookName = "2. Kitap" };
//Book book3 = new() { BookName = "3. Kitap" };

//Author author1 = new() { AuthorName = "1. Yazar" };
//Author author2 = new() { AuthorName = "2. Yazar" };
//Author author3 = new() { AuthorName = "3. Yazar" };

//book1.Authors.Add(author1);
//book1.Authors.Add(author2);

//book2.Authors.Add(author1);
//book2.Authors.Add(author2);
//book2.Authors.Add(author3);

//book3.Authors.Add(author3);

//await context.AddAsync(book1);
//await context.AddAsync(book2);
//await context.AddAsync(book3);
//await context.SaveChangesAsync();
#endregion

class Person
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Address Address { get; set; }
}
class Address
{
    public int Id { get; set; }
    public string PersonAddress { get; set; }

    public Person Person { get; set; }
}
class Blog
{
    public Blog()
    {
        Posts = new HashSet<Post>();
    }
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; }
}
class Post
{
    public int Id { get; set; }

    // One to Many iliski kurdugumuz Post - Blog tablolarinda SetNull ozelligini kullanmak istiyorsak BlogId property si ? nullable olarak isaretlenmelidir. Aksi halde migration hatasi aliriz.
    public int? BlogId { get; set; }
    public string Title { get; set; }

    public Blog Blog { get; set; }
}
class Book
{
    public Book()
    {
        Authors = new HashSet<Author>();
    }
    public int Id { get; set; }
    public string BookName { get; set; }

    public ICollection<Author> Authors { get; set; }
}
class Author
{
    public Author()
    {
        Books = new HashSet<Book>();
    }
    public int Id { get; set; }
    public string AuthorName { get; set; }

    public ICollection<Book> Books { get; set; }
}


class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.;Database=ApplicationDb;Integrated Security=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>()
            .HasOne(a => a.Person)
            .WithOne(p => p.Address)
            .HasForeignKey<Address>(a => a.Id);

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Blog)
            .WithMany(b => b.Posts)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books);
    }
}