
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine();

#region Default Conventions
// Her iki entity de Navigation Property ile birbirlerini tekil olarak referans ederek fiziksel bir iliskinin olacagi ifade edilir.
// One to One iliski turunde, dependent entity nin hangisi oldugunu default olarak belirleyebilmek pek kolay degildir. Bu durumda fiziksel olarak bir foreign key' e karsilik property / kolon tanimlayarak cozum getirebiliyoruz.
// Boylece foreign key e karsilik property tanimlayarak luzumsuz bir kolon olusturmus oluyoruz.

//public class Employee
//{
//    public int Id { get; set; }
//    public string Name { get; set; }

//    public EmployeeAddress EmployeeAddress { get; set; }
//}

//public class EmployeeAddress
//{
//    public int Id { get; set; }

//    // One to One iliskide hangisinin dependent hangisinin principal oldugunu, dependent entity nin icinde foreign key i tanimlayarak EF' ye belirtmis oluyoruz.
//    public int EmployeeId { get; set; }
//    public string Address { get; set; }

//    public Employee Employee { get; set; }
//}
#endregion

#region Data Annotations
// Navigation Property ler tanimlanmalidir.
// ForeignKey kolonunun ismi default conventions in disinda bir kolon olacaksa eger ForeignKey attribute ile bunu bildirebiliriz.
// 1 e 1 iliskide ekstradan Foreign Key kolonuna ihtiyac olmayacagi icin dependent entity deki id kolonunu hem foreign key hemde primary key olarak kullanmayi tercih ediyoruz. Bu daha avantajlidir.

//public class Employee
//{
//    public int Id { get; set; }
//    public string Name { get; set; }

//    public EmployeeAddress EmployeeAddress { get; set; }
//}

//public class EmployeeAddress
//{
//    // Bir kolonu Primary ve Foreign Key olarak birlikte tanimlarsak : hem ekstradan EmployeeId kolonuna gerek kalmaz hemde bu EmployeeId kolonuna unique demek zorunda kalmayiz. PrimaryKey oldugu icin Unique olur, ForeignKey oldugu icinde iliskisel olur. "One to One" tablolarda bu yontem daha mantiklidir.
//    [Key, ForeignKey(nameof(Employee))]
//    public int Id { get; set; }

//    //[ForeignKey(nameof(Employee))]
//    //public int EmployeeId { get; set; }
//    public string Address { get; set; }

//    public Employee Employee { get; set; }
//}
#endregion

#region Fluent API
// Digerlerinde oldugu gibi gene Navigation Property ler tanimlanir.
// Geri kalan islemler DbContext sinifinin icinde OnModelCreating metodu override edilerek bu metodun icinde tanimlanir.
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }

    public EmployeeAddress EmployeeAddress { get; set; }
}

public class EmployeeAddress
{
    public int Id { get; set; }

    public string Address { get; set; }

    public Employee Employee { get; set; }
}
#endregion

public class ECompanyDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.; Database=ECompanyDb; Integrated Security=True");
    }

    // Model' larin (Entity lerin) veritabaninda generare edilecek yapilari bu fonksiyon icerisinde konfigure edilir.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Navigation Propertyler iliskilendirildi.
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.EmployeeAddress)
            .WithOne(a => a.Employee)
            // ForeignKey oldugunu belirttik.
            .HasForeignKey<EmployeeAddress>(c => c.Id);

        // Ayni zamanda Primarykey oldugunuda belirttik.
        modelBuilder.Entity<EmployeeAddress>()
            .HasKey(a => a.Id);
    }
}