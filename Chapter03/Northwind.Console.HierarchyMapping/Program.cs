using Microsoft.Data.SqlClient; // To use SqlConnectionStringBuilder.
using Microsoft.EntityFrameworkCore; // GenerateCreateScript()
using Northwind.Models; // HierarchyDb, Person, Student, Employee

SqlConnectionStringBuilder conStrBuilder = new()
{
    // "ServerName\InstanceName" e.g. @".\sqlexpress".
    DataSource = ".",
    InitialCatalog = "HierarchyMapping",
    TrustServerCertificate = true,
    MultipleActiveResultSets = true,
    // Because we want to fail faster. Default is 15 seconds.
    ConnectTimeout = 3,

    // If using Windows Integrated authentication.
    IntegratedSecurity = true,
    //// If using SQL Server authentication.
    //UserID = Environment.GetEnvironmentVariable("MSSQL_USR"),
    //Password = Environment.GetEnvironmentVariable("MSSQL_PWD"),
};

DbContextOptionsBuilder<HierarchyDb> dbCtxOptBuilder = new();
dbCtxOptBuilder.UseSqlServer(conStrBuilder.ConnectionString);

using HierarchyDb db = new(dbCtxOptBuilder.Options);

bool deleted = await db.Database.EnsureDeletedAsync();
WriteLine($"Database deleted: {deleted}");

bool created = await db.Database.EnsureCreatedAsync();
WriteLine($"Database created: {created}");
WriteLine("SQL script used to create the database:");
WriteLine(db.Database.GenerateCreateScript());

db.Students.Add(new() { Name = "Connor Roy", Subject = "Politics" });
db.Employees.Add(new() { Name = "Kerry Castellabate", HireDate = DateTime.UtcNow });
int affectedRows = db.SaveChanges();
WriteLine($"{affectedRows} people added.");

if (db.Students is null || !db.Students.Any())
{
    WriteLine("There are no students.");
}
else
{
    foreach (Student student in db.Students)
    {
        WriteLine("{0} studies {1}", 
            student.Name, student.Subject);
    }
}

if (db.Employees is null || !db.Employees.Any())
{
    WriteLine("There are no employees.");
}
else
{
    foreach (Employee employee in db.Employees)
    {
        WriteLine("{0} was hired on {1}", 
            employee.Name, employee.HireDate);
    }
}

foreach (Person person in db.People)
{
    WriteLine("[{0}] {1} has ID of {2}.",
        person.GetType().Name, person.Name, person.Id);
}
