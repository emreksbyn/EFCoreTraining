using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

Console.WriteLine();


#region Default Conventions

//public class Employee // Dependent Entity
//{
//    public int Id { get; set; }
//    public string Name { get; set; }

//    // EF asagidaki property i tanimlamasanda Navigation Property lerine gore bu kolonu db ye ekler !!
//    //public int DepartmentId { get; set; }
//    public Department Department { get; set; }
//}

//public class Department
//{
//    public int Id { get; set; }
//    public string DepartmentName { get; set; }

//    public ICollection<Employee> Employees { get; set; }
//}

#endregion

#region Data Annotations
// Default conventions yonteminde foreign key kolonuna karsilik gelen property i tanimladigimizda bu property ismi temel geleneksel entity tanimlama kurallarina uymuyorsa eger Data Annotations lar ile belirtilebilir.

//public class Employee
//{
//    public int Id { get; set; }
//    [ForeignKey(nameof(Department))]
//    public int DepId { get; set; }
//    public string Name { get; set; }

//    public Department Department { get; set; }
//}

//public class Department
//{
//    public int Id { get; set; }
//    public string DepartmentName { get; set; }

//    public ICollection<Employee> Employees { get; set; }
//}

#endregion

#region Fluent API
public class Employee
{
    public int Id { get; set; }
    // Belirtmezsek default olarak DepartmentId olarak db ye eklenir. Ozel isim vereceksek DbContext icinde tanimlamamiz gerekir.
    //public int DepId { get; set; }
    public string Name { get; set; }

    public Department Department { get; set; }
}

public class Department
{
    public int Id { get; set; }
    public string DepartmentName { get; set; }

    public ICollection<Employee> Employees { get; set; }
}

#endregion

public class ECompanyDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.; Database=ECompanyDb; Integrated Security=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            //.HasForeignKey(e => e.DepId)
            ;
    }
}