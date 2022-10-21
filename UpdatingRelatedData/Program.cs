using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();

#region One to One İlişkisel Senaryolarda Veri Güncelleme
#region Saving
//Person person = new Person()
//{
//    Name = "Emre",
//    Address = new Address() { PersonAddress = "Istanbul" }
//};

//Person person2 = new()
//{
//    Name = "Ali"
//};

//await context.Persons.AddRangeAsync(person, person2);
//await context.SaveChangesAsync();
#endregion

#region 1. Durum | Esas tablodaki veriye bağımlı veriyi değiştirme

//Person? person = await context.Persons
//                .Include(p => p.Address)
//                .FirstOrDefaultAsync(p => p.Id == 1);
//context.Addresses.Remove(person.Address);
//person.Address = new Address()
//{
//    PersonAddress = "New Istanbul"
//};
//await context.SaveChangesAsync();

#endregion
#region 2. Durum | Bağımlı verinin ilişkisel olduğu ana veriyi güncelleme

//Address? address = await context.Addresses.FindAsync(1);
//address.Id = 2;
//await context.SaveChangesAsync();

// Yapacagimiz islem bu olursa alacagimiz hata su olur :
// System.InvalidOperationException: 'The property 'Address.Id' is part of a key and so cannot be modified or marked as modified. To change the principal of an existing entity with an identifying foreign key, first delete the dependent and invoke 'SaveChanges', and then associate the dependent with the new principal.'
// Bize oneride bulunuyor dedigi gibi yaparsak amacimiza ulasiriz.

//Address? address = await context.Addresses.FindAsync(2);
//context.Addresses.Remove(address);
//context.SaveChanges();

//var person = await context.Persons.FindAsync(1);
//address.Person = person;

//await context.Addresses.AddAsync(address);
//await context.SaveChangesAsync();

#endregion
#endregion

#region One to Many İlişkisel Senaryolarda Veri Güncelleme
#region Saving

//Blog blog = new Blog()
//{
//    Name = "Emreksbyn.com Blog",
//    Posts = new List<Post>()
//    {
//        new Post(){ Title = "1. Post" },
//        new Post(){ Title = "2. Post" },
//        new Post(){ Title = "3. Post" }
//    }
//};

//await context.Blogs.AddAsync(blog);
//await context.SaveChangesAsync();

#endregion

#region 1. Durum | Esas tablodaki veriye bağımlı verileri değiştirme

//Blog? blog = await context.Blogs
//                .Include(b => b.Posts)
//                .FirstOrDefaultAsync(b => b.Id == 1);

//Post? deletedPost = blog.Posts.FirstOrDefault(p => p.Id == 2);
//blog.Posts.Remove(deletedPost);

//blog.Posts.Add(new Post { Title = "4. Post" });
//blog.Posts.Add(new Post { Title = "5. Post" });

//await context.SaveChangesAsync();

#endregion
#region 2. Durum | Bağımlı verilerin ilişkisel olduğu ana veriyi güncelleme

//Post? post = await context.Posts.FindAsync(4);
//post.Blog = new Blog()
//{
//    Name = "2. Blog"
//};

//context.SaveChanges();

//Post? post = await context.Posts.FindAsync(5);
//Blog? blog = await context.Blogs.FindAsync(2);

//post.Blog = blog;

//context.SaveChanges();

#endregion
#endregion

#region Many to Many İlişkisel Senaryolarda Veri Güncelleme
#region Saving

//Book book1 = new Book { BookName = "1. Book" };
//Book book2 = new Book { BookName = "2. Book" };
//Book book3 = new Book { BookName = "3. Book" };

//Author author1 = new() { AuthorName = "1. Author" };
//Author author2 = new() { AuthorName = "2. Author" };
//Author author3 = new() { AuthorName = "3. Author" };

//book1.Authors.Add(author1);
//book1.Authors.Add(author2);

//book2.Authors.Add(author1);
//book2.Authors.Add(author2);
//book2.Authors.Add(author3);

//book3.Authors.Add(author3);

//await context.AddRangeAsync(book1, book2, book3);
//await context.SaveChangesAsync();
#endregion

#region 1. Örnek

//Book? book = await context.Books.FindAsync(1);
//Author? author = await context.Authors.FindAsync(3);
//book.Authors.Add(author);

//await context.SaveChangesAsync();

#endregion
#region 2. Örnek

//Author? author = await context.Authors
//    .Include(a => a.Books)
//    .FirstOrDefaultAsync(a => a.Id == 3);

//foreach (var book in author.Books)
//{
//    if (book.Id != 1)
//        author.Books.Remove(book);
//}
//await context.SaveChangesAsync();

#endregion
#region 3. Örnek

//Book? book = await context.Books
//            .Include(b => b.Authors)
//            .FirstOrDefaultAsync(b => b.Id == 2);

//Author? deletedAuthor = book.Authors.FirstOrDefault(a => a.Id == 1);
//book.Authors.Remove(deletedAuthor);

//Author? addedAuthor = await context.Authors.FindAsync(3);
//book.Authors.Add(addedAuthor);
//book.Authors.Add(new Author { AuthorName = "4. Author" });

//await context.SaveChangesAsync();

#endregion
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
    public int BlogId { get; set; }
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
        optionsBuilder.UseSqlServer("Server=.;Database=ApplicationDb;Trusted_Connection=Yes");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>()
            .HasOne(a => a.Person)
            .WithOne(p => p.Address)
            .HasForeignKey<Address>(a => a.Id);
    }
}