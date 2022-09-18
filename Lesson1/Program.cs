using Lesson1;
using System.Data.SqlClient;

#region SQL Queries in Code Without ORM
await using SqlConnection connection = new SqlConnection(
    $"Server=.;Database=Northwind; Integrated Security=True");
await connection.OpenAsync();

SqlCommand command = new("Select * from Employees", connection);
SqlDataReader dataReader = await command.ExecuteReaderAsync();
while (await dataReader.ReadAsync())
{
    Console.WriteLine($"{dataReader["FirstName"]} {dataReader["LastName"]}");
}
await connection.CloseAsync();
#endregion


#region With ORM
NorthwindDbContext context = new();
List<Employee> employees = context.Employees.ToList();
foreach (var employee in employees)
{
    Console.WriteLine(employee.FirstName);
} 
#endregion
