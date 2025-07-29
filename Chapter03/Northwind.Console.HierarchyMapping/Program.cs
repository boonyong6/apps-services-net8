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

ContactDetails contactA = new()
{
    Address = new("1234 Broadway Ave, Apt 5B", "New York", "10001", "USA"),
    Phone = "+12125550198",
};

ContactDetails contactB = new()
{
    Address = new("87 Orchard Street, Unit 3A", "New York", "10002", "USA"),
    Phone = "+16465550127",
};

db.Students.Add(new() { Name = "Connor Roy", Subject = "Politics", Contact = contactA });
db.Employees.Add(new() { Name = "Kerry Castellabate", HireDate = DateTime.UtcNow, Contact = contactB });
int affectedRows = db.SaveChanges();
WriteLine($"{affectedRows} people added.");

var queryWithJsonColumn = db.People.Where(p => p.Contact.Address.Postcode == "10001").Select(p => p.Contact.Address.Street);
WriteLine("\nQuery with JSON column example:");
WriteLine(queryWithJsonColumn.ToQueryString());
WriteLine();

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
