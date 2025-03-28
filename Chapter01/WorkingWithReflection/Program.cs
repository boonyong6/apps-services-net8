using System.Reflection;
using System.Runtime.CompilerServices; // CompilerGeneratedAttribute
using Packt.Shared; // CoderAttribute

WriteLine("Assembly metadata:");
Assembly? assembly = Assembly.GetEntryAssembly();

if (assembly is null)
{
    WriteLine("Failed to get entry assembly.");
    return;
}

WriteLine($"  Full name: {assembly.FullName}");
WriteLine($"  Location: {assembly.Location}");
WriteLine($"  Entry point: {assembly.EntryPoint?.Name}");

IEnumerable<Attribute> attributes = assembly.GetCustomAttributes();
WriteLine($"  Assembly-level attributes:");
foreach (Attribute a in attributes)
{
    WriteLine($"    {a.GetType()}");
}

var version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
WriteLine($"  Version: {version?.InformationalVersion}");

var company = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();
WriteLine($"  Company: {company?.Company}");

WriteLine();
WriteLine($"* Types:");
Type[] types = assembly.GetTypes();

foreach (Type type in types)
{
    WriteLine();

    if (type.GetCustomAttribute<CompilerGeneratedAttribute>() is not null)
    {
        WriteLine("Skipping compiler-generated types...");
        continue;
    }

    WriteLine($"Type: {type.FullName}");
    MemberInfo[] members = type.GetMembers();

    foreach (MemberInfo member in members)
    {
        WriteLine("{0}: {1} {2}",
            member.MemberType, member.Name, member.DeclaringType?.Name);

        IOrderedEnumerable<CoderAttribute> coders =
            member.GetCustomAttributes<CoderAttribute>()
            .OrderByDescending(c => c.LastModified);

        foreach (CoderAttribute coder in coders)
        {
            WriteLine("-> Modified by {0} on {1}",
                coder.Coder, coder.LastModified.ToShortDateString());
        }
    }
}