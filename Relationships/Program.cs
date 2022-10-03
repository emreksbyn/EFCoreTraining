

#region Relationships Terimleri

#region Principal Entity(Asil Entity)
// Kendi basina var olabilen tabloyu modelleyen entity' e denir.
// --> "Departments" tablosunu modelleyen "Department" entity' sidir.
#endregion

#region Dependent Entity(Bagimli Entity)
// Kendi basina var olamayan, bir baska tabloya bagimli(iliskisel olarak bagimli) olan tabloyu modelleyen entity' e denir.
// "Employees" tablosunu modelleyen "Employee" entity' sidir.
#endregion

#region Foreign Key
// Principal ile Dependant entity arasindaki iliskiyi saglayan key' dir.
// Dependent Entity de tanimlanir.
// Principal Entity deki Principal Key i tutar.
#endregion

#region Principal Key
// Principal Entity deki id' nin kendisidir. Principal Entity nin kimligi olan kolonu ifade eden property dir.
#endregion

#endregion


#region Navigation Property ??
// Iliskisel tablolar arasindaki fiziksel erisimi entity class lari uzerinden saglayan property' lerdir.
// Bir property' nin navigation property olabilmesi icin kesinlikle entity turunden olmasi gerekir.
// Navigation propert ler entity lerdeki tanimlarina gore N to N veya 1 to N seklinde iliski turlerini ifade etmektedir. 
#endregion


#region Iliski Turleri

#region One to One
// Calisan ile addresi arasindaki iliski ornek olabilir.
// Karı koca arasindaki iliski olabilir.
#endregion

#region One to Many
// Calisan ile departmani arasindaki iliski.
// Anne ile cocuklari arasindaki iliski.
#endregion

#region Many to Many
// Bir calisanin birden fazla projesi olabilir. Bir projede de birden fazla calisan olabilir.
#endregion

#endregion


#region EF Core da Iliski Yapilandirma Yontemleri

#region Default Conventions
// Varsayilan entity kurallarini kullanarak yapilan iliski yapilandirma yontemleridir.
// Navigation property leri kullanarak iliski sablonlarini cikarmaktadir.
#endregion

#region Data Annotations Attributes
// Entity nin niteliklerine gore ince ayarlar yapmamizi saglayan attribute lardir. [Key], [ForeignKey]..
#endregion

#region Fluent API
// Entity modelllerindeki iliskileri yapilandirirken daha detayli calismamizi saglayan yontemdir.

#region HasOne
// Ilgili entity nin iliskisel entity' e One to One yada One to N(Many) olacak sekilde iliskisini yapilandirmaya baslayan metottur.
#endregion

#region HasMany
// Ilgili entity nin iliskisel entity' e N(Many) to One yada N to N olacak sekilde iliskisini yapilandirmaya baslayan metottur.
#endregion

#region WithOne
// HasOne yada HasMany den sonra One to One yada N(Many) to One olacak sekilde iliski yapilandirmasini tamamlayan metottur.
#endregion

#region WithMany
// HasOne yada HasMany den sonra One to N(Many) yada N to N olacak sekilde iliski yapilandirmasini tamamlayan metottur.
#endregion

#endregion

#endregion


Console.WriteLine();

class Employee
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public int DepartmentId { get; set; }

    // Navigation property
    public Department Department { get; set; }
}

class Department
{
    public int Id { get; set; }
    public string DepartmentName { get; set; }

    // Navigation property
    public List<Employee> Employees { get; set; }
}