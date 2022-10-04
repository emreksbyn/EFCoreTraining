using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine();

#region Default Conventions
// Iki entity arasindaki iliskiyi Navigaiton Property den cogul olarak kurmaliyiz. (ICollection, List)
// Cross table otomatik olusur.
// Olusan Cross Table, Composite Primary Key e sahip olarak gelir. - Gelen bu idler Entity isimlerinin cogul haline Id eklenmis halidir(BooksId, AuthorsId) - Yani hem BooksId hemde AuthorsId Primary Key olur. Ikili olarak tekrar engellenmis olur.
//class Book
//{
//    public int Id { get; set; }
//    public string BookName { get; set; }

//    public ICollection<Author> Authors { get; set; }
//}

//class Author
//{
//    public int Id { get; set; }
//    public string AuthorName { get; set; }

//    public ICollection<Book> Books { get; set; }
//}
#endregion

#region Data Annotations
// Cross Table manuel olarak olusturulmak zorundadir.
// Entity lerle olusturdugumuz Cross Table Entity arasinda One to Many iliskisi kurulmali.
// Cross Table da Composite PK yi Data Annotations ile kuramiyoruz. Bunun icin Fluent API ile calisma yapmamiz gerekiyor.
// Cross Table' i manuel olarak olusturdugumuzda bu Entity' i DbContext class' inda DbSet<> olarak vermemiz gerekmez.
//[Key] --> Tek basina Composite PK yapamaz Fluent API uygulamamiz gerekir. Fluent API'de gerekmez kullanmamiz gerekmez.

//class Book
//{
//    public int Id { get; set; }
//    public string BookName { get; set; }

//    public ICollection<BookAuthor> Authors { get; set; }
//}

//// Cross Table
//class BookAuthor
//{
//    //[Key]
//    public int BookId { get; set; }
//    //[Key]
//    public int AuthorId { get; set; }

//    // Entity adindan farkli bir isim vereceksek bu sekilde ForeignKey olduklarini belirmemiz gerekir.

//    //[ForeignKey(nameof(Book))]
//    //public int BId { get; set; }
//    //[ForeignKey(nameof(Author))]
//    //public int AId { get; set; }

//    public Book Book { get; set; }
//    public Author Author { get; set; }
//}

//class Author
//{
//    public int Id { get; set; }
//    public string AuthorName { get; set; }
//    public ICollection<BookAuthor> Books { get; set; }

//}

#endregion

#region Fluent API
// Cross Table manuel olarak olusturulmalidir.
// DbSet<> olarak eklememiz gerekmez.
// Composite PK HasKey metodu ile kurulmalidir.
class Book
{
    public int Id { get; set; }
    public string BookName { get; set; }

    public ICollection<BookAuthor> Authors { get; set; }
}

class BookAuthor
{
    public int BookId { get; set; }
    public int AuthorId { get; set; }

    public Book Book { get; set; }
    public Author Author { get; set; }

}

class Author
{
    public int Id { get; set; }
    public string AuthorName { get; set; }

    public ICollection<BookAuthor> Books { get; set; }
}

#endregion

class EBookDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.; Database=EBookDb; Integrated Security=True;");
    }

    // Data Annotations
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<BookAuthor>()
    //        .HasKey(ba => new { ba.BookId, ba.AuthorId });
    //    //.HasKey(ba => new { ba.BId, ba.AId });
    //}

    // Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.BookId, ba.AuthorId });

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Book)
            .WithMany(b => b.Authors)
            .HasForeignKey(ba => ba.BookId);

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(ba => ba.AuthorId);
    }
}