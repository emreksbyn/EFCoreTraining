using Microsoft.EntityFrameworkCore;
using System.Net;
using System;
using System.Reflection.Metadata;

Console.WriteLine();

AppDbContext context = new();

#region One to One Iliskisel Senaryolarda Veri Ekleme
// Eger once principal eklenecekse dependent entity vermek zorunda degiliz.
// Fakat once dependent eklenecekse principal entity eklemek zorundayiz !


// 1- Principal Entity Uzerinden Dependent Entity Ekleme

//Person person = new();
//person.Name = "Emre";
//person.Address = new() { PersonAddress = "Zonguldak" };

//await context.AddAsync(person);
//await context.SaveChangesAsync();

// 2- Dependent Entity Uzerinden Principal Entity Ekleme

//Address address = new Address();
//address.PersonAddress = "Gokcebey";
//address.Person = new Person() { Name = "Ali" };

//await context.AddAsync(address);
//await context.SaveChangesAsync();

//class Person
//{
//    public int Id { get; set; }
//    public string Name { get; set; }

//    public Address Address { get; set; }
//}

//class Address
//{
//    public int Id { get; set; }
//    public string PersonAddress { get; set; }

//    public Person Person { get; set; }
//}

//class AppDbContext : DbContext
//{
//    public DbSet<Person> Persons { get; set; }
//    public DbSet<Address> Addresses { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer("Server=.; Database= AppDb; Integrated Security=True;");
//    }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Address>()
//            .HasOne(a => a.Person)
//            .WithOne(p => p.Address)
//            .HasForeignKey<Address>(a => a.Id);
//    }
//}

#endregion

#region One to Many Iliskisel Senaryolarda Veri Ekleme

#region 1- Principal Entity Uzerinden Dependent Entity Ekleme
// Nesne Referansi Uzerinden Ekleme

//Blog blog = new() { Name = "emrekisaboyun.com" };
//blog.Posts.Add(new Post { Title = "Post 1" });
//blog.Posts.Add(new Post { Title = "Post 2" });
//blog.Posts.Add(new Post { Title = "Post 3" });
//await context.AddAsync(blog);
//await context.SaveChangesAsync();

// Object Initializer Uzerinden Ekleme

//Blog blog2 = new Blog()
//{
//    Name = "A Blog",
//    Posts = new HashSet<Post>() { new Post { Title = "Post 4" }, new Post { Title = "Post 5" } }
//};
//await context.AddAsync(blog2);
//await context.SaveChangesAsync();

#endregion

#region 2- Dependent Entity Uzerinden Principal Entity Ekleme

// Dependent entity uzerinden veri ekleyecegimizde principal entity ile beraber eklemek zorundayiz ! 
//Post post = new()
//{
//    Title = "Post 6",
//    Blog = new Blog() { Name = "B Blog" }
//};

//await context.AddAsync(post);
//await context.SaveChangesAsync();


#endregion

#region 3- Foreign Key Kolonu Uzerinden Veri Ekleme
// 1. ve 2. yontemler hic olmayan verilerin iliskisel olarak eklenmesini saglarken, bu 3. yontem onceden eklenmis olan principal entity verisiyle yeni dependent entitylerin iliskisel olarak eslestirilmesini saglamaktadir.

//Post post = new Post()
//{
//    Title = "Post 7",
//    BlogId = 1
//};

//await context.AddAsync(post);
//await context.SaveChangesAsync();

#endregion

//class Blog
//{
//    public Blog()
//    {
//        Posts = new HashSet<Post>();
//    }
//    public int Id { get; set; }
//    public string Name { get; set; }

//    public ICollection<Post> Posts { get; set; }
//}

//class Post
//{
//    public int Id { get; set; }
//    public int BlogId { get; set; }
//    public string Title { get; set; }

//    public Blog Blog { get; set; }
//}

//class AppDbContext : DbContext
//{
//    public DbSet<Blog> Blogs { get; set; }
//    public DbSet<Post> Posts { get; set; }
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer("Server=.; Database= AppDb; Integrated Security=True;");
//    }
//}
#endregion

#region Many to Many Iliskisel Senaryolarda Veri Ekleme

#region N to N iliskisi eger ki default conventions uzerinden tasarlanmissa kullanilan bir yontemdir.

//Book book = new Book()
//{
//    BookName = "A Kitabi",
//    Authors = new HashSet<Author>()
//    {
//        new Author() { AuthorName = "Emre" }
//    }
//};

//context.Books.Add(book);
//context.SaveChanges();

#endregion

#region N to N iliskisi eger ki fluent api ile tasarlanmis ise kullanilan bir yontemdir.

//Author author = new Author()
//{
//    AuthorName = "Ali",
//    Books = new HashSet<BookAuthor>()
//    {
//        new BookAuthor() { BookId =1 },
//        new BookAuthor() { Book = new Book() { BookName = "B Kitabi"} }
//    }
//};

//await context.AddAsync(author);
//await context.SaveChangesAsync();

#endregion

#region Default Conventions icin

//class Book
//{
//    public Book()
//    {
//        Authors = new HashSet<Author>();
//    }
//    public int Id { get; set; }
//    public string BookName { get; set; }

//    public ICollection<Author> Authors { get; set; }
//}

//class Author
//{
//    public Author()
//    {
//        Books = new HashSet<Book>();
//    }
//    public int Id { get; set; }
//    public string AuthorName { get; set; }

//    public ICollection<Book> Books { get; set; }
//}

#endregion

#region Fluent API icin
class Book
{
    public Book()
    {
        Authors = new HashSet<BookAuthor>();
    }
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
    public Author()
    {
        Books = new HashSet<BookAuthor>();
    }
    public int Id { get; set; }
    public string AuthorName { get; set; }

    public ICollection<BookAuthor> Books { get; set; }
}
#endregion


class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.; Database= AppDb; Integrated Security=True;");
    }

    // Fluent API icin
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

#endregion